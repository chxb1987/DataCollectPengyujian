namespace demo
{
    partial class frmRealGraph
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
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl3 = new ZedGraph.ZedGraphControl();
            this.cbx_data1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_data2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_data3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.time_showGraph = new System.Windows.Forms.Timer(this.components);
            this.time_showGraph2 = new System.Windows.Forms.Timer(this.components);
            this.time_showGraph3 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(93, 55);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(864, 234);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Location = new System.Drawing.Point(93, 320);
            this.zedGraphControl2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 0D;
            this.zedGraphControl2.ScrollMaxY = 0D;
            this.zedGraphControl2.ScrollMaxY2 = 0D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = 0D;
            this.zedGraphControl2.ScrollMinY2 = 0D;
            this.zedGraphControl2.Size = new System.Drawing.Size(864, 225);
            this.zedGraphControl2.TabIndex = 0;
            // 
            // zedGraphControl3
            // 
            this.zedGraphControl3.Location = new System.Drawing.Point(93, 596);
            this.zedGraphControl3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGraphControl3.Name = "zedGraphControl3";
            this.zedGraphControl3.ScrollGrace = 0D;
            this.zedGraphControl3.ScrollMaxX = 0D;
            this.zedGraphControl3.ScrollMaxY = 0D;
            this.zedGraphControl3.ScrollMaxY2 = 0D;
            this.zedGraphControl3.ScrollMinX = 0D;
            this.zedGraphControl3.ScrollMinY = 0D;
            this.zedGraphControl3.ScrollMinY2 = 0D;
            this.zedGraphControl3.Size = new System.Drawing.Size(864, 225);
            this.zedGraphControl3.TabIndex = 0;
            // 
            // cbx_data1
            // 
            this.cbx_data1.FormattingEnabled = true;
            this.cbx_data1.Items.AddRange(new object[] {
            "无",
            "主轴负载",
            "X轴负载",
            "Y轴负载",
            "Z轴负载",
            "供电电压",
            "供电电流"});
            this.cbx_data1.Location = new System.Drawing.Point(160, 15);
            this.cbx_data1.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_data1.Name = "cbx_data1";
            this.cbx_data1.Size = new System.Drawing.Size(160, 23);
            this.cbx_data1.TabIndex = 1;
            this.cbx_data1.SelectedIndexChanged += new System.EventHandler(this.cbx_data1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "1数据源选择：";
            // 
            // cbx_data2
            // 
            this.cbx_data2.FormattingEnabled = true;
            this.cbx_data2.Items.AddRange(new object[] {
            "无",
            "主轴负载",
            "X轴负载",
            "Y轴负载",
            "Z轴负载",
            "供电电压",
            "供电电流"});
            this.cbx_data2.Location = new System.Drawing.Point(463, 15);
            this.cbx_data2.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_data2.Name = "cbx_data2";
            this.cbx_data2.Size = new System.Drawing.Size(160, 23);
            this.cbx_data2.TabIndex = 1;
            this.cbx_data2.SelectedIndexChanged += new System.EventHandler(this.cbx_data2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(352, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "2数据源选择：";
            // 
            // cmb_data3
            // 
            this.cmb_data3.FormattingEnabled = true;
            this.cmb_data3.Items.AddRange(new object[] {
            "无",
            "主轴负载",
            "X轴负载",
            "Y轴负载",
            "Z轴负载",
            "供电电压",
            "供电电流"});
            this.cmb_data3.Location = new System.Drawing.Point(796, 15);
            this.cmb_data3.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_data3.Name = "cmb_data3";
            this.cmb_data3.Size = new System.Drawing.Size(160, 23);
            this.cmb_data3.TabIndex = 1;
            this.cmb_data3.SelectedIndexChanged += new System.EventHandler(this.cmb_data3_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(685, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "3数据源选择：";
            // 
            // time_showGraph
            // 
            this.time_showGraph.Interval = 1000;
            this.time_showGraph.Tick += new System.EventHandler(this.time_showGraph_Tick);
            // 
            // time_showGraph2
            // 
            this.time_showGraph2.Interval = 1000;
            this.time_showGraph2.Tick += new System.EventHandler(this.time_showGraph2_Tick);
            // 
            // time_showGraph3
            // 
            this.time_showGraph3.Interval = 1000;
            this.time_showGraph3.Tick += new System.EventHandler(this.time_showGraph3_Tick);
            // 
            // frmRealGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 836);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_data3);
            this.Controls.Add(this.cbx_data2);
            this.Controls.Add(this.cbx_data1);
            this.Controls.Add(this.zedGraphControl3);
            this.Controls.Add(this.zedGraphControl2);
            this.Controls.Add(this.zedGraphControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmRealGraph";
            this.Text = "frmRealGraph";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRealGraph_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private ZedGraph.ZedGraphControl zedGraphControl3;
        private System.Windows.Forms.ComboBox cbx_data1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbx_data2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_data3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer time_showGraph;
        private System.Windows.Forms.Timer time_showGraph2;
        private System.Windows.Forms.Timer time_showGraph3;
    }
}