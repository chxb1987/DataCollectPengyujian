namespace demo
{
    partial class NC_Err_Log
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Err_log = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Err_log
            // 
            this.Err_log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Err_log.Location = new System.Drawing.Point(0, 3);
            this.Err_log.Name = "Err_log";
            this.Err_log.Size = new System.Drawing.Size(355, 146);
            this.Err_log.TabIndex = 0;
            this.Err_log.Text = "";
            this.Err_log.MouseEnter += new System.EventHandler(this.Err_log_MouseEnter);
            this.Err_log.MouseLeave += new System.EventHandler(this.Err_log_MouseLeave);
            // 
            // NC_Err_Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Err_log);
            this.Name = "NC_Err_Log";
            this.Size = new System.Drawing.Size(360, 152);
            this.MouseEnter += new System.EventHandler(this.Err_log_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Err_log_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Err_log;
    }
}
