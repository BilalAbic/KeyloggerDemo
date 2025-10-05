namespace Keylogger
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbnKeylogger = new System.Windows.Forms.Label();
            this.lbnVeriSilme1 = new System.Windows.Forms.Label();
            this.lbnVeriSilme2 = new System.Windows.Forms.Label();
            this.lbnVeriSilme3 = new System.Windows.Forms.Label();
            this.rbtOnay = new System.Windows.Forms.RadioButton();
            this.lbnEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.llbnProjeLink = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.llbnProfilLink = new System.Windows.Forms.LinkLabel();
            this.btnOnay = new System.Windows.Forms.Button();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lbnKeylogger
            // 
            this.lbnKeylogger.AutoSize = true;
            this.lbnKeylogger.Font = new System.Drawing.Font("Informal Roman", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnKeylogger.Location = new System.Drawing.Point(41, 24);
            this.lbnKeylogger.Name = "lbnKeylogger";
            this.lbnKeylogger.Size = new System.Drawing.Size(383, 103);
            this.lbnKeylogger.TabIndex = 0;
            this.lbnKeylogger.Text = "Keylogger";
            // 
            // lbnVeriSilme1
            // 
            this.lbnVeriSilme1.AutoSize = true;
            this.lbnVeriSilme1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnVeriSilme1.Location = new System.Drawing.Point(55, 250);
            this.lbnVeriSilme1.Name = "lbnVeriSilme1";
            this.lbnVeriSilme1.Size = new System.Drawing.Size(276, 24);
            this.lbnVeriSilme1.TabIndex = 1;
            this.lbnVeriSilme1.Text = "Veri silme talebi için sol taraftaki ";
            // 
            // lbnVeriSilme2
            // 
            this.lbnVeriSilme2.AutoSize = true;
            this.lbnVeriSilme2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnVeriSilme2.Location = new System.Drawing.Point(55, 276);
            this.lbnVeriSilme2.Name = "lbnVeriSilme2";
            this.lbnVeriSilme2.Size = new System.Drawing.Size(391, 24);
            this.lbnVeriSilme2.TabIndex = 2;
            this.lbnVeriSilme2.Text = "‘Veri Silme Talebi’ formunu eksiksiz doldurun;";
            // 
            // lbnVeriSilme3
            // 
            this.lbnVeriSilme3.AutoSize = true;
            this.lbnVeriSilme3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnVeriSilme3.Location = new System.Drawing.Point(55, 302);
            this.lbnVeriSilme3.Name = "lbnVeriSilme3";
            this.lbnVeriSilme3.Size = new System.Drawing.Size(568, 24);
            this.lbnVeriSilme3.TabIndex = 3;
            this.lbnVeriSilme3.Text = "talebiniz alındıktan sonra işlem durumunuz e‑posta ile bildirilecektir.";
            // 
            // rbtOnay
            // 
            this.rbtOnay.AutoSize = true;
            this.rbtOnay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOnay.Location = new System.Drawing.Point(661, 282);
            this.rbtOnay.Name = "rbtOnay";
            this.rbtOnay.Size = new System.Drawing.Size(129, 24);
            this.rbtOnay.TabIndex = 4;
            this.rbtOnay.TabStop = true;
            this.rbtOnay.Text = "Veriler Silinsin ";
            this.rbtOnay.UseVisualStyleBackColor = true;
            // 
            // lbnEmail
            // 
            this.lbnEmail.AutoSize = true;
            this.lbnEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnEmail.Location = new System.Drawing.Point(657, 253);
            this.lbnEmail.Name = "lbnEmail";
            this.lbnEmail.Size = new System.Drawing.Size(68, 24);
            this.lbnEmail.TabIndex = 6;
            this.lbnEmail.Text = "E-Mail:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(723, 250);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(161, 29);
            this.txtEmail.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(720, 86);
            this.label1.TabIndex = 8;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(55, 374);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "Proje:";
            // 
            // llbnProjeLink
            // 
            this.llbnProjeLink.AutoSize = true;
            this.llbnProjeLink.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.llbnProjeLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.llbnProjeLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbnProjeLink.Location = new System.Drawing.Point(111, 374);
            this.llbnProjeLink.Name = "llbnProjeLink";
            this.llbnProjeLink.Size = new System.Drawing.Size(147, 24);
            this.llbnProjeLink.TabIndex = 10;
            this.llbnProjeLink.TabStop = true;
            this.llbnProjeLink.Text = "KeyloggerDemo";
            this.llbnProjeLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbnProjeLink_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(295, 374);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "Yazar:";
            // 
            // llbnProfilLink
            // 
            this.llbnProfilLink.AutoSize = true;
            this.llbnProfilLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llbnProfilLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbnProfilLink.Location = new System.Drawing.Point(354, 374);
            this.llbnProfilLink.Name = "llbnProfilLink";
            this.llbnProfilLink.Size = new System.Drawing.Size(82, 24);
            this.llbnProfilLink.TabIndex = 12;
            this.llbnProfilLink.TabStop = true;
            this.llbnProfilLink.Text = "BilalAbic";
            this.llbnProfilLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbnProfilLink_LinkClicked);
            // 
            // btnOnay
            // 
            this.btnOnay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(222)))), ((int)(((byte)(189)))));
            this.btnOnay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOnay.Location = new System.Drawing.Point(661, 309);
            this.btnOnay.Name = "btnOnay";
            this.btnOnay.Size = new System.Drawing.Size(223, 36);
            this.btnOnay.TabIndex = 14;
            this.btnOnay.Text = "Gönder";
            this.btnOnay.UseVisualStyleBackColor = false;
            // 
            // rtxtLog
            // 
            this.rtxtLog.Location = new System.Drawing.Point(59, 420);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(840, 120);
            this.rtxtLog.TabIndex = 15;
            this.rtxtLog.Text = "";
            this.rtxtLog.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(161)))), ((int)(((byte)(186)))));
            this.ClientSize = new System.Drawing.Size(951, 566);
            this.Controls.Add(this.rtxtLog);
            this.Controls.Add(this.btnOnay);
            this.Controls.Add(this.llbnProfilLink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.llbnProjeLink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lbnEmail);
            this.Controls.Add(this.rbtOnay);
            this.Controls.Add(this.lbnVeriSilme3);
            this.Controls.Add(this.lbnVeriSilme2);
            this.Controls.Add(this.lbnVeriSilme1);
            this.Controls.Add(this.lbnKeylogger);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keylogger";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbnKeylogger;
        private System.Windows.Forms.Label lbnVeriSilme1;
        private System.Windows.Forms.Label lbnVeriSilme2;
        private System.Windows.Forms.Label lbnVeriSilme3;
        private System.Windows.Forms.RadioButton rbtOnay;
        private System.Windows.Forms.Label lbnEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llbnProjeLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llbnProfilLink;
        private System.Windows.Forms.Button btnOnay;
        private System.Windows.Forms.RichTextBox rtxtLog;
    }
}

