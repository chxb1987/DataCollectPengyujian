namespace demo
{
    partial class frmAddnc
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
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Test = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtver5 = new System.Windows.Forms.TextBox();
            this.txtdt = new System.Windows.Forms.TextBox();
            this.txtver4 = new System.Windows.Forms.TextBox();
            this.txtver3 = new System.Windows.Forms.TextBox();
            this.txtver2 = new System.Windows.Forms.TextBox();
            this.txtver1 = new System.Windows.Forms.TextBox();
            this.txtxinhao = new System.Windows.Forms.TextBox();
            this.txtnum = new System.Windows.Forms.TextBox();
            this.txtid = new System.Windows.Forms.TextBox();
            this.reset = new System.Windows.Forms.Button();
            this.registe = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.txtFac = new System.Windows.Forms.ComboBox();
            this.labnum = new System.Windows.Forms.Label();
            this.cmbnum = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(286, 23);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(140, 25);
            this.txtIP.TabIndex = 0;
            this.txtIP.Text = "192.168.188.130";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(216, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(39, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "生产厂商";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(511, 23);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(54, 25);
            this.txtPort.TabIndex = 0;
            this.txtPort.Text = "5005";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(441, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "端口号";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbnum);
            this.groupBox1.Controls.Add(this.txtFac);
            this.groupBox1.Controls.Add(this.Test);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.labnum);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(879, 64);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通信参数";
            // 
            // Test
            // 
            this.Test.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Test.Location = new System.Drawing.Point(747, 18);
            this.Test.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(112, 35);
            this.Test.TabIndex = 0;
            this.Test.Text = "测试连接";
            this.Test.UseVisualStyleBackColor = false;
            this.Test.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtver5);
            this.groupBox2.Controls.Add(this.txtdt);
            this.groupBox2.Controls.Add(this.txtver4);
            this.groupBox2.Controls.Add(this.txtver3);
            this.groupBox2.Controls.Add(this.txtver2);
            this.groupBox2.Controls.Add(this.txtver1);
            this.groupBox2.Controls.Add(this.txtxinhao);
            this.groupBox2.Controls.Add(this.txtnum);
            this.groupBox2.Controls.Add(this.txtid);
            this.groupBox2.Location = new System.Drawing.Point(16, 86);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(879, 239);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "注册参数";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(271, 100);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 18);
            this.label9.TabIndex = 1;
            this.label9.Text = "版本5";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(23, 136);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 18);
            this.label11.TabIndex = 1;
            this.label11.Text = "注册时间";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(23, 102);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 18);
            this.label8.TabIndex = 1;
            this.label8.Text = "版本4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(521, 64);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 18);
            this.label7.TabIndex = 1;
            this.label7.Text = "版本3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(271, 66);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "版本2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(23, 69);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 18);
            this.label5.TabIndex = 1;
            this.label5.Text = "版本1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(521, 30);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 1;
            this.label12.Text = "系统型号";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(271, 32);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 18);
            this.label10.TabIndex = 1;
            this.label10.Text = "系统编号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(23, 35);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "系统ID";
            // 
            // txtver5
            // 
            this.txtver5.Location = new System.Drawing.Point(363, 94);
            this.txtver5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtver5.Name = "txtver5";
            this.txtver5.Size = new System.Drawing.Size(140, 25);
            this.txtver5.TabIndex = 0;
            // 
            // txtdt
            // 
            this.txtdt.Location = new System.Drawing.Point(115, 130);
            this.txtdt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtdt.Name = "txtdt";
            this.txtdt.Size = new System.Drawing.Size(140, 25);
            this.txtdt.TabIndex = 0;
            // 
            // txtver4
            // 
            this.txtver4.Location = new System.Drawing.Point(115, 96);
            this.txtver4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtver4.Name = "txtver4";
            this.txtver4.Size = new System.Drawing.Size(140, 25);
            this.txtver4.TabIndex = 0;
            // 
            // txtver3
            // 
            this.txtver3.Location = new System.Drawing.Point(613, 58);
            this.txtver3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtver3.Name = "txtver3";
            this.txtver3.Size = new System.Drawing.Size(140, 25);
            this.txtver3.TabIndex = 0;
            // 
            // txtver2
            // 
            this.txtver2.Location = new System.Drawing.Point(363, 60);
            this.txtver2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtver2.Name = "txtver2";
            this.txtver2.Size = new System.Drawing.Size(140, 25);
            this.txtver2.TabIndex = 0;
            // 
            // txtver1
            // 
            this.txtver1.Location = new System.Drawing.Point(115, 62);
            this.txtver1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtver1.Name = "txtver1";
            this.txtver1.Size = new System.Drawing.Size(140, 25);
            this.txtver1.TabIndex = 0;
            // 
            // txtxinhao
            // 
            this.txtxinhao.Location = new System.Drawing.Point(613, 24);
            this.txtxinhao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtxinhao.Name = "txtxinhao";
            this.txtxinhao.Size = new System.Drawing.Size(140, 25);
            this.txtxinhao.TabIndex = 0;
            // 
            // txtnum
            // 
            this.txtnum.Location = new System.Drawing.Point(363, 26);
            this.txtnum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtnum.Name = "txtnum";
            this.txtnum.Size = new System.Drawing.Size(140, 25);
            this.txtnum.TabIndex = 0;
            // 
            // txtid
            // 
            this.txtid.Location = new System.Drawing.Point(115, 29);
            this.txtid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtid.Name = "txtid";
            this.txtid.Size = new System.Drawing.Size(140, 25);
            this.txtid.TabIndex = 0;
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(539, 332);
            this.reset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(112, 35);
            this.reset.TabIndex = 0;
            this.reset.Text = "重置";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // registe
            // 
            this.registe.Location = new System.Drawing.Point(659, 332);
            this.registe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.registe.Name = "registe";
            this.registe.Size = new System.Drawing.Size(112, 35);
            this.registe.TabIndex = 0;
            this.registe.Text = "注册";
            this.registe.UseVisualStyleBackColor = true;
            this.registe.Click += new System.EventHandler(this.registe_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(779, 332);
            this.cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(112, 35);
            this.cancel.TabIndex = 0;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // txtFac
            // 
            this.txtFac.FormattingEnabled = true;
            this.txtFac.Items.AddRange(new object[] {
            "华中数控",
            "广州数控",
            "沈阳高精",
            "航天数控"});
            this.txtFac.Location = new System.Drawing.Point(115, 25);
            this.txtFac.Name = "txtFac";
            this.txtFac.Size = new System.Drawing.Size(84, 23);
            this.txtFac.TabIndex = 2;
            this.txtFac.Text = "华中数控";
            // 
            // labnum
            // 
            this.labnum.AutoSize = true;
            this.labnum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labnum.Location = new System.Drawing.Point(573, 26);
            this.labnum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labnum.Name = "labnum";
            this.labnum.Size = new System.Drawing.Size(80, 18);
            this.labnum.TabIndex = 1;
            this.labnum.Text = "机床编号";
            // 
            // cmbnum
            // 
            this.cmbnum.FormattingEnabled = true;
            this.cmbnum.Location = new System.Drawing.Point(660, 23);
            this.cmbnum.Name = "cmbnum";
            this.cmbnum.Size = new System.Drawing.Size(62, 23);
            this.cmbnum.TabIndex = 3;
            this.cmbnum.Text = "1";
            // 
            // frmAddnc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 382);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.registe);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmAddnc";
            this.Text = "系统注册";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Test;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button registe;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtver5;
        private System.Windows.Forms.TextBox txtdt;
        private System.Windows.Forms.TextBox txtver4;
        private System.Windows.Forms.TextBox txtver3;
        private System.Windows.Forms.TextBox txtver2;
        private System.Windows.Forms.TextBox txtver1;
        private System.Windows.Forms.TextBox txtnum;
        private System.Windows.Forms.TextBox txtid;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtxinhao;
        private System.Windows.Forms.ComboBox txtFac;
        private System.Windows.Forms.ComboBox cmbnum;
        private System.Windows.Forms.Label labnum;
    }
}