
namespace BotAnzeigen
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStartBot = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStopBot = new System.Windows.Forms.Button();
            this.listBoxAdList = new System.Windows.Forms.ListBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelSearchURL = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSearchUrl = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtMessageText = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(486, 104);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 127);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Anleitung";
            // 
            // btnStartBot
            // 
            this.btnStartBot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnStartBot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStartBot.Location = new System.Drawing.Point(12, 494);
            this.btnStartBot.Name = "btnStartBot";
            this.btnStartBot.Size = new System.Drawing.Size(109, 42);
            this.btnStartBot.TabIndex = 2;
            this.btnStartBot.Text = "Bot starten";
            this.btnStartBot.UseVisualStyleBackColor = false;
            this.btnStartBot.Click += new System.EventHandler(this.btnStartBot_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nachrichtentext";
            // 
            // btnStopBot
            // 
            this.btnStopBot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnStopBot.Enabled = false;
            this.btnStopBot.Location = new System.Drawing.Point(127, 494);
            this.btnStopBot.Name = "btnStopBot";
            this.btnStopBot.Size = new System.Drawing.Size(109, 42);
            this.btnStopBot.TabIndex = 5;
            this.btnStopBot.Text = "Bot stoppen";
            this.btnStopBot.UseVisualStyleBackColor = false;
            this.btnStopBot.Click += new System.EventHandler(this.btnStopBot_Click);
            // 
            // listBoxAdList
            // 
            this.listBoxAdList.FormattingEnabled = true;
            this.listBoxAdList.ImeMode = System.Windows.Forms.ImeMode.On;
            this.listBoxAdList.Location = new System.Drawing.Point(15, 37);
            this.listBoxAdList.Name = "listBoxAdList";
            this.listBoxAdList.Size = new System.Drawing.Size(573, 134);
            this.listBoxAdList.TabIndex = 8;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(16, 32);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(55, 13);
            this.labelUsername.TabIndex = 9;
            this.labelUsername.Text = "Username";
            // 
            // labelSearchURL
            // 
            this.labelSearchURL.AutoSize = true;
            this.labelSearchURL.Location = new System.Drawing.Point(11, 109);
            this.labelSearchURL.Name = "labelSearchURL";
            this.labelSearchURL.Size = new System.Drawing.Size(57, 13);
            this.labelSearchURL.TabIndex = 10;
            this.labelSearchURL.Text = "Such-URL";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(21, 58);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(50, 13);
            this.labelPassword.TabIndex = 11;
            this.labelPassword.Text = "Passwort";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSearchUrl);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUsername);
            this.groupBox2.Controls.Add(this.labelUsername);
            this.groupBox2.Controls.Add(this.labelSearchURL);
            this.groupBox2.Controls.Add(this.labelPassword);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtMessageText);
            this.groupBox2.Location = new System.Drawing.Point(12, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(602, 155);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Einstellungen";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // txtSearchUrl
            // 
            this.txtSearchUrl.Location = new System.Drawing.Point(77, 106);
            this.txtSearchUrl.Name = "txtSearchUrl";
            this.txtSearchUrl.Size = new System.Drawing.Size(126, 20);
            this.txtSearchUrl.TabIndex = 14;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(77, 55);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(126, 20);
            this.txtPassword.TabIndex = 13;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.SystemColors.Window;
            this.txtUsername.Location = new System.Drawing.Point(77, 29);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(126, 20);
            this.txtUsername.TabIndex = 12;
            // 
            // txtMessageText
            // 
            this.txtMessageText.Location = new System.Drawing.Point(219, 29);
            this.txtMessageText.Multiline = true;
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.Size = new System.Drawing.Size(369, 112);
            this.txtMessageText.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.listBoxAdList);
            this.groupBox3.Location = new System.Drawing.Point(12, 306);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(602, 182);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Logging";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Angefragte Anzeigen";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(626, 543);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnStopBot);
            this.Controls.Add(this.btnStartBot);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "BotAnzeigen";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStartBot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStopBot;
        private System.Windows.Forms.ListBox listBoxAdList;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelSearchURL;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSearchUrl;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtMessageText;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

