namespace LocationServer
{
    partial class Server
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
            this.debug_checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ProtocolPath_textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StartServer_button1 = new System.Windows.Forms.Button();
            this.Port_No_textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timeout_textBox2 = new System.Windows.Forms.TextBox();
            this.message_listBox1 = new System.Windows.Forms.ListBox();
            this.Savedetails_button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // debug_checkBox1
            // 
            this.debug_checkBox1.AutoSize = true;
            this.debug_checkBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debug_checkBox1.Location = new System.Drawing.Point(608, 68);
            this.debug_checkBox1.Name = "debug_checkBox1";
            this.debug_checkBox1.Size = new System.Drawing.Size(92, 28);
            this.debug_checkBox1.TabIndex = 0;
            this.debug_checkBox1.Text = "Debug:";
            this.debug_checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Protocol Used:";
            // 
            // ProtocolPath_textBox1
            // 
            this.ProtocolPath_textBox1.Location = new System.Drawing.Point(155, 80);
            this.ProtocolPath_textBox1.Name = "ProtocolPath_textBox1";
            this.ProtocolPath_textBox1.Size = new System.Drawing.Size(212, 22);
            this.ProtocolPath_textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(319, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Console Messages";
            // 
            // StartServer_button1
            // 
            this.StartServer_button1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartServer_button1.Location = new System.Drawing.Point(155, 389);
            this.StartServer_button1.Name = "StartServer_button1";
            this.StartServer_button1.Size = new System.Drawing.Size(216, 37);
            this.StartServer_button1.TabIndex = 5;
            this.StartServer_button1.Text = "Start Server:";
            this.StartServer_button1.UseVisualStyleBackColor = true;
            this.StartServer_button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Port_No_textBox2
            // 
            this.Port_No_textBox2.Location = new System.Drawing.Point(155, 117);
            this.Port_No_textBox2.Name = "Port_No_textBox2";
            this.Port_No_textBox2.Size = new System.Drawing.Size(212, 22);
            this.Port_No_textBox2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "Port No:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(384, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Timeout:";
            // 
            // timeout_textBox2
            // 
            this.timeout_textBox2.Location = new System.Drawing.Point(488, 120);
            this.timeout_textBox2.Name = "timeout_textBox2";
            this.timeout_textBox2.Size = new System.Drawing.Size(212, 22);
            this.timeout_textBox2.TabIndex = 9;
            // 
            // message_listBox1
            // 
            this.message_listBox1.FormattingEnabled = true;
            this.message_listBox1.ItemHeight = 16;
            this.message_listBox1.Location = new System.Drawing.Point(157, 218);
            this.message_listBox1.Name = "message_listBox1";
            this.message_listBox1.Size = new System.Drawing.Size(468, 132);
            this.message_listBox1.TabIndex = 10;
            // 
            // Savedetails_button1
            // 
            this.Savedetails_button1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Savedetails_button1.Location = new System.Drawing.Point(509, 390);
            this.Savedetails_button1.Name = "Savedetails_button1";
            this.Savedetails_button1.Size = new System.Drawing.Size(90, 35);
            this.Savedetails_button1.TabIndex = 11;
            this.Savedetails_button1.Text = "Save";
            this.Savedetails_button1.UseVisualStyleBackColor = true;
            this.Savedetails_button1.Click += new System.EventHandler(this.Savedetails_button1_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Savedetails_button1);
            this.Controls.Add(this.message_listBox1);
            this.Controls.Add(this.timeout_textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Port_No_textBox2);
            this.Controls.Add(this.StartServer_button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ProtocolPath_textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.debug_checkBox1);
            this.Name = "Server";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox debug_checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ProtocolPath_textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button StartServer_button1;
        private System.Windows.Forms.TextBox Port_No_textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox timeout_textBox2;
        private System.Windows.Forms.ListBox message_listBox1;
        private System.Windows.Forms.Button Savedetails_button1;
    }
}