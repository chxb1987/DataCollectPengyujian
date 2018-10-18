using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace demo
{
    public partial class NC_Err_Log : UserControl
    {
        public NC_Err_Log()
        {
            InitializeComponent();
        }
        public struct Last_position
        {
            public int  last_x;
            public int  last_y;

        }
        Last_position positon = new Last_position();
        private void Err_log_MouseEnter(object sender, EventArgs e)
        {
            this.BringToFront();
            positon.last_x = this.Location.X;
            positon.last_y = this.Location.Y;
            if (this.Location.X + 360 > 1900)
            {
                this.Location = new Point(this.Location.X - 360 + 81, this.Location.Y);
                this.Height = 131;
                this.Width = 360;
            }
            else
            {
                this.Height = 131;
                this.Width = 360;
            }

        }

        private void Err_log_MouseLeave(object sender, EventArgs e)
        {
            this.Location = new Point(positon.last_x, positon.last_y);
            this.Height = 131;
            this.Width = 81;
        }
        public delegate void LogAppendDelegate(Color color, string text);

        public void LogAppend(Color color, string text)
        {
            this.Err_log.AppendText("\n");
            this.Err_log.SelectionColor = color;
            this.Err_log.AppendText(text);
        }
        public void NC_Add_ErrLog(demo.FormMain.AlarmStruct errlog)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
           //this.Err_time.Text = System.DateTime.Now.ToString("MM-dd HH:mm");
            this.Err_log.Invoke(la, Color.Red, DateTime.Now.ToString("HH:mm:ss") + "发生报警" + errlog.alarm_num.ToString() + errlog.alarm_text);
        }
        public void NC_Delete_ErrLog(demo.FormMain.AlarmStruct errlog)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.Err_log.Invoke(la, Color.Green, DateTime.Now.ToString("HH:mm:ss") + "报警消除" + errlog.alarm_num.ToString() + errlog.alarm_text);
        }

    }
}
