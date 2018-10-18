using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace demo
{
    public partial class StatusData_user : UserControl
    {
        public StatusData_user()
        {
            InitializeComponent();
        }
        public int NC_StatusUpdate(Real_status tb_aut)
        {
            switch (tb_aut.intComm_flag)
            {
                case 0:
                    this.Com_err.BackColor = Color.Gray;
                    break;
                case 1:
                    this.Com_err.BackColor = Color.Green;
                    break;
                case 2:
                    this.Com_err.BackColor = Color.Red;
                    break;
                default:
                    break;
            }
            switch (tb_aut.intErr_flag)
            {
                case 0:
                    this.NC_err.BackColor = Color.Gray;
                    break;
                case 1:
                    this.NC_err.BackColor = Color.Green;
                    break;
                case 2:
                    this.NC_err.BackColor = Color.Red;
                    break;
                default:
                    break;
            }
            switch (tb_aut.intPower_flag)
            {
                case 0:
                    this.NC_power.BackColor = Color.Gray;
                    break;
                case 1:
                    this.NC_power.BackColor = Color.Green;
                    break;
                default:
                    break;
            }
            switch (tb_aut.strState_nc)
            {
                case 0:
                    this.Run_status.Text = "0";
                    break;
                case 1:
                    this.Run_status.Text = "程序运行完成";
                    break;
                case 2:
                    this.Run_status.Text = "螺纹加工";
                    break;
                case 3:
                    this.Run_status.Text = "刚性攻丝";
                    break;
                case 4:
                    this.Run_status.Text = "重运行复位状态";
                    break;
                case 5:
                    this.Run_status.Text = "急停";
                    break;
                case 6:
                    this.Run_status.Text = "复位";
                    break;
                case 7:
                    this.Run_status.Text = "运行中";
                    break;
                case 8:
                    this.Run_status.Text = "回零中";
                    break;
                case 9:
                    this.Run_status.Text = "轴移动中";
                    break;
                case 10:
                    this.Run_status.Text = "空闲";
                    break;
                case 11:
                    this.Run_status.Text = "暂停";
                    break;
                default:
                    break;

            }
            this.Err_num.Text = tb_aut.intErr_num.ToString();
            this.Err_text.Text = tb_aut.strErr_txt;
            this.IP_add.Text = tb_aut.strIPv4_txt;
            this.Sys_num.Text = tb_aut.intSys_num.ToString();
          
            this.Err_time.Text = tb_aut.strErr_time;   //最近一次报警发生时间
            this.Power_time.Text = tb_aut.strPower_time;
            this.Feed_speed.Text = tb_aut.intFeed_v.ToString("0.00");
            this.Spindle_speed.Text = tb_aut.intSpindle_v.ToString("0.00");
            this.Axis_1_loc.Text = tb_aut.intAxis_1.ToString("0.000");
            this.Axis_2_loc.Text = tb_aut.intAxis_2.ToString("0.000");
            this.Axis_3_loc.Text = tb_aut.intAxis_3.ToString("0.000");

            this.Prog_gcode.Text = tb_aut.strProg_Gcode;
            this.Prog_num.Text = tb_aut.intProg_num.ToString();
            this.Prog_running.Text = tb_aut.intProg_running.ToString();
            this.Prog_coding.Text = tb_aut.intProg_decode.ToString();
            this.Curr_I_X.Text = tb_aut.intAxis_Curr_1.ToString("0.000");
            this.Curr_I_Y.Text = tb_aut.intAxis_Curr_2.ToString("0.000");
            this.Curr_I_Z.Text = tb_aut.intAxis_Curr_3.ToString("0.000");
            this.Curr_I_Spin.Text = tb_aut.intAxis_Curr_4.ToString("0.000");

            this.temper.Text = tb_aut.douTemperature.ToString("0.0");
            this.humid.Text = tb_aut.douHumidity.ToString("0.0");
            this.Votage_1.Text = tb_aut.douVoltage_1ch.ToString("0.00");
            this.Votage_2.Text = tb_aut.douVoltage_2ch.ToString("0.00");
            this.Votage_3.Text = tb_aut.douVoltage_3ch.ToString("0.00");

            //this.Err_log.Text += tb_aut.strErr_log + System.Environment.NewLine; //Err_Log需要单独写个方法进行更新

            return 1;
        }
        //public delegate void LogAppendDelegate(Color color,string text);

        //public void LogAppend(Color color,string text)
        //{
        //    this.Err_log.AppendText("\n");
        //    this.Err_log.SelectionColor=color;
        //    this.Err_log.AppendText(text);
        //}
        //public void NC_Add_ErrLog(demo.FormMain.AlarmStruct errlog )
        //{
        //    LogAppendDelegate la = new LogAppendDelegate(LogAppend);
        //    this.Err_time.Text = System.DateTime.Now.ToString("MM-dd HH:mm");
        //    this.Err_log.Invoke(la, Color.Red, DateTime.Now.ToString("HH:mm:ss") + "发生报警" + errlog.alarm_num.ToString() + errlog.alarm_text);
        //}
        //public void NC_Delete_ErrLog(demo.FormMain.AlarmStruct errlog)
        //{
        //    LogAppendDelegate la = new LogAppendDelegate(LogAppend);
        //    this.Err_log.Invoke(la, Color.Green, DateTime.Now.ToString("HH:mm:ss") + "报警消除" + errlog.alarm_num.ToString() + errlog.alarm_text);
        //}
        

        //public void NC_Add_ErrLog(demo.FormMain.AlarmStruct errlog)
        //{
        //    this.Err_log.SelectionColor = Color.Red;
        //    //this.Err_log.AppendText = System.Environment.NewLine;
        //    this.Err_log.AppendText(System.DateTime.Now.ToString("HH:mm:sss") + "报警" + errlog.alarm_num.ToString() + errlog.alarm_text);//获得系统时间，当作时间戳
        //    this.Err_log.Text += System.Environment.NewLine;
        //}

        //public void NC_Delete_ErrLog(demo.FormMain.AlarmStruct errlog)
        //{
        //    this.Err_log.SelectionColor = Color.Green;
        //    this.Err_log.AppendText(System.DateTime.Now.ToString("HH:mm:sss") + "报警消除" + errlog.alarm_num.ToString() + errlog.alarm_text);//获得系统时间，当作时间戳
        //    this.Err_log.Text += System.Environment.NewLine;

        //}
        /*用户控件中的实时状态和图表按钮不是通用型的需要在主窗体中单独绘制*/
        //private void Real_statu_Click(object sender, EventArgs e)
        //{
        //    using (NC_Status_Form status_detail = new NC_Status_Form())
        //    {
        //        status_detail.ShowDialog();

        //    }
        //}
        //private void Data_xls_Click(object sender, EventArgs e)
        //{
        //    using (Findlimit status_detail = new Findlimit())
        //    {
        //        status_detail.ShowDialog();

        //    }
        //}
        //public int FindTheLimit(Real_status tb_aut)
        //{
        //    string pathout = "f:\\FindLimit.txt";
        //    StreamWriter sw = new StreamWriter(pathout, true);
        //        sw.WriteLine(tb_aut.intAxis_1.ToString());
        //    sw.Close();
        //    sw.Dispose();
        //    return 1;
        //}
        /*测试用例*/
        public static void WriteTxt(bool append, Real_status tb_aut)
        {
            string filePathName = "f:\\FindLimit_150ms_3s_threading_test.txt";
            StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.Default);
            //foreach (var fieldInfo in typeof(Real_status).GetFields())
            //{
            //    fileWriter.WriteLine("Name:{0},Type{1}", fieldInfo.Name, fieldInfo.FieldType);
            //}
            fileWriter.WriteLine("ClientNo." + tb_aut.intSys_num.ToString() + "PC时间：" + tb_aut.dataPC_time + "   主轴转速：" + tb_aut.intSpindle_v.ToString() + "   FTP请求时间：" + tb_aut.dataPC_time_Mill.ToString() + System.Environment.NewLine);
            //fileWriter.WriteLine(tb_aut.dataPC_time);
            //fileWriter.WriteLine(tb_aut.dataPC_time_Mill.ToString());
            //fileWriter.WriteLine();
            fileWriter.Flush();
            fileWriter.Close();
        }

        private void Err_text_MouseEnter(object sender, EventArgs e)
        {
            this.Err_text.BringToFront();
            this.Err_text.Height = 102;
            this.Err_text.Width = 80;
        }

        private void Err_text_MouseLeave(object sender, EventArgs e)
        {
            this.Err_text.Height = 21;
            this.Err_text.Width = 80;

        }

        private void Prog_gcode_MouseEnter(object sender, EventArgs e)
        {
            this.Prog_gcode.BringToFront();
            this.Prog_gcode.Height = 102;
            this.Prog_gcode.Width = 80;
        }

        private void Prog_gcode_MouseLeave(object sender, EventArgs e)
        {
            this.Prog_gcode.Height = 21;
            this.Prog_gcode.Width = 80;
        }

        //private void Err_log_MouseEnter(object sender, EventArgs e)
        //{
        //    Err_log.BringToFront();
        //    Err_log.Height = 131;
        //    Err_log.Width = 320;
            
        //}

        //private void Err_log_MouseLeave(object sender, EventArgs e)
        //{
        //    Err_log.SendToBack();
        //    Err_log.Height = 131;
        //    Err_log.Width = 81;
        //}

        
      

       

       

      
    }
}
