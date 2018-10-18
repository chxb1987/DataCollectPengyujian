namespace demo
{
    partial class frmdbPushData
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
            this.db_progressBar = new System.Windows.Forms.ProgressBar();
            this.pushdata = new System.Windows.Forms.Button();
            this.db_tblist = new System.Windows.Forms.ListView();
            this.table_list = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.line_num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size_sum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pbar_state = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // db_progressBar
            // 
            this.db_progressBar.Location = new System.Drawing.Point(48, 276);
            this.db_progressBar.Name = "db_progressBar";
            this.db_progressBar.Size = new System.Drawing.Size(266, 23);
            this.db_progressBar.TabIndex = 0;
            // 
            // pushdata
            // 
            this.pushdata.Location = new System.Drawing.Point(65, 318);
            this.pushdata.Name = "pushdata";
            this.pushdata.Size = new System.Drawing.Size(75, 23);
            this.pushdata.TabIndex = 1;
            this.pushdata.Text = "上传";
            this.pushdata.UseVisualStyleBackColor = true;
            this.pushdata.Click += new System.EventHandler(this.pushdata_Click);
            // 
            // db_tblist
            // 
            this.db_tblist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.table_list,
            this.line_num,
            this.size_sum});
            this.db_tblist.GridLines = true;
            this.db_tblist.Location = new System.Drawing.Point(48, 51);
            this.db_tblist.Name = "db_tblist";
            this.db_tblist.Size = new System.Drawing.Size(283, 188);
            this.db_tblist.TabIndex = 2;
            this.db_tblist.UseCompatibleStateImageBehavior = false;
            this.db_tblist.View = System.Windows.Forms.View.Details;
            // 
            // table_list
            // 
            this.table_list.Text = "数据表";
            this.table_list.Width = 73;
            // 
            // line_num
            // 
            this.line_num.Text = "数据数";
            this.line_num.Width = 78;
            // 
            // size_sum
            // 
            this.size_sum.Text = "存储量";
            this.size_sum.Width = 128;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "上传进度条";
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(214, 318);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "数据库容量信息";
            // 
            // pbar_state
            // 
            this.pbar_state.AutoSize = true;
            this.pbar_state.BackColor = System.Drawing.SystemColors.Control;
            this.pbar_state.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pbar_state.Location = new System.Drawing.Point(123, 250);
            this.pbar_state.Name = "pbar_state";
            this.pbar_state.Size = new System.Drawing.Size(0, 14);
            this.pbar_state.TabIndex = 4;
            // 
            // frmdbPushData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 351);
            this.Controls.Add(this.pbar_state);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.db_tblist);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.pushdata);
            this.Controls.Add(this.db_progressBar);
            this.Name = "frmdbPushData";
            this.Text = "数据库数据上传窗口";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar db_progressBar;
        private System.Windows.Forms.Button pushdata;
        private System.Windows.Forms.ListView db_tblist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader table_list;
        private System.Windows.Forms.ColumnHeader line_num;
        private System.Windows.Forms.ColumnHeader size_sum;
        private System.Windows.Forms.Label pbar_state;
    }
}