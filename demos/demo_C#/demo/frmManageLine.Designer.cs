namespace demo
{
    partial class frmManageLine
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
            this.dtGridView_NC = new System.Windows.Forms.DataGridView();
            this.btn_LogIn = new System.Windows.Forms.Button();
            this.btn_LogOff = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.一键登入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.一键登出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridView_NC)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtGridView_NC
            // 
            this.dtGridView_NC.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dtGridView_NC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridView_NC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtGridView_NC.Location = new System.Drawing.Point(36, 38);
            this.dtGridView_NC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtGridView_NC.Name = "dtGridView_NC";
            this.dtGridView_NC.ReadOnly = true;
            this.dtGridView_NC.RowHeadersVisible = false;
            this.dtGridView_NC.RowTemplate.Height = 23;
            this.dtGridView_NC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridView_NC.Size = new System.Drawing.Size(1023, 298);
            this.dtGridView_NC.TabIndex = 0;
            // 
            // btn_LogIn
            // 
            this.btn_LogIn.Location = new System.Drawing.Point(733, 372);
            this.btn_LogIn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_LogIn.Name = "btn_LogIn";
            this.btn_LogIn.Size = new System.Drawing.Size(112, 26);
            this.btn_LogIn.TabIndex = 1;
            this.btn_LogIn.Text = "登入";
            this.btn_LogIn.UseVisualStyleBackColor = true;
            this.btn_LogIn.Click += new System.EventHandler(this.btn_LogIn_Click);
            // 
            // btn_LogOff
            // 
            this.btn_LogOff.Location = new System.Drawing.Point(853, 372);
            this.btn_LogOff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_LogOff.Name = "btn_LogOff";
            this.btn_LogOff.Size = new System.Drawing.Size(112, 26);
            this.btn_LogOff.TabIndex = 1;
            this.btn_LogOff.Text = "登出";
            this.btn_LogOff.UseVisualStyleBackColor = true;
            this.btn_LogOff.Click += new System.EventHandler(this.btn_LogOff_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(973, 372);
            this.close.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(112, 26);
            this.close.TabIndex = 1;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.一键登入ToolStripMenuItem,
            this.一键登出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1101, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 一键登入ToolStripMenuItem
            // 
            this.一键登入ToolStripMenuItem.Name = "一键登入ToolStripMenuItem";
            this.一键登入ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.一键登入ToolStripMenuItem.Text = "一键登入";
            this.一键登入ToolStripMenuItem.Click += new System.EventHandler(this.一键登入ToolStripMenuItem_Click);
            // 
            // 一键登出ToolStripMenuItem
            // 
            this.一键登出ToolStripMenuItem.Name = "一键登出ToolStripMenuItem";
            this.一键登出ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.一键登出ToolStripMenuItem.Text = "一键登出";
            this.一键登出ToolStripMenuItem.Click += new System.EventHandler(this.一键登出ToolStripMenuItem_Click);
            // 
            // frmManageLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 414);
            this.Controls.Add(this.close);
            this.Controls.Add(this.btn_LogOff);
            this.Controls.Add(this.btn_LogIn);
            this.Controls.Add(this.dtGridView_NC);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmManageLine";
            this.Text = "管理现有连接";
            this.Load += new System.EventHandler(this.manageLine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridView_NC)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtGridView_NC;
        private System.Windows.Forms.Button btn_LogIn;
        private System.Windows.Forms.Button btn_LogOff;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 一键登入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 一键登出ToolStripMenuItem;
    }
}