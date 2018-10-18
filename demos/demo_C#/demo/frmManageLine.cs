
#define MARCO_DB
//#undef MARCO_DB

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace demo
{
    public partial class frmManageLine : Form
    {
        public frmManageLine()
        {
            InitializeComponent();
        }

        private void manageLine_Load(object sender, EventArgs e)
        {
            SetNCData();
        }
        DataSet ds = new DataSet();
        private void SetNCData()
        {
            try
            {
                DataBase addnc = new DataBase();
                MySqlConnection sqlcon = new MySqlConnection(addnc.M_str_sqlcon);
                sqlcon.Open();
                string select = "select * from tb_nc ";
                MySqlDataAdapter da = new MySqlDataAdapter(select, sqlcon);
                da.Fill(ds);
                dtGridView_NC.DataSource = ds.Tables[0];
                for (int i = 6; i < ds.Tables[0].Columns.Count; i++)
                    dtGridView_NC.Columns[i].Visible = false;
                sqlcon.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("连接数据库失败");
            }
            finally 
            {
               
            }
            
        }
        /*登陆系统，发送相关信息*/
        private void LogIn_Enqueue(Int16 num)
        {
            string updatecom = string.Empty;
            
            //string NC_ID = dtGridView_NC.SelectedCells[Constants.TB_ID].Value.ToString();  //id在数据tb_nc的第三列
            string NC_ID = ds.Tables[0].Rows[num-1][2].ToString();
            long temp = 0;
            Data_LogIn_info login_info = new Data_LogIn_info();
#if (MARCO_DEBUG)
            int ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_SN_NUM, ref NC_ID, clientno);
            if (ret32 == 0)
            {
                // value64 = value64 / Move_unit * 1000 * 60;
                login_info.strNC_ID = NC_ID;
            }
            else
            {
                MessageBox.Show("登入失败");
            }
#else
            login_info.id = NC_ID;
#endif
            if (CGlbFunc.Power_time_today[num] != null)
            {
                TimeSpan span = DateTime.Now - DateTime.Parse(CGlbFunc.Power_time_today[num]);
                //读取数据库的历史值
                string comm = "select id,ontime from tb_login_local as t where not exists(select 1 from tb_login_local where id=t.id and dt>t.dt )and id='" + NC_ID + "'";
                if (CGlbFunc.LoginFlag[num] == Constants.LOGINFLAG)
                {
                    updatecom = "update tb_nc set state= '已登录' where ID_selfinc='" + num + "'";
                }
                if (CGlbFunc.LoginFlag[num] == Constants.LOGOFFLAG)
                {
                    updatecom = "update tb_nc set state= '未登录' where ID_selfinc='" + num + "'";
                }

                MySqlDataReader reader = null;
                MySqlConnection sqlcon = new MySqlConnection(FormMain.MyDataBase.M_str_sqlcon);
                MySqlCommand cmd2 = new MySqlCommand(updatecom, sqlcon);

                try
                {
                    sqlcon.Open();
                    MySqlCommand cmd = new MySqlCommand(comm, sqlcon);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        //Console.WriteLine(string.Format("{0},{1}",reader[0],reader[1]));
                        temp = (long)reader[1];
                    }
                    else
                    {
                        temp = 0;
                    }
                    reader.Close();
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("连接数据库失败");
                }
                finally
                {
                    cmd2.Dispose();
                    //sqlcon.Close();
                }
                login_info.runtime = temp + (long)span.TotalSeconds;
                login_info.ontime = login_info.runtime;
                login_info.time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //时间戳，实验室环境下可以用做开机时间参考
                #region
                //本地存储
                string insertcom = "insert into tb_login_local(id,ontime,runtime,dt) values('" + login_info.id + "','" + login_info.ontime + "','" + login_info.runtime + "','" + login_info.time + "')";
                MySqlCommand cmd3 = new MySqlCommand(insertcom, sqlcon);
                cmd3.ExecuteNonQuery();
                cmd3.Dispose();
                sqlcon.Close();
                #endregion
                //SetNCData();

                FormMain.queue_login.Enqueue(login_info);
            }
            else
            {
                MessageBox.Show("系统未登录");
            }
        }

        private void LogIn_Hnc(short rowinput,string ipinput)
        {
            string ip = ipinput;
            ushort port = 21;
            short ActiveClientNo = HncApi.HNC_NetConnect(ip, port);
            if (ActiveClientNo >= 0)
            {
                Console.WriteLine("HZ Connect successed");
                CGlbFunc.LoginFlag[rowinput] = Constants.LOGINFLAG; //登陆标志位
                CGlbFunc.clientNo[rowinput] = ActiveClientNo;
                CGlbFunc.ip_info[rowinput] = ip;
                CGlbFunc.port_info[rowinput] =port;    //连接成功存储相关信息到GLOBLE变量
                CGlbFunc.Power_time_today[rowinput] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff"); //存储每次开机时间用于显示
#if (MARCO_DB)
                LogIn_Enqueue(rowinput);
#endif

            }
            else
            {
                MessageBox.Show("连接失败，请检查后重试！");  //华中数控连接成功会返回一个大于等于0的clientno
            }
        }
        private void LogIn_Ht(short rowinput, string ipinput)
        {
            string ip = ipinput;
            ushort pt = 6665;
            string port = pt.ToString();
            try
            {
                //为什么要系统编号，是将ip和端口号与编号相互绑定，之后通过编号就能进行连接断开重连的操作？？
                int res = HtApi.ClientConnectServer(rowinput - 15, ip, port);
                if (res == 0)
                {
                    Console.WriteLine("航天数控连接成功");
                    CGlbFunc.LoginFlag[rowinput] = Constants.LOGINFLAG; //登陆标志位
                    //CGlbFunc.clientNo[rowinput] = ActiveClientNo;
                    CGlbFunc.ip_info[rowinput] = ip;
                    CGlbFunc.port_info[rowinput] = pt;   //连接成功存储相关信息到GLOBLE变量
                    CGlbFunc.Power_time_today[rowinput] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff"); //存储每次开机时间用于显示
#if (MARCO_DB)
                    LogIn_Enqueue(rowinput);
#endif

                }
                else
                {
                    MessageBox.Show("连接失败，请检查后重试！");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("连接失败");
            }

        }

        private void LogIn_Gsk(short rowinput, string ipinput)
        {
            IPAddress addr = IPAddress.Parse(ipinput);
            byte[] ip_by = addr.GetAddressBytes();
            //Byte[] ipadd = System.Text.Encoding.Default.GetBytes("192.168.188.121");
            IntPtr hInst = GskApi.GSKRM_CreateInstance(ref ip_by[0], 1);
            if (hInst==IntPtr.Zero)
            {
                Console.WriteLine("Connect failed");
                MessageBox.Show("广州数控"+(rowinput-9).ToString()+" 号机，连接失败，请检查后重试！");  
            }
            else
            {
                Console.WriteLine("GSK Connect successed");
                CGlbFunc.LoginFlag[rowinput] = Constants.LOGINFLAG;
                CGlbFunc.gsktock[rowinput] = hInst;
                CGlbFunc.ip_info[rowinput] = ipinput;
                CGlbFunc.id_info[rowinput] = dtGridView_NC.SelectedCells[Constants.TB_ID].Value.ToString();
                                                         //连接成功存储相关信息到GLOBLE变量
                CGlbFunc.Power_time_today[rowinput] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //存储每次开机时间用于显示
#if (MARCO_DB)
                    LogIn_Enqueue(rowinput);
#endif
            }
            
        }

        private void LogIn_Gj(short rowinput, string ipinput)
         {
             char[] ip = ipinput.ToCharArray();
             ushort port = 5005;
             int ActiveClientNo = -1;
             Int32 retval = GjApi.connect430ToNC(ref ActiveClientNo, ip, port);
             if (retval == 0)
             {
                 Console.WriteLine("HZ Connect successed");
                 CGlbFunc.LoginFlag[rowinput] = Constants.LOGINFLAG; //登陆标志位
                 CGlbFunc.clientNo[rowinput] = (short)ActiveClientNo;
                 CGlbFunc.ip_info[rowinput] = ipinput;
                 CGlbFunc.port_info[rowinput] = port;    //连接成功存储相关信息到GLOBLE变量
                 CGlbFunc.Power_time_today[rowinput] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //存储每次开机时间用于显示
#if (MARCO_DB)
                 LogIn_Enqueue(rowinput);
#endif

             }
             else
             {
                 MessageBox.Show("连接失败，请检查后重试！");  //申请存储空间nmlServerArray失败返回值是-1，无地址或端口返回值是-2，“不存在编号为**的机器”时函数返回值是-3，“与NC建立连接时出错”时函数返回值是-,4,执行正常则函数返回值是0。
             }
         }

        private void btn_LogIn_Click(object sender, EventArgs e)
        {
            string ipslc = "";
            short rownum = 0;
            if (MessageBox.Show("确定登入吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (dtGridView_NC.SelectedCells[5].Value.ToString() == "华中数控")
                {
                    ipslc = dtGridView_NC.SelectedCells[Constants.TB_IP].Value.ToString();//ip位于第4列
                    rownum = Convert.ToInt16(dtGridView_NC.SelectedCells[Constants.TB_ROWNUM].Value);
                    LogIn_Hnc(rownum,ipslc);
                
                }
                if (dtGridView_NC.SelectedCells[5].Value.ToString() == "广州数控")
                {
                    ipslc = dtGridView_NC.SelectedCells[Constants.TB_IP].Value.ToString();//ip位于第4列
                    rownum = Convert.ToInt16(dtGridView_NC.SelectedCells[Constants.TB_ROWNUM].Value);
                    LogIn_Gsk(rownum, ipslc);
                   
                }
                if (dtGridView_NC.SelectedCells[5].Value.ToString() == "沈阳高精")
                {
                    ipslc = dtGridView_NC.SelectedCells[Constants.TB_IP].Value.ToString();//ip位于第4列
                    rownum = Convert.ToInt16(dtGridView_NC.SelectedCells[Constants.TB_ROWNUM].Value);
                    LogIn_Gj(rownum, ipslc);

                }
                if (dtGridView_NC.SelectedCells[5].Value.ToString() == "航天数控")
                {
                    ipslc = dtGridView_NC.SelectedCells[Constants.TB_IP].Value.ToString();//ip位于第4列
                    rownum = Convert.ToInt16(dtGridView_NC.SelectedCells[Constants.TB_ROWNUM].Value);
                    LogIn_Ht(rownum, ipslc);
                }
            }
        }


        private void btn_LogOff_Click(object sender, EventArgs e)
        {
            string ipslc = "";
            short rownum = 0;
            if (MessageBox.Show("确定登出吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (dtGridView_NC.SelectedCells[Constants.TB_FCTR].Value.ToString() == "华中数控")
                {
                    ipslc = dtGridView_NC.SelectedCells[Constants.TB_IP].Value.ToString();//ip位于第4列
                    rownum = Convert.ToInt16(dtGridView_NC.SelectedCells[Constants.TB_ROWNUM].Value);
                    LogOff_Hnc(rownum, ipslc);

                }
                if (dtGridView_NC.SelectedCells[Constants.TB_FCTR].Value.ToString() == "广州数控")
                {
                    ipslc = dtGridView_NC.SelectedCells[Constants.TB_IP].Value.ToString();//ip位于第4列
                    rownum = Convert.ToInt16(dtGridView_NC.SelectedCells[Constants.TB_ROWNUM].Value);
                    LogOff_Gsk(rownum, ipslc);

                }
                if (dtGridView_NC.SelectedCells[Constants.TB_FCTR].Value.ToString() == "沈阳高精")
                {
                    ipslc = dtGridView_NC.SelectedCells[Constants.TB_IP].Value.ToString();//ip位于第4列
                    rownum = Convert.ToInt16(dtGridView_NC.SelectedCells[Constants.TB_ROWNUM].Value);
                    LogOff_Gj(rownum, ipslc);

                }
            }

        }
        private void LogOff_Hnc(short rowinput, string ipinput)
        {
            string ip = ipinput;
            CGlbFunc.LoginFlag[rowinput] = Constants.LOGOFFLAG; //登出标志位
            CGlbFunc.clientNo[rowinput] = -1;                   //表示不在线
            CGlbFunc.ip_info[rowinput] = ip;
           
#if (MARCO_DB)
                LogIn_Enqueue(rowinput);
#endif
        }

        private void LogOff_Gsk(short rowinput, string ipinput)
        {
            GskApi.GSKRM_CloseInstance(CGlbFunc.gsktock[rowinput]);//关闭一个实例，没有返回值
            CGlbFunc.LoginFlag[rowinput] = Constants.LOGOFFLAG;
            CGlbFunc.gsktock[rowinput] = IntPtr.Zero;
            CGlbFunc.ip_info[rowinput] = ipinput;
            //连接成功存储相关信息到GLOBLE变量
#if (MARCO_DB)
                LogIn_Enqueue(rowinput);
#endif
        }
        private void LogOff_Ht(short rowinput, string ipinput)
        {
            HtApi.DeleteServer(rowinput-14);
            GjApi.disconnect430ToNC(CGlbFunc.clientNo[rowinput]);//关闭一个实例，没有返回值
            CGlbFunc.LoginFlag[rowinput] = Constants.LOGOFFLAG;
            //CGlbFunc.clientNo[rowinput] = -1;
            CGlbFunc.ip_info[rowinput] = ipinput;
            //连接成功存储相关信息到GLOBLE变量
#if (MARCO_DB)
            LogIn_Enqueue(rowinput);
#endif
        }

        private void LogOff_Gj(short rowinput, string ipinput)
        {
             GjApi.disconnect430ToNC(CGlbFunc.clientNo[rowinput]);//关闭一个实例，没有返回值
             CGlbFunc.LoginFlag[rowinput] = Constants.LOGOFFLAG;
             CGlbFunc.clientNo[rowinput] = -1;
             CGlbFunc.ip_info[rowinput] = ipinput;
            //连接成功存储相关信息到GLOBLE变量
#if (MARCO_DB)
                LogIn_Enqueue(rowinput);
#endif

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public  void login_all()
        {
            string ipslc = "";
            short rownum = 0;
            //华中
            for (int i = 0; i < 5; i++)
            {
                ipslc = ds.Tables[0].Rows[i][3].ToString();
                rownum = Convert.ToInt16(i + 1);
                LogIn_Hnc(rownum, ipslc);
            }
            //广州数控
            for (int i = 5; i < 10; i++)
            {
                ipslc = ds.Tables[0].Rows[i][3].ToString();
                rownum = Convert.ToInt16(i + 1);
                LogIn_Gsk(rownum, ipslc);
            }
            //高精
            for (int i = 10; i < 15; i++)
            {
                ipslc = ds.Tables[0].Rows[i][3].ToString();
                rownum = Convert.ToInt16(i + 1);
                LogIn_Gj(rownum, ipslc);
            }
            //航天
            for (int i = 15; i < 20; i++)
            {
                ipslc = ds.Tables[0].Rows[i][3].ToString();
                rownum = Convert.ToInt16(i + 1);
                LogIn_Ht(rownum, ipslc);
            }
        }
        public  void logout_all()
        {
            string ipslc = "";
            short rownum = 0;
            //华中
            for (int i = 0; i < 5; i++)
            {
                ipslc = ds.Tables[0].Rows[i][3].ToString();
                rownum = Convert.ToInt16(i + 1);
                LogOff_Hnc(rownum, ipslc);
            }
            //广州数控
            for (int i = 5; i < 10; i++)
            {
                ipslc = ds.Tables[0].Rows[i][3].ToString();
                rownum = Convert.ToInt16(i + 1);
                LogOff_Gsk(rownum, ipslc);
            }
            //高精
            for (int i = 10; i < 15; i++)
            {
                ipslc = ds.Tables[0].Rows[i][3].ToString();
                rownum = Convert.ToInt16(i + 1);
                LogOff_Gj(rownum, ipslc);
            }
            //航天
            for (int i = 15; i < 20; i++)
            {
                ipslc = ds.Tables[0].Rows[i][3].ToString();
                rownum = Convert.ToInt16(i + 1);
                LogOff_Ht(rownum, ipslc);
            }
        }
        private void 一键登入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMain.flag = 1;
            login_all();
        }

        private void 一键登出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMain.flag = 0;
            logout_all();
        }
    }
}
