namespace demo
{
    partial class IpEnter
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
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_connect = new System.Windows.Forms.Button();
            this.textBox_ipaddress = new System.Windows.Forms.TextBox();
            this.label_ip = new System.Windows.Forms.Label();
            this.label_port = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(146, 99);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 7;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(14, 99);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(75, 23);
            this.button_connect.TabIndex = 6;
            this.button_connect.Text = "连接";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // textBox_ipaddress
            // 
            this.textBox_ipaddress.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.helpProvider1.SetHelpString(this.textBox_ipaddress, "HZ IP: 192.168.1.133");
            this.textBox_ipaddress.Location = new System.Drawing.Point(12, 47);
            this.textBox_ipaddress.Name = "textBox_ipaddress";
            this.helpProvider1.SetShowHelp(this.textBox_ipaddress, true);
            this.textBox_ipaddress.Size = new System.Drawing.Size(149, 26);
            this.textBox_ipaddress.TabIndex = 5;
            this.textBox_ipaddress.Text = "192.168.10.110";
            this.textBox_ipaddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_ipaddress_KeyPress);
            // 
            // label_ip
            // 
            this.label_ip.AutoSize = true;
            this.label_ip.Location = new System.Drawing.Point(12, 21);
            this.label_ip.Name = "label_ip";
            this.label_ip.Size = new System.Drawing.Size(65, 12);
            this.label_ip.TabIndex = 4;
            this.label_ip.Text = "目标IP地址";
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(180, 21);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(41, 12);
            this.label_port.TabIndex = 8;
            this.label_port.Text = "端口号";
            // 
            // textBox_port
            // 
            this.textBox_port.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.helpProvider1.SetHelpString(this.textBox_port, "21");
            this.textBox_port.Location = new System.Drawing.Point(169, 47);
            this.textBox_port.Name = "textBox_port";
            this.helpProvider1.SetShowHelp(this.textBox_port, true);
            this.textBox_port.Size = new System.Drawing.Size(65, 26);
            this.textBox_port.TabIndex = 9;
            this.textBox_port.Text = "21";
            // 
            // IpEnter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 160);
            this.Controls.Add(this.textBox_ipaddress);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label_port);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.label_ip);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IpEnter";
            this.Text = "IpEnter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.TextBox textBox_ipaddress;
        private System.Windows.Forms.Label label_ip;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}