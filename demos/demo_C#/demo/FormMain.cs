/*条件编译                                           */
/*打开TWINCAT获取数据线程                             */
/*因为环境数据包含有电压信号，所以需要按1秒的周期采集上传*/
#define MARCO_TWINCAT   
//#undef  MARCO_TWINCAT
/*关闭事件线程                                        */
#define MARCO_THREAD
#undef  MARCO_THREAD   
/*关闭向WEB服务器推数据的线程                          */
#define MARCO_POSTWEB
//#undef  MARCO_POSTWEB 
/*打开数据库存储线程                                   */
#define MARCO_DB     
#undef MARCO_DB
/*是否处于调试状态*/
#define MARCO_DEBUG
#undef MARCO_DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;   //支持JsonConvert，JArray,JObject,JValue,Jproperty
using MySql.Data.MySqlClient;
/*twincat ads library*/
using TwinCAT.Ads;
using demo.datastruct;

using System.Collections.Concurrent;//安全队列


namespace demo
{
    public partial class FormMain : Form
    {
        public static DataBase MyDataBase = new DataBase();
        private Int16 ActiveClientNo = 0;
        private String ip = "115.156.144.37";
        private UInt16 port = 21;
        //调用事件必须的结构体
        public struct SExtraEvent   
        {
            public Int16 clientNo;
            public Int16 chNo;
        }

        /* 建立 线性数组结构存储 用于更新Err_Log*/
        public struct AlarmStruct
        {
            public string ID;  
            public string alarm_num;  //可能报警号中包含轴的信息（广数）
            public string alarm_text;

        }
        /*因为无法使用 List定义数组，所以需要建立20个 List存储历史报警记录*/
        public static List<AlarmStruct> CurrentAlarm = new List<AlarmStruct>();  //华中的报警数据结构
        public static List<AlarmStruct> HistoryAlarm_1 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_2 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_3 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_4 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_5 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_6 = new List<AlarmStruct>(); //广州数控的报警数据结构
        public static List<AlarmStruct> HistoryAlarm_7 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_8 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_9 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_10 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_11 = new List<AlarmStruct>(); //沈阳高精数控的报警数据结构
        public static List<AlarmStruct> HistoryAlarm_12 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_13 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_14 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_15 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_16 = new List<AlarmStruct>(); //航天数控的报警数据结构
        public static List<AlarmStruct> HistoryAlarm_17 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_18 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_19 = new List<AlarmStruct>();
        public static List<AlarmStruct> HistoryAlarm_20 = new List<AlarmStruct>();

        /*建立各类发送数据的队列*/
        public static ConcurrentQueue<Data_LogIn_info> queue_login = new ConcurrentQueue<Data_LogIn_info>();
        public static ConcurrentQueue<Data_Identity_info> queue_id = new ConcurrentQueue<Data_Identity_info>(); //注册队列暂时没有使用，当与web服务器断连的时候，无法注册
        public static ConcurrentQueue<Data_Err_info> queue_err = new ConcurrentQueue<Data_Err_info>();
        public static ConcurrentQueue<Data_Run_info> queue_state = new ConcurrentQueue<Data_Run_info>();
        public static ConcurrentQueue<Data_Env_info> queue_env = new ConcurrentQueue<Data_Env_info>();

        /*界面显示用结构体*/
        Real_status real_data = new Real_status(); //定义全局变量实现real_data的多次写入(DNC twincat分别写入)

        /*运行数据 结构体*/
        private static Data_LogIn_info login_info = new Data_LogIn_info();
        Data_Identity_info identity_info = new Data_Identity_info();
        Data_Err_info err_info = new Data_Err_info();
        Data_Env_info env_info = new Data_Env_info();
        Data_json data_info = new Data_json();


        /*使用第二个线程轮循事件接口*/
        [DllImport("kernel32")]
        static extern uint GetTickCount();

        /*  调用事件函数 */
        private void ThreadProc_event()  //按键实现了事件的查询，这是一个阻塞接口，在网络中没有事件时，会一直等待，直到返回一个事件。
        {                                 //所以必须采用多线程编程实现
            SEventElement ev = new SEventElement();             //C# 里面结构体的使用，需要new来实例化一个
            while (true)
            {
                Int32 ret = HncApi.HNC_EventGetSysEv(ref ev);
                if (ret == 0)   //获取成功
                {
                    byte[] bytes = Array.ConvertAll(ev.buf, (a) => (byte)a);  //C# 实现Sbyte[](有符号-128-127)到byte[]（无符号0-255）的转换
                    //byte[] bytes = ev.buf.Cast<byte>().ToArray();
                    SExtraEvent structure = new SExtraEvent();
                    int size = Marshal.SizeOf(typeof(SExtraEvent));
                    IntPtr allocIntPtr = Marshal.AllocHGlobal(size);          //分配内存，返回一个指针
                    try
                    {
                        Marshal.Copy(bytes, 0, allocIntPtr, size);
                        structure = (SExtraEvent)Marshal.PtrToStructure(allocIntPtr, typeof(SExtraEvent));
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(allocIntPtr);                //释放申请的内存
                    }
                    //textBox3.Text += ("编号为 " + structure.clientNo.ToString() + " 的系统" + " 事件源：" + ev.src.ToString("x") + "   事件Code：" + ev.code.ToString("x") +  System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff")+System.Environment.NewLine);
                    string temp = "编号为 " + structure.clientNo.ToString() + " 的系统" + "  事件源：" + ev.src.ToString("x") + "   事件Code：" + ev.code.ToString("x")+System.DateTime.Now.ToString("   yyyy-MM-dd HH:mm:sss.ffff   ");
                    WriteTxt_test(temp);
                }
                //else            //获取失败
                //{
                //    textBox3.Text += ("获取当前事件失败，请检查" +System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff") + System.Environment.NewLine);

                //}
                Thread.Sleep(1000);
            }
        }
        /*事件写入txt */
        public void WriteTxt_test(string temp)
        {
            bool append = true;
            string filePathName = "f:\\threading_test_2.txt";
            StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.Default);
          
            fileWriter.WriteLine(temp + System.Environment.NewLine);
           
            fileWriter.Flush();
            fileWriter.Close();
        }
        private static TwincatGet ads = new TwincatGet();
        /*主窗体函数*/
        public FormMain()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            int rec = -1;
            InitializeComponent();
            /*主界面下方图片的显示*/
            pictureBox3.Image = demo.Properties.Resources.buaa;
            pictureBox2.Image = demo.Properties.Resources.huazhong;
            pictureBox1.Image = demo.Properties.Resources.guangshu;
            pictureBox4.Image = demo.Properties.Resources.lantian;
            pictureBox5.Image = demo.Properties.Resources.hangtian_2;
#if (MARCO_DB)
            try
            {
                MyDataBase.con_open();

            }
            catch
            {
                MessageBox.Show("数据库连接失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
#endif
#if (MARCO_TWINCAT)
            TwincatGet.TwinCAT_ADS_LOAD();    //与ethercat网卡建立连接
            if (ads.GetSmokeFlag())
                this.btn_smoke.BackColor=Color.Green;
            else
                this.btn_smoke.BackColor = Color.Red;

#endif
            GetLocalIpAddr(ref ip);
            rec = HncApi.HNC_NetInit(ip, port);
            if (rec != 0)
            {
                MessageBox.Show("初始化失败");
                /*初始化失败*/
            }
            else 
            {
#if (MARCO_THREAD)
                Thread t = new Thread(new ThreadStart(ThreadProc_event));     //20161220网络初始化的时候开始另一个进程采集事件，可以注释掉
                t.Start();
                Thread.Sleep(0);
#endif
            }
          /*系统刚启动时,tb_nc登录状态清除*/
            MyDataBase.con_open();
            string updatecom = "update tb_nc SET state='未登录'";
            MyDataBase.Update(MyDataBase.My_Conn, updatecom);
            MyDataBase.con_close();
        }
        /*获取本地计算机的IP地址表，并选取正确的IP，现在DNC网络使用的是192.168.188.1网段，EtherCAT使用另一个192.168网段*/
        public static void GetLocalIpAddr(ref string localIP)
        {
            string hostName = Dns.GetHostName();
            IPHostEntry localHost = Dns.GetHostEntry(hostName);
            IPAddress localIpAddr = null;
            //localIpAddr = localHost.AddressList[1]; // AddressList[0]:IPV6    AddressList[1]:IPV4
            foreach (IPAddress ip in localHost.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && ip.GetAddressBytes()[0] == 192 && ip.GetAddressBytes()[2]==188)
                {
                    localIpAddr = ip;
                    break;
                }
            }
            localIP = localIpAddr.ToString();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
#if (MARCO_DEBUG)
            Console.WriteLine("\t\n退出登录");
#else
            MyDataBase.con_open();
            string updatecom = "update tb_nc SET state='未登录'";
            MyDataBase.Update(MyDataBase.My_Conn,updatecom);
            MyDataBase.con_close();
#endif
            HncApi.HNC_NetExit();        //网络退出
            Process.GetCurrentProcess().Kill();
            System.Threading.Thread.CurrentThread.Abort();
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
#if(MARCO_TWINCAT)
            TwincatGet.tcclient.Dispose();
#endif
   
        }

        
        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            
        }
        /*登陆系统，发送相关信息*/
        public void LogInFunction(Int16 clientno)
        {
            string NC_ID = "";
            long temp = 0;
            int ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_SN_NUM, ref NC_ID, clientno);
            if (ret32 == 0)
            {
                // value64 = value64 / Move_unit * 1000 * 60;
                login_info.id = NC_ID;
            }
            else 
            {
                MessageBox.Show("登录失败");
            }
            TimeSpan span = DateTime.Now - Convert.ToDateTime(CGlbFunc.Power_time_today[clientno]);
            /*读取数据库的历史值，找到某个ID的系统最新累计开机时间*/
            string comm = "select ontime from tb_login as t where not exists(select 1 from tb_login where id=t.id and dt>t.dt) and id='"+NC_ID+"'";
            MySqlDataReader reader = null;
            MySqlConnection sqlcon = new MySqlConnection(MyDataBase.M_str_sqlcon);
            try
            {
                sqlcon.Open();
                MySqlCommand cmd = new MySqlCommand(comm, sqlcon);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    temp = long.Parse((string)reader[0]);
                }
                else
                {
                    temp = 0;
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                reader.Close();
                sqlcon.Close();
            }
            login_info.ontime = temp + (long)span.TotalSeconds;
            login_info.runtime = login_info.ontime;
            login_info.time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm：ss"); //时间戳，实验室环境下可以用做开机时间参考
           
            //支持统计单次开机时间
            queue_login.Enqueue(login_info);
        }
        /*登陆系统，发送相关信息*/
        public static void LogOffFunction(Int16 clientno)
        {
            string NC_ID = "";
            long temp = 0;
            int ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_SN_NUM, ref NC_ID, clientno);
            if (ret32 == 0)
            {
                // value64 = value64 / Move_unit * 1000 * 60;
                login_info.id= NC_ID;
            }
            else
            {
                MessageBox.Show("登出失败");
            }
            TimeSpan span = DateTime.Now - DateTime.Parse(CGlbFunc.Power_time_today[clientno]);
            //读取数据库的历史值
            string comm = "select ontime from tb_login as t where not exists(select 1 from tb_login where id=t.id and dt>t.dt and '"+NC_ID+"')";
            MySqlDataReader reader = null;
            MySqlConnection sqlcon = new MySqlConnection(MyDataBase.M_str_sqlcon);
            try
            {
                sqlcon.Open();
                MySqlCommand cmd = new MySqlCommand(comm, sqlcon);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    temp = long.Parse((string)reader[0]);
                }
                else
                {
                    temp = 0;
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                reader.Close();
                sqlcon.Close();
            }
            login_info.ontime = temp + (long)span.TotalSeconds;
            login_info.runtime = login_info.ontime;
            login_info.time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //时间戳，实验室环境下可以用做开机时间参考

            //支持统计单次开机时间 
            queue_login.Enqueue(login_info);
        }

        private void 建立新连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (IpEnter ipaddress = new IpEnter())
            {
                if (ipaddress.ShowDialog() == DialogResult.OK)
                {
                    ip = IpEnter.ip;
                    port = IpEnter.port;
                    ActiveClientNo = HncApi.HNC_NetConnect(IpEnter.ip, IpEnter.port);
                    if (ActiveClientNo >= 0)
                    {
                        CGlbFunc.clientNo[CGlbFunc.SumOfClient] = ActiveClientNo;
                        CGlbFunc.ip_info[ActiveClientNo] = IpEnter.ip;
                        CGlbFunc.port_info[ActiveClientNo] = IpEnter.port;    //连接成功存储相关信息到GLOBLE变量
                        CGlbFunc.Power_time_today[ActiveClientNo] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //存储每次开机时间用于显示
                        CGlbFunc.SumOfClient++;
#if MARCO_DB
                        LogOffFunction(ActiveClientNo);
#endif
                        timer_update.Enabled = true;  //开数据采集定时器,1秒轮询一次
                        
#if MARCO_POSTWEB
                        timer_pushdata.Enabled = true;     
                        //开启发送定时器，20ms发送一次
#else 
#if MARCO_DB
                        timer_DBsave.Enabled = true;  //开启DBsave定时器，20毫秒轮询一遍队列
#endif
#endif
                        Selc_equi_comboBox1.Items.Add("NC" + CGlbFunc.SumOfClient);               //下来选择菜单添加NC1,NC2，以后可以通过数据库检索进行优化
                        
                    }
                    else
                    {
                        MessageBox.Show("连接失败，请检查后重试！");  //华中数控连接成功会返回一个大于等于0的clientno
                    }
                }
            }
            
        }

       
        private void 查看已有连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Conected_Line inf = new Conected_Line())
            {
                inf.ShowDialog();
            }
            
        }


      /* 更新全部报警显示模块*/
        private void RegRefresh_ALL(short clientnum)
        {
            int alarmID = 0;
            int index = 0;      //index取值范围为 0~curAlarmNum-1，curAlarmNum是当前报警个数
            string txt = string.Empty;
            int curAlarmNum = 0;   //当前报警个数
            int curMsgNum = 0;  //当前提示个数
            HncApi.HNC_AlarmGetNum((int)AlarmType.ALARM_TYPE_ALL,  //获取所有类型的报警
                                   (int)AlarmLevel.ALARM_ERR,               //获取err和msg提示
                                   ref curAlarmNum, clientnum);
            HncApi.HNC_AlarmGetNum((int)AlarmType.ALARM_TYPE_ALL,  //获取所有类型的报警
                                   (int)AlarmLevel.ALARM_MSG,               //获取err和msg提示
                                   ref curMsgNum, clientnum);


            //if (curAlarmNum == 0 && curMsgNum == 0)
            //{
            //    textBox1.Text = txt.ToString();
            //}
            if (curAlarmNum != 0)
            {
                Err_Log_All.Text += "编号为" + clientnum.ToString() + "的数控系统" + System.Environment.NewLine;
               
                for (index = 0; index < curAlarmNum; index++)
                {
                    int ret = HncApi.HNC_AlarmGetData((int)AlarmType.ALARM_TYPE_ALL,  //获取所有类型的报警
                                                    (int)AlarmLevel.ALARM_ERR, //仅获取error
                                                    index,
                                                    ref alarmID,     //获取此报警的唯一ID，可用于报警识别
                                                    ref txt,             //报警文本
                                                   clientnum);
                  
                    if (ret == 0)   //获取成功
                    {
                        Err_Log_All.Text += ("当前报警ID：" + alarmID.ToString() + "   报警内容：" + txt.ToString() + System.Environment.NewLine);
                       
                    }
                    else            //获取失败
                    {
                        Err_Log_All.Text += ("获取当前报警失败，请检查" + System.Environment.NewLine);
                        
                    }
                }
            }
            if (curMsgNum != 0)
            {
                Warning_Log_All.Text += "编号为" + clientnum.ToString() + "的数控系统" + System.Environment.NewLine;

                for (index = 0; index < curMsgNum; index++)
                {
                    int ret = HncApi.HNC_AlarmGetData((int)AlarmType.ALARM_TYPE_ALL,  //获取所有类型的报警
                                                    (int)AlarmLevel.ALARM_MSG, //仅获取msg
                                                    index,
                                                    ref alarmID,     //获取此报警的唯一ID，可用于报警识别
                                                    ref txt,             //报警文本
                                                    clientnum);
                   
                    if (ret == 0)   //获取成功
                    {
                        Warning_Log_All.Text += ("当前提示ID：" + alarmID.ToString() + "   提示内容：" + txt.ToString() + System.Environment.NewLine);

                    }
                    else            //获取失败
                    {
                        Warning_Log_All.Text += ("获取当前提示失败，请检查" + System.Environment.NewLine);

                    }
                }
            }
            
        }
        /*华中数控获取数据函数*/
        /*real status 按照输入的clientno更新显示*/
        private void Real_status_hz(Int32 num)
        {

           Data_Run_info run_info = new Data_Run_info();
           Int32 alarmID = 0;
           Int32 index = 0;      //index取值范围为 0~num-1，num是当前报警个数
           String txt = String.Empty;
           String NC_ID = ""; 
           int curAlarmNum = 0;   //当前报警个数
           Int32 Move_unit = 0;
           Int32 Turn_unit = 0;
           Int32 ret32 = 0;
           Int32 value = 0;
           Double value64 = 0;
           float value_flo = 0;
           UInt32 timestart;
           UInt32 stoptime;
           short clientno = CGlbFunc.clientNo[num];

           ClearRealData();
           timestart = GetTickCount();
           real_data.strErr_log = " ";       //每次调用Real_Status时清除strErr_Log
           real_data.strIPv4_txt = CGlbFunc.ip_info[num].Remove(0,4); //为了显示关键信息，将192.删除
           real_data.intSys_num = num;
           ret32 = HncApi.HNC_NetIsConnect(clientno);
           if (ret32 == 0)
           {
               real_data.intPower_flag = 1;
               real_data.intComm_flag = 1;
           }
           else
           {
               real_data.intPower_flag = 2;
               real_data.intComm_flag = 2;
           }
            /*华中断线重连*/
           if (real_data.intComm_flag == 2)
           {
               string ip = CGlbFunc.ip_info[num];
               ushort port = 21;
               short ActiveClientNo = HncApi.HNC_NetConnect(ip, port);
               if (ActiveClientNo >= 0)
               {
                   Console.WriteLine("HZ Connect successed");
                   
                   CGlbFunc.clientNo[num] = ActiveClientNo;
               }
               else
               {
                   Console.WriteLine("连接失败，请检查后重试！");   //华中数控连接成功会返回一个大于等于0的clientno
                   return;
               }
 
           }
           //ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_TYPE, Constants.AXIS_LOGIC_NUM_X, ref axisType, CGlbFunc.clientNo[0]);
           //switch (axisType)
           //{
           //    case 1: // 直线轴
           HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_MOVE_UNIT, ref Move_unit, clientno);
           //    break;
           //default: // 旋转轴
           HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_TURN_UNIT, ref Turn_unit, clientno);
           //        break;
           //}

           /*系统ID*/
           ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_SN_NUM, ref NC_ID, clientno);
           if (ret32 == 0)
           {
               // value64 = value64 / Move_unit * 1000 * 60;
               run_info.id = NC_ID;
           }
           /*    电机转速     */
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_MOTOR_REV, Constants.AXIS_LOGIC_NUM_X, ref value64, clientno);
           if (ret32 == 0)
           {
               //value64 = value64 * 16000 / 131072  * 60;   /*华中数控电子齿轮比分子为16000(um),即螺距为16mm每圈,电子齿轮比分母是131072,即XYZ 轴使用的编码器是一圈131072个脉冲,*/
               real_data.intAxis_1 = value64;                /*按照开发文挡转换出现错误,系统内部已经转换完成了--|*/
               run_info.aspd1 = (float)value64;
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_MOTOR_REV, Constants.AXIS_LOGIC_NUM_Y, ref value64, clientno);
           if (ret32 == 0)
           {
              // value64 = value64 * 16000 / 131072 * 60;
               real_data.intAxis_2 = value64;
               run_info.aspd2 = (float)value64;
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_MOTOR_REV, Constants.AXIS_LOGIC_NUM_Z, ref value64, clientno);
           if (ret32 == 0)
           {
               //value64 = value64 * 16000 / 131072 * 60;
               real_data.intAxis_3 = value64;
               run_info.aspd3 = (float)value64;
           }
           

           /*    电机负载电流    */
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, Constants.AXIS_LOGIC_NUM_X, ref value64, clientno);
          if (ret32 == 0)
           {
               real_data.intAxis_Curr_1 = value64;   //负载电流单位为A,机床空负载时有静态误差,会出现负电流
               run_info.load1 = (float)value64; 
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, Constants.AXIS_LOGIC_NUM_Y, ref value64, clientno);
           if (ret32 == 0)
           {
               real_data.intAxis_Curr_2 = value64;
               run_info.load2 = (float)value64; 
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, Constants.AXIS_LOGIC_NUM_Z, ref value64, clientno);
           if (ret32 == 0)
           {
               real_data.intAxis_Curr_3 = value64;
               run_info.load3 = (float)value64; 
           }
            /*需要增加对轴实际位置和指令位置的采集 ，数据分析中使用两者之差得出跟踪误差*/
           /*    ACT_POS相对实际位置     */
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_ACT_POS_RCS, Constants.AXIS_LOGIC_NUM_X, ref value, clientno);
           if (ret32 == 0)
           {
               value_flo = (float)value / Move_unit;
               run_info.apst1 = value_flo;
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_ACT_POS_RCS, Constants.AXIS_LOGIC_NUM_Y, ref value, clientno);
           if (ret32 == 0)
           {
               value_flo = (float)value / Move_unit;
               run_info.apst2 = value_flo;
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_ACT_POS_RCS, Constants.AXIS_LOGIC_NUM_Z, ref value, clientno);
           if (ret32 == 0)
           {
               value_flo = (float)value / Move_unit;
               run_info.apst3 = value_flo;
           }
           /*    CMD_POS相对指令位置     */
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_POS_RCS, Constants.AXIS_LOGIC_NUM_X, ref value, clientno);
           if (ret32 == 0)
           {
               value_flo = (float)value / Move_unit;
               run_info.cpst1 = value_flo;
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_POS_RCS, Constants.AXIS_LOGIC_NUM_Y, ref value, clientno);
           if (ret32 == 0)
           {
               value_flo = (float)value / Move_unit;
               run_info.cpst2 = value_flo;
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_POS_RCS, Constants.AXIS_LOGIC_NUM_Z, ref value, clientno);
           if (ret32 == 0)
           {
               value_flo = (float)value / Move_unit;
               run_info.cpst3 = value_flo;
           }
           /*    跟踪误差    */
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_FOLLOW_ERR, Constants.AXIS_LOGIC_NUM_X, ref value, clientno);
           if (ret32 == 0)
           {
               real_data.intAxis_1_err = value;  //单位为mm,一般固定不变
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_FOLLOW_ERR, Constants.AXIS_LOGIC_NUM_Y, ref value, clientno);
           if (ret32 == 0)
           {
               real_data.intAxis_2_err = value;
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_FOLLOW_ERR, Constants.AXIS_LOGIC_NUM_Z, ref value, clientno);
           if (ret32 == 0)
           {
               real_data.intAxis_3_err = value;
           }
           /*    主轴实际进给速度     */
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_ACT_FEEDRATE, 0, 0, ref value64, clientno);
           if (ret32 == 0)
           {
              // value64 = value64 / Move_unit * 1000 * 60;
               real_data.intFeed_v = value64;
           }
           /*    主轴实际速度(转速)     */
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_ACT_SPDL_SPEED, 0, 0, ref value64, clientno);
           if (ret32 == 0)
           {
               //value64 = value64 / 4096 * 60;   /*主轴作为旋转轴,电子齿轮分子应该为360度,分母为4096,即主轴所用编码器为一圈4096个脉冲*/
              real_data.intSpindle_v = value64;   /*主轴逻辑轴号为5,进给轴逻辑轴号分别是0,1,2*/
              run_info.cas = (float)value64;
           }
           /*    增加 主轴指令速度(转速)     */
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_CMD_SPDL_SPEED, 0, 0, ref value64, clientno);
           if (ret32 == 0)
           {
               //value64 = value64 / 4096 * 60;   /*主轴作为旋转轴,电子齿轮分子应该为360度,分母为4096,即主轴所用编码器为一圈4096个脉冲*/
               // real_data.intSpindle_v = value64;   /*主轴逻辑轴号为5,进给轴逻辑轴号分别是0,1,2*/
               run_info.ccs = (float)value64;
           }
           /*    主轴负载电流     */
           double CurrentRate = 0;
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_RATED_CUR, 5, ref value64, clientno);
           if (ret32 == 0)
           {
               //value64 = value64 / Turn_unit * 1000 * 60;
               CurrentRate = value64;
           }
           ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, 5, ref value64, clientno);
           if (ret32 == 0)
           {
               real_data.intAxis_Curr_4 = value64 * CurrentRate/100;   //负载电流单位为A,机床空负载时有静态误差,会出现负电流
               run_info.aload = (float)real_data.intAxis_Curr_4;
           }
           /*    运行程序编号     */
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_RUN_PROG, 0, 0, ref value, clientno);
           if (ret32 == 0)
           {
               real_data.intProg_num = value ;
               run_info.pd = (short)value;
           }
            /*运行程序名*/
           string progname = string.Empty;
           ret32 = HncApi.HNC_FprogGetFullName(0, ref progname, clientno);
           if (ret32 == 0)
           {
               string[] sArray = progname.ToString().Split(new Char[1] { '/' });
               run_info.pn = sArray[2];
           }
           /*    运行程序行     */
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_RUN_ROW, 0, 0, ref value, clientno);
           if (ret32 == 0)
           {
               real_data.intProg_running=value;
               run_info.pl = value;
           }
           /*    编码程序行     */
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_DCD_ROW, 0, 0, ref value, clientno);
           if (ret32 == 0)
           {
              real_data.intProg_decode=value;
           }
           /*    读取当前G代码     */
           //ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_CMD_TYPE, 0, 0, ref value, clientno);
           //if (ret32 == 0)
           //{
             //  real_data.strProg_Gcode = value.ToString();  //当前G 代码只是编码译码过程中的中间码,没有实际意义[姜工],后期考虑去除
           //}
            /*Gcode内容*/
           if (real_data.intProg_running >= 0)
           {
               real_data.strProg_Gcode = Gcode.gcode[real_data.intProg_running];
           }
           /* 通道模态（共80种） */
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_MODAL, 0, 0, ref value, clientno);
           if (ret32 == 0)
           {
               run_info.pm = (short)value;
           }
           /*上次报警发生时间*/
           real_data.strErr_time = CGlbFunc.Error_time_last[num];
           ///*    加工计件     */
           //ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_PART_CNTR, 0, 0, ref value, clientno);
           //if (ret32 == 0)
           //{
           //    real_data.intProcess_num= value;
           //}
           ///*    加工总数     */
           //ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_PART_STATI, 0, 0, ref value, clientno);
           //if (ret32 == 0)
           //{
           //    real_data.intProcess_sum = value;
           //}
           /*故障信息比较*/
           HncApi.HNC_AlarmGetNum((int)AlarmType.ALARM_TYPE_ALL,  //获取所有类型的报警
                                  (int)AlarmLevel.ALARM_ERR,               //仅获取error
                                  ref curAlarmNum, clientno);
           if (curAlarmNum != 0)
           {
               CurrentAlarm.Clear();  //
               for (index = 0; index < curAlarmNum; index++)
               {
                   real_data.intErr_flag = 2;
                   ret32 = HncApi.HNC_AlarmGetData((int)AlarmType.ALARM_TYPE_ALL,  //获取所有类型的报警
                                             (int)AlarmLevel.ALARM_ERR, //仅获取error
                                             index,
                                             ref alarmID,     //获取此报警的唯一ID，可用于报警识别
                                             ref txt,             //报警文本
                                            clientno);
                   real_data.intErr_num = alarmID.ToString();
                   real_data.strErr_txt = txt;

                   AlarmStruct gns = new AlarmStruct();  //更新当前报警列表，包含了ID
                   gns.ID = NC_ID;
                   gns.alarm_num = alarmID.ToString();
                   gns.alarm_text = txt;
                   CurrentAlarm.Add(gns); 

                  // real_data.strErr_log += System.DateTime.Now.ToString("MM-dd HH:mm:sss") + "报警发生："+txt;//获得系统时间，当作时间戳

               }
               #region 报警处理
               //当前报警集合清空后，再采集，与历史报警集合作差集(把当前list中不同的添加到历史中去,相同不添加)
               //新产生的报警都会在当前集合，再添加到历史集合中去
               //历史报警与当前报警作差集，得到的集合就是报警消除了的那个，再历史报警中去除，并在界面显示
               switch (num)
               {
                   case 1:
                       List<AlarmStruct> inter = CurrentAlarm.Except(HistoryAlarm_1).ToList();
                       foreach (AlarmStruct temp in inter)
                       {
                           HistoryAlarm_1.Add(temp);
                           //触发Err_Log发生新报警动作
                           
                            CGlbFunc.Error_time_last[num]= System.DateTime.Now.ToString("MM-dd HH:mm");
                           NC_Err_Log1.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                           ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                       }
                       break;
                   case 2:
                       inter = CurrentAlarm.Except(HistoryAlarm_2).ToList();
                       foreach (AlarmStruct temp in inter)
                       {
                           HistoryAlarm_2.Add(temp);
                           //触发Err_Log发生新报警动作
                           CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                           NC_Err_Log2.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                           ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                       }
                       break;
                   case 3:
                       inter = CurrentAlarm.Except(HistoryAlarm_3).ToList();
                       foreach (AlarmStruct temp in inter)
                       {
                           HistoryAlarm_3.Add(temp);
                           //触发Err_Log发生新报警动作
                           CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                           NC_Err_Log3.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                           ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                       }
                       break;
                   case 4:
                       inter = CurrentAlarm.Except(HistoryAlarm_4).ToList();
                       foreach (AlarmStruct temp in inter)
                       {
                           HistoryAlarm_4.Add(temp);
                           //触发Err_Log发生新报警动作
                           CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                           NC_Err_Log4.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                           ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                       }
                       break;
                   case 5:
                       inter = CurrentAlarm.Except(HistoryAlarm_5).ToList();
                       foreach (AlarmStruct temp in inter)
                       {
                           HistoryAlarm_5.Add(temp);
                           //触发Err_Log发生新报警动作
                           CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                           NC_Err_Log5.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                           ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                       }
                       break;
                  
                   default:
                       break;
               }

           }
           else
           {
               CurrentAlarm.Clear();  //
               real_data.intErr_flag = 0;           //当没有报警的时候Err提示灯变灰,报警号显示为0,报警文本清除
               real_data.intErr_num = "0";
               real_data.strErr_txt = null;
           }

           switch (num)
           {
               case 1:
                   List<AlarmStruct> inter2 = HistoryAlarm_1.Except(CurrentAlarm).ToList();
                   foreach (AlarmStruct temp in inter2)
                   {
                       HistoryAlarm_1.Remove(temp);
                       //触发报警清除动作
                       NC_Err_Log1.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                       ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                   }
                   break;
               case 2:
                   inter2 = HistoryAlarm_2.Except(CurrentAlarm).ToList();
                   foreach (AlarmStruct temp in inter2)
                   {
                       HistoryAlarm_2.Remove(temp);
                       //触发报警清除动作
                       NC_Err_Log2.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                       ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                   }
                   break;
               case 3:
                   inter2 = HistoryAlarm_3.Except(CurrentAlarm).ToList();
                   foreach (AlarmStruct temp in inter2)
                   {
                       HistoryAlarm_3.Remove(temp);
                       //触发报警清除动作
                       NC_Err_Log3.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                       ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                   }
                   break;
               case 4:
                   inter2 = HistoryAlarm_4.Except(CurrentAlarm).ToList();
                   foreach (AlarmStruct temp in inter2)
                   {
                       HistoryAlarm_4.Remove(temp);
                       //触发报警清除动作
                       NC_Err_Log4.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                       ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                   }
                   break;
               case 5:
                   inter2 = HistoryAlarm_5.Except(CurrentAlarm).ToList();
                   foreach (AlarmStruct temp in inter2)
                   {
                       HistoryAlarm_5.Remove(temp);
                       //触发报警清除动作
                       NC_Err_Log5.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                       ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                   }
                   break;
               
               default:
                   break;
           }
               #endregion

           /*       NC状态     */
           //real_data.strState_nc = 0;
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_IS_PROGEND, 0, 0, ref value, clientno);
           if (ret32 == 0 && value != 0)
           {
               real_data.strState_nc = 1;
               run_info.ps = "程序运行完成";
           }
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_IS_THREADING, 0, 0, ref value, clientno);
           if (ret32 == 0 && value != 0)
           {
               real_data.strState_nc = 2;
               run_info.ps = "螺纹加工";
           }
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_IS_RIGID, 0, 0, ref value, clientno);
           if (ret32 == 0 && value != 0)
           {
               real_data.strState_nc = 3;
               run_info.ps = "刚性攻丝";
           }
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_IS_REWINDED, 0, 0, ref value, clientno);
           if (ret32 == 0 && value != 0)
           {
               real_data.strState_nc = 4;
               run_info.ps = "重运行复位";
           }
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_IS_ESTOP, 0, 0, ref value, clientno);
           if (ret32 == 0 && value != 0)
           {
               real_data.strState_nc = 5;
               run_info.ps = "急停";
           }
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_IS_RESETTING, 0, 0, ref value, clientno);
           if (ret32 == 0 && value != 0)
           {
               real_data.strState_nc = 6;
               run_info.ps = "复位";
           }
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_IS_RUNNING, 0, 0, ref value, clientno);
           if (ret32 == 0 && value != 0)
           {
               real_data.strState_nc = 7;
               run_info.ps = "运行中";
           }
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_IS_HOMING, 0, 0, ref value, clientno);
           if (ret32 == 0 && value != 0)
           {
               real_data.strState_nc = 8;
               run_info.ps = "回零中";
           }
           ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_IS_MOVING, 0, 0, ref value, clientno);
           if (ret32 == 0 && value != 0)
           {
               real_data.strState_nc = 9;
               run_info.ps = "轴移动中";
           }
          

            stoptime = GetTickCount();         //在调用前后使用GetTickCount函数，获得轮循一台设备的时间
            //System.DateTime currentTime = new System.DateTime();
            real_data.dataPC_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff");//获得系统时间，当作时间戳

            run_info.time = real_data.dataPC_time;  //运行数据采集时间戳

            real_data.dataPC_time_Mill = stoptime - timestart;
            StatusData_user.WriteTxt(true, real_data);  //调用statusdata类的方法writetxt,写入txt
            real_data.strPower_time = CGlbFunc.Power_time_today[num];  //每次都在更新开机时间

            queue_state.Enqueue(run_info);

 #if MARCO_TWINCAT 
            TwincatGet adsget = new TwincatGet();
            ads.ADS_dataget(NC_ID, num, real_data, env_info);
            

            //ADS_dataget(NC_ID, num);    //调用函数获取twincat数据
 #endif

            switch (num)
            {
                case  1:
            NC_status_1.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case  2:
            NC_status_2.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                    case  3:
            NC_status_3.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                    case  4:
            NC_status_4.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                    case  5:
            NC_status_5.NC_StatusUpdate(real_data);  //更新显示的数据方法
            break;
                default :
                    break;
            }
        }
        /*广州数控获取数据函数*/
        /*real status 按照输入的gsktocket更新显示*/
        private void Real_status_gsk(Int32 num)
        {
            Data_Run_info run_info = new Data_Run_info();
            Int32 curalarmnum = 0;
            Int32 alarmflag = 0;
            String txt = String.Empty;
            Int32 ret32 = 0;
            UInt32 timestart;
            UInt32 stoptime;
            IntPtr gskclient=CGlbFunc.gsktock[num];
            IntPtr retptr = IntPtr.Zero;
            string NC_ID = string.Empty;
            ClearRealData();
            /*开始请求数据 打包 包含了run_info和real_data*/

            timestart = GetTickCount();
            real_data.strErr_log = " ";       //每次调用Real_Status时清除strErr_Log
            real_data.strIPv4_txt = CGlbFunc.ip_info[num].Remove(0, 4);
            real_data.intSys_num = num;

            ret32 = GskApi.GSKRM_GetConnectState(gskclient); //-1:无效实例； 0:未连接; 1:已经连接
            if (ret32 == 1)
            {
                real_data.intPower_flag = 1;
                real_data.intComm_flag = 1;
            }
            else
            {
                /*广数没有自动重连的功能，需要断连以后重新连接*/
                IPAddress addr = IPAddress.Parse(CGlbFunc.ip_info[num]);
                byte[] ip_by = addr.GetAddressBytes();
                retptr = GskApi.GSKRM_CreateInstance(ref ip_by[0], 1);//表示TCP/IP连接
                if (retptr == IntPtr.Zero)
                {
                    real_data.intPower_flag = 2;
                    real_data.intComm_flag = 2;
                    return ;
                }
                else
                {
                    CGlbFunc.gsktock[num] = retptr;  //更新连接实例
                    gskclient = retptr;
                }
            }
            /*系统ID*/
            run_info.id = CGlbFunc.id_info[num];

            retptr = InfoApi.GSKRM_GetVersionInfo(gskclient);
            if (retptr != IntPtr.Zero)
            {
                VERS_INFO structure = new VERS_INFO();
                structure = (VERS_INFO)Marshal.PtrToStructure(retptr, typeof(VERS_INFO));
                NC_ID = structure.softWareNumber;
                run_info.id = NC_ID;
            }
            else
            {
                Console.WriteLine("fail to get gsk_id");
                /*广数没有自动重连的功能，需要断连以后重新连接*/
                IPAddress addr = IPAddress.Parse(CGlbFunc.ip_info[num]);
                byte[] ip_by = addr.GetAddressBytes();
                retptr = GskApi.GSKRM_CreateInstance(ref ip_by[0], 1);//表示TCP/IP连接
                if (retptr == IntPtr.Zero)
                {
                    real_data.intPower_flag = 2;
                    real_data.intComm_flag = 2;
                }
                else
                {
                    CGlbFunc.gsktock[num] = retptr;  //更新连接实例
                    gskclient = retptr;
                }
            }
            /*0524 添加电机负载电流*/
            retptr = InfoApi.GSKRM_GetbhRunInfo(gskclient);
            if (retptr != IntPtr.Zero)
            {
                BHSAMPLE_STATIC s = new BHSAMPLE_STATIC();
                s = (BHSAMPLE_STATIC)Marshal.PtrToStructure(retptr, typeof(BHSAMPLE_STATIC));
                /*    电机负载电流    */

                real_data.intAxis_Curr_1 = s.load[0] / 10000;   //负载电流单位为A,机床空负载时有静态误差,会出现负电流
                run_info.load1 = s.load[0] / 10000;

                real_data.intAxis_Curr_2 = s.load[1] / 10000;
                run_info.load2 = s.load[1] / 10000;

                real_data.intAxis_Curr_3 = s.load[2]/10000;
                run_info.load3 = s.load[2] / 10000;

                /*    主轴负载电流     */

                real_data.intAxis_Curr_4 = s.aload/10;   //负载电流单位为A,机床空负载时有静态误差,会出现负电流
                run_info.aload = s.aload/10;
                /*运行程序名           */

                run_info.pn = s.progname;
                /*运行程序num          */
                real_data.intProg_num = s.prognum;
                run_info.pd = s.prognum;
                /*    增加 主轴指令速度(转速)     */
                run_info.ccs = s.ccs;
            }
             /*get axis infomation*/
            retptr = InfoApi.GSKRM_GetAxisInfo(gskclient);
            if (retptr != IntPtr.Zero)
            {
                AXIS_INFO s = new AXIS_INFO();
                s = (AXIS_INFO)Marshal.PtrToStructure(retptr, typeof(AXIS_INFO));

                /*    电机转速     */

                real_data.intAxis_1 = s.ActiveAxisSpeed[0];
                run_info.aspd1 = (float)real_data.intAxis_1;

                real_data.intAxis_2 = s.ActiveAxisSpeed[1];
                run_info.aspd2 = (float)real_data.intAxis_2;


                real_data.intAxis_3 = s.ActiveAxisSpeed[2];
                run_info.aspd3 = (float)real_data.intAxis_3; 

                /*需要增加对轴实际位置和指令位置的采集 ，数据分析中使用两者之差得出跟踪误差*/
                /*    ACT_POS相对实际位置     */
                run_info.apst1 = (float)s.Abs_Coord[0];
                run_info.apst2 = (float)s.Abs_Coord[1];
                run_info.apst3 = (float)s.Abs_Coord[2];

                /*    CMD_POS相对指令位置     */
                run_info.cpst1 = (float)s.Rel_Coord[0];
                run_info.cpst2 = (float)s.Rel_Coord[1];
                run_info.cpst3 = (float)s.Rel_Coord[2];

                /*    调试用位置代替电机转速     */
                //real_data.intAxis_1 = s.Rel_Coord[0];
               // real_data.intAxis_2 = s.Rel_Coord[1];
                //real_data.intAxis_3 = s.Rel_Coord[2];

                /*    跟踪误差 /广数为剩余距离   */

                real_data.intAxis_1_err = 0;
                real_data.intAxis_2_err = 0;
                real_data.intAxis_3_err = 0;

                /*    主轴实际进给速度     */

                real_data.intFeed_v = s.Frd;

                /*    主轴实际速度(转速)     */

                real_data.intSpindle_v = s.Spd;
                run_info.cas = s.Spd;

                /*    增加 主轴指令速度(转速)     */

                //run_info.ccs = (float)value64;

            }
            else
            {
                Console.WriteLine("fail to get gsk_axis");
            }

            /*get runstate infomation*/
            retptr = InfoApi.GSKRM_GetRunInfo(gskclient);
            if (retptr != IntPtr.Zero)
            {
                RUNSTAT_INFO s = new RUNSTAT_INFO();
                s = (RUNSTAT_INFO)Marshal.PtrToStructure(retptr, typeof(RUNSTAT_INFO));
                /*    运行程序编号     */
                real_data.intProg_num = 0;
                //run_info.pd = 0;
                /*运行程序名           */
                //run_info.pn = s.progfileName;
                /*    运行程序行     */

                real_data.intProg_running = (int)s.BlockNumber;
                run_info.pl = (int)s.BlockNumber;
                /*Gcode内容*/
                real_data.strProg_Gcode = Gcode.gcode[real_data.intProg_running];

                /*    编码程序行     */

                real_data.intProg_decode = 0;

                /*    读取当前G代码     */

               // real_data.strProg_Gcode = string.Empty;  //当前G 代码只是编码译码过程中的中间码,没有实际意义[姜工],后期考虑去除

                /* 通道模态（共22种） */
                for (short j = 0; j < 22; j++)
                {
                    if (s.Gmode[j] == 1)
                        run_info.pm = j;
                }
                /*       NC状态     */
                switch (s.OpMode)
                {
                    case 0:
                        real_data.strState_nc = 1;
                        run_info.ps = "就绪";
                        break;
                    case 2:
                        real_data.strState_nc = 5;
                        run_info.ps = "急停";
                        break;
                    case 1:
                        real_data.strState_nc = 6;
                        run_info.ps = "复位";
                        break;
                    case 5:
                        real_data.strState_nc = 8;
                        run_info.ps = "回零中";
                        break;
                    case 4:
                        real_data.strState_nc = 9;
                        run_info.ps = "轴移动中";
                        break;
                    default:
                        real_data.strState_nc = 7;
                        run_info.ps = "运行中";
                        break;
                }
                if (s.currentalarmflag != 0)
                {
                    alarmflag = 1;
                    curalarmnum = s.currentalarmflag;
                }
                real_data.intErr_num = s.currentAlarmNum.ToString(); //广州数控的当前报警号可以直接采集
            }
            else
            {
                Console.WriteLine("fail to get gsk_runstate");
            }
            /*上一次报警时间*/
            real_data.strErr_time = CGlbFunc.Error_time_last[num];
             /*get program infomation*/
            //Int32 totalprogram = 0;
            //retptr = InfoApi.GSKRM_GetProgramInfo(gskclient, ref totalprogram); //返回0无法正确取得
            //if (retptr != IntPtr.Zero)
            //{
            //    PROGRAM_INFO s = new PROGRAM_INFO();
            //    s = (PROGRAM_INFO)Marshal.PtrToStructure(retptr, typeof(PROGRAM_INFO));
            //    run_info.pn = s.programName;
            //    run_info.pd = (short)s.index;
            //}

            /*故障信息比较*/
            Int32 totalhistroy=0;
            ALARMLIST CurAlarmList = new ALARMLIST();
            CurrentAlarm.Clear();
            if (alarmflag != 0)
            {
                retptr = InfoApi.GSKRM_GetAlarmInfo(gskclient, ref totalhistroy);
                if (retptr != IntPtr.Zero)
                {
                    CurAlarmList = (ALARMLIST)Marshal.PtrToStructure(retptr, typeof(ALARMLIST));
                    //for (int i = 0; i < 20; i++)
                    //{
                    //    Console.WriteLine(CurAlarmList.alarm[0]);
                    //}
                }
                else
                {
                    Console.WriteLine("fail to get gsk_erro");
                }

                real_data.intErr_flag = 2;  //red

                real_data.strErr_txt = CurAlarmList.alarm[0].ErrorMessage;
                //real_data.intErr_num = CurAlarmList.alarm[0].ErrorNoStr + CurAlarmList.alarm[0].axisNo.ToString(); //更新显示部分，err_txt与err_num对应最新报警的信息，广数支持获取int型的报警号。
                for (int i = 0; i < curalarmnum; i++)
                {
                    AlarmStruct his = new AlarmStruct();
                    his.ID = NC_ID ;
                    his.alarm_num = CurAlarmList.alarm[i].ErrorNoStr + "&"+CurAlarmList.alarm[i].axisNo.ToString();
                    his.alarm_text = CurAlarmList.alarm[i].ErrorMessage;
                    CurrentAlarm.Add(his);
                }
                 switch (num)
                 {
                     case 6:
                         List<AlarmStruct> inter = CurrentAlarm.Except(HistoryAlarm_6).ToList();
                         foreach (AlarmStruct temp in inter)
                         {
                             HistoryAlarm_6.Add(temp);
                             //触发Err_Log发生新报警动作
                             CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                             NC_Err_Log6.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                             ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                         }
                         break;
                     case 7:
                         inter = CurrentAlarm.Except(HistoryAlarm_7).ToList();
                         foreach (AlarmStruct temp in inter)
                         {
                             HistoryAlarm_7.Add(temp);
                             //触发Err_Log发生新报警动作
                             CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                             NC_Err_Log7.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                             ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                         }
                         break;
                     case 8:
                         inter = CurrentAlarm.Except(HistoryAlarm_8).ToList();
                         foreach (AlarmStruct temp in inter)
                         {
                             HistoryAlarm_8.Add(temp);
                             //触发Err_Log发生新报警动作
                             CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                             NC_Err_Log8.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                             ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                         }
                         break;
                     case 9:
                         inter = CurrentAlarm.Except(HistoryAlarm_9).ToList();
                         foreach (AlarmStruct temp in inter)
                         {
                             HistoryAlarm_9.Add(temp);
                             //触发Err_Log发生新报警动作
                             CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                             NC_Err_Log9.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                             ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                         }
                         break;
                     case 10:
                         inter = CurrentAlarm.Except(HistoryAlarm_10).ToList();
                         foreach (AlarmStruct temp in inter)
                         {
                             HistoryAlarm_10.Add(temp);
                             //触发Err_Log发生新报警动作
                             CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                             NC_Err_Log10.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                             ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                         }
                         break;
                     default:
                         break;
                 }
      
            }
            else
            {
                CurrentAlarm.Clear();  
                real_data.intErr_flag = 0;           //当没有报警的时候Err提示灯变灰,报警号显示为0,报警文本清除
                real_data.intErr_num = "0";
                real_data.strErr_txt = null;
            }
            switch (num)
            {
                case 6:
                    List<AlarmStruct> inter2 = HistoryAlarm_6.Except(CurrentAlarm).ToList();
                    foreach (AlarmStruct temp in inter2)
                    {
                        HistoryAlarm_6.Remove(temp);
                        //触发报警清除动作
                        NC_Err_Log6.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                        ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                    }
                    break;
                case 7:
                    inter2 = HistoryAlarm_7.Except(CurrentAlarm).ToList();
                    foreach (AlarmStruct temp in inter2)
                    {
                        HistoryAlarm_7.Remove(temp);
                        //触发报警清除动作
                        NC_Err_Log7.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                        ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                    }
                    break;
                case 8:
                    inter2 = HistoryAlarm_8.Except(CurrentAlarm).ToList();
                    foreach (AlarmStruct temp in inter2)
                    {
                        HistoryAlarm_8.Remove(temp);
                        //触发报警清除动作
                        NC_Err_Log8.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                        ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                    }
                    break;
                case 9:
                    inter2 = HistoryAlarm_9.Except(CurrentAlarm).ToList();
                    foreach (AlarmStruct temp in inter2)
                    {
                        HistoryAlarm_9.Remove(temp);
                        //触发报警清除动作
                        NC_Err_Log9.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                        ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                    }
                    break;
                case 10:
                    inter2 = HistoryAlarm_10.Except(CurrentAlarm).ToList();
                    foreach (AlarmStruct temp in inter2)
                    {
                        HistoryAlarm_10.Remove(temp);
                        //触发报警清除动作
                        NC_Err_Log10.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                        ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                    }
                    break;
                default:
                    break;
            }
            stoptime = GetTickCount();                                                        //在调用前后使用GetTickCount函数，获得轮循一台设备的时间
            real_data.dataPC_time_Mill = stoptime - timestart;
            real_data.dataPC_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff");//获得系统时间，当作时间戳
            run_info.time = real_data.dataPC_time;                                            //运行数据采集时间戳
            StatusData_user.WriteTxt(true, real_data);                                        //调用statusdata类的方法writetxt,写入txt
            real_data.strPower_time = CGlbFunc.Power_time_today[num];                        //每次都在更新开机时间
            /*打包数据完成 下一步run_info入队列，real_data 更新显示*/
            queue_state.Enqueue(run_info);


#if MARCO_TWINCAT
            
            ads.ADS_dataget(NC_ID, num, real_data, env_info);
           // ADS_dataget(NC_ID,num);    //调用函数获取twincat数据
#endif

            switch (num)
            {
                case 6:
                    NC_status_6.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 7:
                    NC_status_7.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 8:
                    NC_status_8.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 9:
                    NC_status_9.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 10:
                    NC_status_10.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                default:
                    break;
            }
        }
        /*高精数控获取数据函数*/
        /*real status 按照输入的clientno更新显示*/
        private void Real_status_gj(Int32 num)
        {

            Data_Run_info run_info = new Data_Run_info();
            Int32 alarmID = 0;
            Int32 index = 0;      //index取值范围为 0~num-1，num是当前报警个数
            String txt = String.Empty;
            String NC_ID = "";
            int curAlarmNum = 0;   //当前报警
            Int32 ret32 = 0;
            Int32 value = 0;
            Double value64 = 0;
            float value_flo = 0;
            UInt32 timestart;
            UInt32 stoptime;
            IntPtr retptr = IntPtr.Zero;
            Int32 clientno = CGlbFunc.clientNo[num];
            //String retstr=string.Empty;
            StringBuilder retstr = new StringBuilder();

            ClearRealData();
            timestart = GetTickCount();
            real_data.strErr_log = " ";       //每次调用Real_Status时清除strErr_Log
            real_data.strIPv4_txt = CGlbFunc.ip_info[num].Remove(0, 4); //为了显示关键信息，将192.删除
            real_data.intSys_num = num;
            //ret32 = HncApi.HNC_NetIsConnect(clientno);
            //if (ret32 == 0)
            //{
                real_data.intPower_flag = 1;
                real_data.intComm_flag = 1;
            //}
            //else
            //{
            //    real_data.intPower_flag = 2;
            //    real_data.intComm_flag = 2;
            //}
           
            /*系统ID*/
            run_info.id = "G" + CGlbFunc.ip_info[num].Remove(0, 4); //沈阳高精ID是自己定义的，只要可以唯一区别数控系统就OK
            NC_ID = run_info.id;
            /*    电机转速     */
            ret32 = GjApi.get430StatusVal(clientno, 2580, ref value64);
            if (ret32 == 0)
            {
                real_data.intAxis_1 = value64*60;       //高精采集的单位为mm/s，乘以60为mm/min           
                run_info.aspd1 = (float)value64*60;
            }
            else
            {
                real_data.intPower_flag = 2;
                real_data.intComm_flag = 2;
            }
            ret32 = GjApi.get430StatusVal(clientno, 2581, ref value64);
            if (ret32 == 0)
            {
                real_data.intAxis_2 = value64*60;
                run_info.aspd2 = (float)value64*60;
            }
            ret32 = GjApi.get430StatusVal(clientno, 2582, ref value64);
            if (ret32 == 0)
            {
                real_data.intAxis_3 = value64*60;
                run_info.aspd3 = (float)value64*60;
            }
            /*    电机负载电流    */
            ret32 = GjApi.get430StatusVal(clientno, 2570, ref value64);
            if (ret32 == 0)
            {
                real_data.intAxis_Curr_1 = value64/100;   //负载电流单位为A,机床空负载时有静态误差,会出现负电流
                run_info.load1 = (float)value64/100;
            }
            ret32 = GjApi.get430StatusVal(clientno, 2571, ref value64);
            if (ret32 == 0)
            {
                real_data.intAxis_Curr_2 = value64/100;
                run_info.load2 = (float)value64/100;
            }
            ret32 = GjApi.get430StatusVal(clientno, 2572, ref value64);
            if (ret32 == 0)
            {
                real_data.intAxis_Curr_3 = value64/100;
                run_info.load3 = (float)value64/100;
            }
            /*需要增加对轴实际位置和指令位置的采集 ，数据分析中使用两者之差得出跟踪误差*/
            /*    ACT_POS相对实际位置     */
            ret32 = GjApi.get430StatusVal(clientno, 2094, ref value64);
            if (ret32 == 0)
            {
                value_flo = (float)value64;
                run_info.apst1 = value_flo;
            }
            ret32 = GjApi.get430StatusVal(clientno, 2096, ref value64);
            if (ret32 == 0)
            {
                value_flo = (float)value64;
                run_info.apst2 = value_flo;
            }
            ret32 = GjApi.get430StatusVal(clientno, 2098, ref value64);
            if (ret32 == 0)
            {
                value_flo = (float)value64;
                run_info.apst3 = value_flo
;
            }
            /*    CMD_POS相对指令位置     */
            ret32 = GjApi.get430StatusVal(clientno, 2082, ref value64);
            if (ret32 == 0)
            {
                value_flo = (float)value64;
                run_info.cpst1 = value_flo;
            }
            ret32 = GjApi.get430StatusVal(clientno, 2084, ref value64);
            if (ret32 == 0)
            {
                value_flo = (float)value64;
                run_info.cpst2 = value_flo;
            }
            ret32 = GjApi.get430StatusVal(clientno, 2086, ref value64);
            if (ret32 == 0)
            {
                value_flo = (float)value64;
                run_info.cpst3 = value_flo;
            }
            /*    主轴实际进给速度     */
            ret32 = GjApi.get430StatusVal(clientno, 2028, ref value64);
            if (ret32 == 0)
            {
                // value64 = value64 / Move_unit * 1000 * 60;
                real_data.intFeed_v = value64;
            }
            /*    主轴实际速度(转速)     */
            ret32 = GjApi.get430StatusVal(clientno, 2048, ref value64);
            if (ret32 == 0)
            {
                real_data.intSpindle_v = value64;   /*主轴逻辑轴号为5,进给轴逻辑轴号分别是0,1,2*/
                run_info.cas = (float)value64;
            }
            /*    增加 主轴指令速度(转速)     */
            ret32 = GjApi.get430StatusVal(clientno, 2046, ref value64);
            if (ret32 == 0)
            {
                run_info.ccs = (float)value64;
            }
            /*    主轴负载电流     */
            ret32 = GjApi.get430StatusVal(clientno, 2562, ref value64);
            if (ret32 == 0)
            {
                real_data.intAxis_Curr_4 = value64/100;   //负载电流单位为A,机床空负载时有静态误差,会出现负电流
                run_info.aload = (float)value64/100;//高精需要除以100，因为空载，仍然存在零点漂移
            }
            /*    运行程序行     */
            ret32 = GjApi.get430StatusVal(clientno, 2013, ref value64);
            if (ret32 == 0)
            {
                real_data.intProg_running = (int)value64;
                run_info.pl = (int)value64;
            }
            /*Gcode内容*/
            real_data.strProg_Gcode = Gcode.gcode[real_data.intProg_running];

            /* 通道模态（共80种） */
            //ret32 = GjApi.get430StatusArrayVal(clientno, 2561,  retptr);
            //if (ret32 == 0)
            //{
            //    int[] s = new int[20];
            //    Marshal.Copy(retptr, s, 0, 20);//将数据从非托管内存指针复制到托管 32 位带符号整数数组
            //    run_info.pm = (short)s[0];
            //} 
            /*       NC状态     */
            ret32 = GjApi.get430StatusVal(clientno, 2003, ref value64);
            if (ret32 == 0)
            {
                if (value64 == 1)
                {
                    real_data.strState_nc = 10;
                    run_info.ps ="空闲";
                }
                if (value64 == 2)
                {
                    real_data.strState_nc = 7;
                    run_info.ps = "运行";
                }
                  if (value64 == 3)
                {
                    real_data.strState_nc =11;
                    run_info.ps = "暂停";
                }
               
            }
            /*程序名*/
            ret32 = GjApi.get430StatusStrVal(clientno, 2152, retstr );
            if (ret32 == 0 )
            {
                string[] sArray = retstr.ToString().Split(new Char[1] {'/'});
                if (sArray.Length != 1)
                {
                    run_info.pn = sArray[2];
                }
            }
            /*上次报警时间*/
            real_data.strErr_time = CGlbFunc.Error_time_last[num];
            /*故障信息比较*/
            StringBuilder retstr_Motion = new StringBuilder();
            StringBuilder retstr_Plc = new StringBuilder();
            StringBuilder retstr_Err = new StringBuilder();

            ret32 = GjApi.get430AutoMotionErrorVal(clientno, retstr_Motion);
            ret32 = GjApi.get430AutoPlcErrorVal(clientno, retstr_Plc);
            ret32 = GjApi.get430ErrorVal(clientno,retstr_Err);

            if (retstr_Motion.Length > 15 || retstr_Plc.Length > 15 || retstr_Err.Length > 15) //如果不为空,即有报警发生，为了防止误差，长度大于3
            {
                CurrentAlarm.Clear();  //
                if (retstr_Motion.ToString() != string.Empty)
                {
                    string strtemp = retstr_Motion.ToString();
                    string[] sArray = strtemp.Split(new char[1] { ':' });
                    real_data.intErr_num = sArray[0].Trim();
                    real_data.strErr_txt = sArray[1].Trim();

                    AlarmStruct gns = new AlarmStruct();  //更新当前报警列表，包含了ID
                    gns.ID = NC_ID;
                    gns.alarm_num = sArray[0].Trim();
                    //gns.alarm_text = sArray[1].Trim();
                    gns.alarm_text = strtemp.Replace(sArray[0].Trim(), "");
                    CurrentAlarm.Add(gns);
                }
                if (retstr_Plc.ToString() != string.Empty && retstr_Plc.Length > 15)
                {
                    string strtemp = retstr_Plc.ToString();
                    string[] sArray = strtemp.Split(new char[1] { ':' });
                    real_data.intErr_num = sArray[0].Trim();
                    real_data.strErr_txt = sArray[1].Trim();

                    AlarmStruct gns = new AlarmStruct();  //更新当前报警列表，包含了ID
                    gns.ID = NC_ID;
                    gns.alarm_num = sArray[0].Trim();
                    //gns.alarm_text = sArray[1].Trim();
                    gns.alarm_text = strtemp.Replace(sArray[0].Trim(), "");
                    CurrentAlarm.Add(gns);
                }
                if (retstr_Err.ToString() != string.Empty && retstr_Err.Length > 15)
                {
                    string strtemp = retstr_Err.ToString();
                    string[] sArray = strtemp.Split(new char[1] { ':' });
                    real_data.intErr_num = sArray[0].Trim();
                    real_data.strErr_txt = strtemp.Replace(sArray[0].Trim(), "");
                    //real_data.strErr_txt = sArray[1].Trim();

                    AlarmStruct gns = new AlarmStruct();  //更新当前报警列表，包含了ID
                    gns.ID = NC_ID;
                    gns.alarm_num = sArray[0].Trim();
                    gns.alarm_text = strtemp.Replace(sArray[0].Trim(), "");
                    //gns.alarm_text = sArray[1].Trim();
                    CurrentAlarm.Add(gns);
                }

                    // real_data.strErr_log += System.DateTime.Now.ToString("MM-dd HH:mm:sss") + "报警发生："+txt;//获得系统时间，当作时间戳
                switch (num)
                {
                    case 11:
                        List<AlarmStruct> inter = CurrentAlarm.Except(HistoryAlarm_11).ToList();
                        foreach (AlarmStruct temp in inter)
                        {
                            HistoryAlarm_11.Add(temp);
                            //触发Err_Log发生新报警动作
                            CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                            NC_Err_Log11.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                            ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                        }
                        break;
                    case 12:
                        inter = CurrentAlarm.Except(HistoryAlarm_12).ToList();
                        foreach (AlarmStruct temp in inter)
                        {
                            HistoryAlarm_12.Add(temp);
                            //触发Err_Log发生新报警动作
                            CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                            NC_Err_Log12.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                            ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                        }
                        break;
                    case 13:
                        inter = CurrentAlarm.Except(HistoryAlarm_13).ToList();
                        foreach (AlarmStruct temp in inter)
                        {
                            HistoryAlarm_13.Add(temp);
                            //触发Err_Log发生新报警动作
                            CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                            NC_Err_Log13.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                            ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                        }
                        break;
                    case 14:
                        inter = CurrentAlarm.Except(HistoryAlarm_14).ToList();
                        foreach (AlarmStruct temp in inter)
                        {
                            HistoryAlarm_14.Add(temp);
                            //触发Err_Log发生新报警动作
                            CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                            NC_Err_Log14.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                            ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                        }
                        break;
                    case 15:
                        inter = CurrentAlarm.Except(HistoryAlarm_15).ToList();
                        foreach (AlarmStruct temp in inter)
                        {
                            HistoryAlarm_15.Add(temp);
                            //触发Err_Log发生新报警动作
                            CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                            NC_Err_Log15.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                            ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                        }
                        break;

                    default:
                        break;
                }

            }//if发生报警
            else
            {
                CurrentAlarm.Clear();  //
                real_data.intErr_flag = 0;           //当没有报警的时候Err提示灯变灰,报警号显示为0,报警文本清除
                real_data.intErr_num = "0";
                real_data.strErr_txt = null;
            }
            switch (num)
            {
                case 11:
                    List<AlarmStruct> inter2 = HistoryAlarm_11.Except(CurrentAlarm).ToList();
                    foreach (AlarmStruct temp in inter2)
                    {
                        HistoryAlarm_11.Remove(temp);
                        //触发报警清除动作
                        NC_Err_Log11.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                        ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                    }
                    break;
                case 12:
                    inter2 = HistoryAlarm_12.Except(CurrentAlarm).ToList();
                    foreach (AlarmStruct temp in inter2)
                    {
                        HistoryAlarm_12.Remove(temp);
                        //触发报警清除动作
                        NC_Err_Log12.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                        ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                    }
                    break;
                case 13:
                    inter2 = HistoryAlarm_13.Except(CurrentAlarm).ToList();
                    foreach (AlarmStruct temp in inter2)
                    {
                        HistoryAlarm_13.Remove(temp);
                        //触发报警清除动作
                        NC_Err_Log13.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                        ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                    }
                    break;
                case 14:
                    inter2 = HistoryAlarm_14.Except(CurrentAlarm).ToList();
                    foreach (AlarmStruct temp in inter2)
                    {
                        HistoryAlarm_14.Remove(temp);
                        //触发报警清除动作
                        NC_Err_Log14.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                        ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                    }
                    break;
                case 15:
                    inter2 = HistoryAlarm_15.Except(CurrentAlarm).ToList();
                    foreach (AlarmStruct temp in inter2)
                    {
                        HistoryAlarm_15.Remove(temp);
                        //触发报警清除动作
                        NC_Err_Log15.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                        ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                    }
                    break;

                default:
                    break;
            }
                    
            stoptime = GetTickCount();         //在调用前后使用GetTickCount函数，获得轮循一台设备的时间
            real_data.dataPC_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff");//获得系统时间，当作时间戳
            run_info.time = real_data.dataPC_time;  //运行数据采集时间戳
            real_data.dataPC_time_Mill = stoptime - timestart;
            StatusData_user.WriteTxt(true, real_data);  //调用statusdata类的方法writetxt,写入txt
            real_data.strPower_time = CGlbFunc.Power_time_today[num];  //每次都在更新开机时间            
            queue_state.Enqueue(run_info);

#if MARCO_TWINCAT 
            //TwincatGet adsget = new TwincatGet();
            ads.ADS_dataget(NC_ID, num, real_data, env_info);
            //ADS_dataget(NC_ID,num);    //调用函数获取twincat数据
#endif

            switch (num)
            {
                case 11:
                    NC_status_11.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 12:
                    NC_status_12.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 13:
                    NC_status_13.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 14:
                    NC_status_14.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 15:
                    NC_status_15.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                default:
                    break;
            }
        }

        //航天数控获取数据函数
        private void Real_status_ht(Int32 num)
        {
            Data_Run_info run_info = new Data_Run_info();
            Int32 curalarmnum = 0;
            Int32 alarmflag = 0;
            String txt = String.Empty;
            UInt32 timestart;
            UInt32 stoptime;
            Int32 index=0;
            string NC_ID = string.Empty;
            ClearRealData();
            /*开始请求数据 打包 包含了run_info和real_data*/

            timestart = GetTickCount();
            real_data.strErr_log = " ";       //每次调用Real_Status时清除strErr_Log
            real_data.strIPv4_txt = CGlbFunc.ip_info[num].Remove(0, 4);
            real_data.intSys_num = num;
            //增加一个判断当前连接的api
            //ret32 = HncApi.HNC_NetIsConnect(clientno);
            //if (ret32 == 0)
            //{
            //    real_data.intPower_flag = 1;
            //    real_data.intComm_flag = 1;
            //}
            //else
            //{
            //    real_data.intPower_flag = 2;
            //    real_data.intComm_flag = 2;
            //}
             
            real_data.intPower_flag = 1;
            real_data.intComm_flag = 1;

            //将采集到的数据放到run_info去存储，送往web;另一方面送入real_data，用于显示
            try
            {
                IntPtr ret = new IntPtr();
                ret = HtApi.GetAxisinfo(num-15);
                if (ret != null)//获取成功
                {
                    HT_AXIS_INFO axis = new HT_AXIS_INFO();
                    axis = (HT_AXIS_INFO)Marshal.PtrToStructure(ret, typeof(HT_AXIS_INFO));
                    //获取主轴信息
                    //主轴实际速度
                    run_info.cas = axis.a_s_value;
                    real_data.intSpindle_v = run_info.cas;
                    //主轴指令速度
                    run_info.ccs = axis.c_s_value;
                    
                    //进给轴指令位置
                    run_info.cpst1 = axis.c_axis[0];
                    run_info.cpst2 = axis.c_axis[1];
                    run_info.cpst3 = axis.c_axis[2];
                    //进给轴实际位置，机床坐标
                    run_info.apst1 = (axis.a_axis_machine[0]);
                    run_info.apst2 = (axis.a_axis_machine[1]);
                    run_info.apst3 = (axis.a_axis_machine[2]);
                    //进给轴实际速度
                    run_info.aspd1 = axis.a_f_value[0];
                    run_info.aspd2 = axis.a_f_value[1];
                    run_info.aspd3 = axis.a_f_value[2];
                    real_data.intAxis_1 = run_info.aspd1;
                    real_data.intAxis_2 = run_info.aspd2;
                    real_data.intAxis_3 = run_info.aspd3;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("获取轴信息失败");
            }
            try
            {
                IntPtr ret = new IntPtr();
                ret = HtApi.GetSysteminfo(num-15);
                if (ret != null)//获取成功
                {
                    SYSTEM_INFO sysinfo = new SYSTEM_INFO();
                    sysinfo = (SYSTEM_INFO)Marshal.PtrToStructure(ret, typeof(SYSTEM_INFO));
                    run_info.id = sysinfo.systemid;//数控系统ID
                    NC_ID = run_info.id;
                    //代码运行状态
                    if (sysinfo.ps == 0)
                    {
                        real_data.strState_nc=1;//运行完成还是11暂停
                        run_info.ps = "停止";
                    }
                    else
                    {
                        real_data.strState_nc=7;
                        run_info.ps = "运行";
                    }
                    //run_info.ps = sysinfo.ps == 0 ? "停止" : "运行";
                    //G代码模态
                    //run_info.pm = sysinfo.ggroup;
                    //程序名
                    run_info.pn = sysinfo.pn;
                    //real_data.intProg_num = sysinfo.pn;
                    //运行代码行号
                    run_info.pl = sysinfo.pl;
                    real_data.intProg_running = sysinfo.pl;
                    //显示的g代码
                    if (real_data.intProg_running >= 0)
                    {
                        real_data.strProg_Gcode = Gcode.gcode[real_data.intProg_running];
                    }
                    //s_loadcurrent;		  主轴负载电流
                    run_info.aload = sysinfo.s_loadcurrent;
                    real_data.intAxis_Curr_4 = sysinfo.s_loadcurrent; 
                    //进给轴负载电流
                    run_info.load1 = sysinfo.axis_loadcurrent[0];
                    run_info.load2 = sysinfo.axis_loadcurrent[1];
                    run_info.load3 = sysinfo.axis_loadcurrent[2];
                    real_data.intAxis_Curr_1 = run_info.load1;
                    real_data.intAxis_Curr_2 = run_info.load2;
                    real_data.intAxis_Curr_3 = run_info.load3;

                }
            }
            catch
            {
                Console.WriteLine("获取电流信息失败");
            }
            try
            {
                IntPtr ret = new IntPtr();
                ret = HtApi.GetAlarminfo(num-15);
                if (ret != null)//获取成功
                {
                    HT_ALARM_INFO ala = new HT_ALARM_INFO();
                    ala = (HT_ALARM_INFO)Marshal.PtrToStructure(ret, typeof(HT_ALARM_INFO));
                }
            }
            catch
            {
                MessageBox.Show("获取报警信息失败");
            }
            try
            {
                IntPtr ret = new IntPtr();
                ret = HtApi.GetAlarminfo(num);
                HT_ALARM_INFO ala = new HT_ALARM_INFO();
                ala = (HT_ALARM_INFO)Marshal.PtrToStructure(ret, typeof(HT_ALARM_INFO));
                if (ret != null)//获取成功
                {                    
                    if (ala.alarmnum != 0)//报警个数不为0
                    {
                        alarmflag = 1;
                        curalarmnum = ala.alarmnum;
                    }
                    //txtaxis.Text = "报警时间：" + ala.alarmtime_occur + "\n" + "报警内容" + ala.alarmcode;
                }
                /*上一次报警时间*/
                real_data.strErr_time = CGlbFunc.Error_time_last[num];
                if (alarmflag == 1)//存在报警
                {
                    CurrentAlarm.Clear();  //
                    real_data.intErr_flag = 2;  //报警标志   
                    for (index = 0; index < curalarmnum; index++)
                    {
                        real_data.intErr_num = ala.alarmcode.ToString();//报警号
                        real_data.strErr_txt = ala.warn;

                        AlarmStruct gns = new AlarmStruct();  //更新当前报警列表，包含了ID
                        gns.ID = NC_ID;
                        gns.alarm_num = ala.alarmcode.ToString();
                        gns.alarm_text = ala.warn;
                        CurrentAlarm.Add(gns);
                    }
                #region 报警处理
               //当前报警集合清空后，再采集，与历史报警集合作差集(把当前list中不同的添加到历史中去,相同不添加)
               //新产生的报警都会在当前集合，再添加到历史集合中去
               //历史报警与当前报警作差集，得到的集合就是报警消除了的那个，再历史报警中去除，并在界面显示
               switch (num)
               {
                   case 16:
                       List<AlarmStruct> inter = CurrentAlarm.Except(HistoryAlarm_16).ToList();
                       foreach (AlarmStruct temp in inter)
                       {
                           HistoryAlarm_16.Add(temp);
                           //触发Err_Log发生新报警动作
                           
                            CGlbFunc.Error_time_last[num]= System.DateTime.Now.ToString("MM-dd HH:mm");
                           NC_Err_Log16.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                           ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                       }
                       break;
                   case 17:
                       inter = CurrentAlarm.Except(HistoryAlarm_17).ToList();
                       foreach (AlarmStruct temp in inter)
                       {
                           HistoryAlarm_17.Add(temp);
                           //触发Err_Log发生新报警动作
                           CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                           NC_Err_Log17.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                           ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                       }
                       break;
                   case 18:
                       inter = CurrentAlarm.Except(HistoryAlarm_18).ToList();
                       foreach (AlarmStruct temp in inter)
                       {
                           HistoryAlarm_18.Add(temp);
                           //触发Err_Log发生新报警动作
                           CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                           NC_Err_Log18.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                           ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                       }
                       break;
                   case 19:
                       inter = CurrentAlarm.Except(HistoryAlarm_19).ToList();
                       foreach (AlarmStruct temp in inter)
                       {
                           HistoryAlarm_19.Add(temp);
                           //触发Err_Log发生新报警动作
                           CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                           NC_Err_Log19.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                           ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                       }
                       break;
                   case 20:
                       inter = CurrentAlarm.Except(HistoryAlarm_20).ToList();
                       foreach (AlarmStruct temp in inter)
                       {
                           HistoryAlarm_20.Add(temp);
                           //触发Err_Log发生新报警动作
                           CGlbFunc.Error_time_last[num] = System.DateTime.Now.ToString("MM-dd HH:mm");
                           NC_Err_Log19.NC_Add_ErrLog(temp);  //更新Err_log的显示，发生报警
                           ErrorEnqueue(temp, Constants.ERRORFIND);  //报警入队等待上传服务器或者数据库
                       }
                       break;
                  
                   default:
                       break;
               }

           }         
            else
            {
                CurrentAlarm.Clear();  //
                real_data.intErr_flag = 0;           //当没有报警的时候Err提示灯变灰,报警号显示为0,报警文本清除
                real_data.intErr_num = "0";
                real_data.strErr_txt = null;
            }
           switch (num)
           {
               case 1:
                   List<AlarmStruct> inter2 = HistoryAlarm_1.Except(CurrentAlarm).ToList();
                   foreach (AlarmStruct temp in inter2)
                   {
                       HistoryAlarm_1.Remove(temp);
                       //触发报警清除动作
                       NC_Err_Log1.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                       ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                   }
                   break;
               case 2:
                   inter2 = HistoryAlarm_2.Except(CurrentAlarm).ToList();
                   foreach (AlarmStruct temp in inter2)
                   {
                       HistoryAlarm_2.Remove(temp);
                       //触发报警清除动作
                       NC_Err_Log2.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                       ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                   }
                   break;
               case 3:
                   inter2 = HistoryAlarm_3.Except(CurrentAlarm).ToList();
                   foreach (AlarmStruct temp in inter2)
                   {
                       HistoryAlarm_3.Remove(temp);
                       //触发报警清除动作
                       NC_Err_Log3.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                       ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                   }
                   break;
               case 4:
                   inter2 = HistoryAlarm_4.Except(CurrentAlarm).ToList();
                   foreach (AlarmStruct temp in inter2)
                   {
                       HistoryAlarm_4.Remove(temp);
                       //触发报警清除动作
                       NC_Err_Log4.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                       ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                   }
                   break;
               case 5:
                   inter2 = HistoryAlarm_5.Except(CurrentAlarm).ToList();
                   foreach (AlarmStruct temp in inter2)
                   {
                       HistoryAlarm_5.Remove(temp);
                       //触发报警清除动作
                       NC_Err_Log5.NC_Delete_ErrLog(temp);  //更新Err_log的显示，发生报警
                       ErrorEnqueue(temp, Constants.ERRORCLEAR);  //报警消除入队等待上传服务器或者数据库
                   }
                   break;
               
               default:
                   break;
           }
               #endregion
            }
            catch
            {
                MessageBox.Show("获取报警信息失败");
            }

            stoptime = GetTickCount();                                                        //在调用前后使用GetTickCount函数，获得轮循一台设备的时间
            real_data.dataPC_time_Mill = stoptime - timestart;
            real_data.dataPC_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff");//获得系统时间，当作时间戳
            run_info.time = real_data.dataPC_time;                                            //运行数据采集时间戳
            StatusData_user.WriteTxt(true, real_data);                                        //调用statusdata类的方法writetxt,写入txt
            real_data.strPower_time = CGlbFunc.Power_time_today[num];                        //每次都在更新开机时间
            /*打包数据完成 下一步run_info入队列，real_data 更新显示*/
            queue_state.Enqueue(run_info);

#if MARCO_TWINCAT

            ads.ADS_dataget(NC_ID, num, real_data, env_info);
            // ADS_dataget(NC_ID,num);    //调用函数获取twincat数据
#endif

            switch (num)
            {
                case 16:
                    statusData_user11.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 17:
                    statusData_user12.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 18:
                    statusData_user13.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 19:
                    statusData_user14.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                case 20:
                    statusData_user15.NC_StatusUpdate(real_data);  //更新显示的数据方法
                    break;
                default:
                    break;
            }



        }
       /*清除real_data实例数据的函数*/
        private void ClearRealData()
        {
            real_data.intComm_flag=0;
            real_data.intPower_flag=0;
            real_data.intErr_flag=0;   //控制三个灯的颜色
            real_data.strIPv4_txt = "000.000.000";
            real_data.strPower_time = "00-00 00:00:00";
            real_data.intSys_num = 0;
            real_data.dataPC_time = string.Empty;
            real_data.dataPC_time_Mill = 0;
            real_data.douHumidity = 0;
            real_data.douTemperature = 0;
            real_data.douVoltage_1ch = 0;
            real_data.douVoltage_2ch = 0;
            real_data.douVoltage_3ch = 0;
            real_data.intAxis_1 = 0;
            real_data.intAxis_1_err = 0;
            real_data.intAxis_2 = 0;
            real_data.intAxis_2_err = 0;
            real_data.intAxis_3 = 0;
            real_data.intAxis_3_err = 0;
            real_data.intAxis_Curr_1 = 0;
            real_data.intAxis_Curr_2 = 0;
            real_data.intAxis_Curr_3 = 0;
            real_data.intAxis_Curr_4 = 0;
            //real_data.intComm_flag = 0;
            //real_data.intErr_flag = 0;
            real_data.intErr_num = "0";
            real_data.intFeed_v = 0;
            //real_data.intPower_flag = 0;
            real_data.intProg_decode = 0;
            real_data.intProg_num = 0;
            real_data.intProg_running = 0;
            real_data.intSpindle_v = 0;
            //real_data.intSys_num = 0;
            real_data.strErr_time = string.Empty;
            real_data.strErr_txt = string.Empty;
            //real_data.strPower_time = string.Empty;
            real_data.strProg_Gcode = string.Empty;
            real_data.strState_nc = 0;


        }
        /* 报警入队函数*/
        void ErrorEnqueue(AlarmStruct temp,int FLAG)
        {
            err_info.id=temp.ID;
            err_info.no=temp.alarm_num;
            err_info.ctt=temp.alarm_text;
            err_info.time=System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff");//入队时间当作报警发生时间
            err_info.f = (Byte)FLAG;
            queue_err.Enqueue(err_info);
           
        }

        /*采集数据定时器*/
        private void timer1_Tick(object sender, EventArgs e)  /*定时器1s更新，实现状态值的重写和错误texbox的填充*/
        {
            Err_Log_All.Text = string.Empty;
            Msg_Log_All.Text = string.Empty;
            Warning_Log_All.Text = string.Empty;

            /*管理主界面的状态灯*/
            CGlbFunc.WebLinkFlag = 0;
            this.WebLink.BackColor = Color.Gainsboro;
            CGlbFunc.DbLinkFlag = 0;
            this.DBLink.BackColor = Color.Gainsboro;

                for (int i = 1; i <=5;i++ )
                {
                    if (CGlbFunc.LoginFlag[i] == Constants.LOGINFLAG ) //华中数控已经登陆成功，数量限制为5台
                    {
                        RegRefresh_ALL(CGlbFunc.clientNo[i]);    //更新textbox里面的故障与提示信息
                        Real_status_hz(i);
                    }
                    else
                    {
                        ClearRealData();
                         switch (i)
                        {
                            case 1:
                                NC_status_1.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 2:
                                NC_status_2.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 3:
                                NC_status_3.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 4:
                                NC_status_4.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 5:
                                NC_status_5.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            default:
                                break;
                        }
                    }
                }
                for (int i=6;i<=10;i++)
                {
                    if (CGlbFunc.LoginFlag[i] == Constants.LOGINFLAG)
                    {
                        Real_status_gsk(i);
                    }
                     else
                    {
                        ClearRealData();
                         switch (i)
                        {
                            case 6:
                                NC_status_6.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 7:
                                NC_status_7.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 8:
                                NC_status_8.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 9:
                                NC_status_9.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 10:
                                NC_status_10.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            default:
                                break;
                        }
                    }
                }
                for (int i = 11; i <= 15; i++)
                {
                    if (CGlbFunc.LoginFlag[i] == Constants.LOGINFLAG) //广州数控已经登陆成功，数量限制为5台
                    {
                        Real_status_gj(i);
                    }
                    else
                    {
                        ClearRealData();
                        switch (i)
                        {
                            case 11:
                                NC_status_11.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 12:
                                NC_status_12.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 13:
                                NC_status_13.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 14:
                                NC_status_14.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 15:
                                NC_status_15.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            default:
                                break;
                        }
                    }
                }
                //航天数控            
                for (int i = 16; i <= 20; i++)
                {
                    if (CGlbFunc.LoginFlag[i] == Constants.LOGINFLAG) //航天数控已经登陆成功，数量限制为5台
                    {
                        Real_status_ht(i);
                    }
                    else
                    {
                        ClearRealData();
                        switch (i)
                        {
                            case 16:
                                statusData_user11.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 12:
                                statusData_user12.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 13:
                                statusData_user13.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 14:
                                statusData_user14.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            case 15:
                                statusData_user15.NC_StatusUpdate(real_data);  //更新显示的数据方法
                                break;
                            default:
                                break;
                        }
                    }
                }                               
        }

        public void pushData(object obj)
        {        
            string rundata = obj.ToString();
            try
            {
                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_local, rundata);
                if (reponce == "error")
                    Console.WriteLine("接受错误");
                else
                    Console.WriteLine("云服务接受到数据，数据response={0}", reponce);
            }
            catch
            {
                Console.WriteLine("向云服务器传输数据出错");
            }           
        }
        Thread t;
        /*发送数据定时器*/
        private void timer_pushdata_Tick(object sender, EventArgs e)
        {
            #region 运行数据
            if (queue_state.Count != 0)
            {
                Console.WriteLine(queue_state.Count);   //调试输出队列内个数
                Data_Run_info temp=new Data_Run_info();
                queue_state.TryDequeue(out temp);
               
                string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                data_info.dt = "["+rundata+"]";  //加[]，打包发送
                data_info.did = Constants.RUNSTATE;
                rundata= WebSreverce_PostJson.ConvertToJson(data_info);
                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, rundata);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
                Thread t = new Thread(new ParameterizedThreadStart(pushData));
                t.Start(rundata);
                if (reponce == "ac")
                {
                    /*管理主界面的状态灯*/
                    CGlbFunc.WebLinkFlag = 1;
                    this.WebLink.BackColor = Color.Green;
                     //Console.WriteLine(rundata);
                    //返回值正确，服务器收到了请求
                }
                else
                {
                    /*管理主界面的状态灯*/
                    CGlbFunc.WebLinkFlag = 2;
                    this.WebLink.BackColor = Color.Red;

                    queue_state.Enqueue(temp); //出现错误，需要进行异常处理。处理方式：先将发送失败数据入队，等下次发送；当队列长度达到警戒值时，说明线路出现长时间故障，开启数据库临时存储计时器，开始写入数据库
                   //可以考虑设置postweb的提醒信号灯，出现阻塞以后颜色变化
                    
                    if (queue_state.Count > 70) //即30min，20台机子的运行数据条数
                    {
                        Console.WriteLine("数据缓存区达到警戒值"+queue_state.Count);
                        try
                        {
                            MyDataBase.con_open();
                        }
                        catch
                        {
                            MessageBox.Show("数据库连接失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        finally
                        {
                            timer_DBsave.Enabled = true;
                        }
                    }

                }
            }
            #endregion
            #region 报警错误
            if (queue_err.Count != 0)
            {
                Data_Err_info temp = new Data_Err_info();
                queue_err.TryDequeue(out temp);
                string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                data_info.dt = rundata;
                data_info.did = Constants.ERRORINFO;

                rundata = WebSreverce_PostJson.ConvertToJson(data_info);
                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, rundata);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
                if (reponce == "ac")
                {
                    /*管理主界面的状态灯*/
                    CGlbFunc.WebLinkFlag = 1;
                    this.WebLink.BackColor = Color.Green;

                    Console.WriteLine(rundata);
                    //返回值正确，服务器收到了请求
                }
                else
                {
                    /*管理主界面的状态灯*/
                    CGlbFunc.WebLinkFlag = 2;
                    this.WebLink.BackColor = Color.Red;

                    queue_err.Enqueue(temp); //出现错误，需要进行异常处理。处理方式：先将发送失败数据入队，等下次发送；当队列长度达到警戒值时，说明线路出现长时间故障，开启数据库临时存储计时器，开始写入数据库
                    //可以考虑设置postweb的提醒信号灯，出现阻塞以后颜色变化
                    if (queue_err.Count > 100) //报警条数可以一直做缓存
                    {
                        timer_DBsave.Enabled = true;
                    }

                }
            }
#endregion
            #region 登录登出
            if (queue_login.Count != 0)
            {
                Data_LogIn_info temp=new Data_LogIn_info();
                queue_login.TryDequeue(out temp);
                string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                data_info.dt = rundata;
                data_info.did = Constants.LOGIN;

                rundata = WebSreverce_PostJson.ConvertToJson(data_info);
                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, rundata);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
                //Console.WriteLine(reponce);
                if (reponce == "ac")
                {
                    /*管理主界面的状态灯*/
                    CGlbFunc.WebLinkFlag = 1;
                    this.WebLink.BackColor = Color.Green;

                    Console.WriteLine(rundata);
                    //返回值正确，服务器收到了请求
                }
                else
                {
                    /*管理主界面的状态灯*/
                    CGlbFunc.WebLinkFlag = 2;
                    this.WebLink.BackColor = Color.Red;

                    queue_login.Enqueue(temp); //出现错误，需要进行异常处理。处理方式：先将发送失败数据入队，等下次发送；当队列长度达到警戒值时，说明线路出现长时间故障，开启数据库临时存储计时器，开始写入数据库
                    //可以考虑设置postweb的提醒信号灯，出现阻塞以后颜色变化
                    if (queue_login.Count > 100) //做缓存
                    {
                        timer_DBsave.Enabled = true;
                    }

                }
            }
            #endregion
            #region 环境数据
            if (queue_env.Count != 0)
            {
                Data_Env_info temp=new Data_Env_info();
                queue_env.TryDequeue(out temp);
                string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                data_info.dt = rundata;
                data_info.did = Constants.ENVINFO;

                rundata = WebSreverce_PostJson.ConvertToJson(data_info);
                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, rundata);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
                //Console.WriteLine(reponce);
                if (reponce == "ac")
                {
                    /*管理主界面的状态灯*/
                    CGlbFunc.WebLinkFlag = 1;
                    this.WebLink.BackColor = Color.Green;

                    Console.WriteLine(rundata);
                    //返回值正确，服务器收到了请求
                }
                else
                {
                    /*管理主界面的状态灯*/
                    CGlbFunc.WebLinkFlag = 2;
                    this.WebLink.BackColor = Color.Red;

                    queue_env.Enqueue(temp); //出现错误，需要进行异常处理。处理方式：先将发送失败数据入队，等下次发送；当队列长度达到警戒值时，说明线路出现长时间故障，开启数据库临时存储计时器，开始写入数据库
                    //可以考虑设置postweb的提醒信号灯，出现阻塞以后颜色变化
                    if (queue_env.Count > 100) //做缓存
                    {
                        timer_DBsave.Enabled = true;
                    }

                }
            }
            #endregion
        }
        /*数据库存储开启定时器，作为一级缓存，从队列里面拿数据*/
        /*数据库设计：只是用两个字段，自增数字和Json字符串    */
        /*数据库设计修改：因为考虑后期的数据提取分析，所以按照详细数据项设计多表结构，自增数字列作为关键字    */       
        private void timer_DBsave_Tick(object sender, EventArgs e)
        {
            timer_pushdata.Enabled = false;//队列达到临界值，已经发生长时间断连，停止上传数据
#if MARCO_POSTWEB
            timer_checkCom.Enabled = true; //开启通信检查定时器，每1min询问一次通断
#endif
            if (queue_state.Count != 0)
            {
                /*管理主界面的状态灯*/
                CGlbFunc.DbLinkFlag = 1;
                this.DBLink.BackColor = Color.Green;

                CGlbFunc.WebLinkFlag = 2;
                this.WebLink.BackColor = Color.Red;

                Console.WriteLine(queue_state.Count); //
                Data_Run_info temp = new Data_Run_info();
                queue_state.TryDequeue(out temp);
                //string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                //data_info.strJsonData = rundata;
                //data_info.bdatatype = Constants.RUNSTATE;
                //rundata = WebSreverce_PostJson.ConvertToJson(data_info);
                MyDataBase.Insert(MyDataBase.My_Conn, "insert into tb_runstate(id,cas,ccs,aload,aspd1,aspd2,aspd3,aspd4,aspd5,apst1,apst2,apst3,apst4,apst5,cpst1,cpst2,cpst3,cpst4,cpst5,load1,load2,load3,load4,load5,pd,pn,ps,pl,pm,time) values('" + temp.id + "','" + temp.cas + "','" + temp.ccs + "','" + temp.aload + "','" + temp.aspd1 + "','" + temp.aspd2 + "','" + temp.aspd3 + "','" + temp.aspd4 + "','" + temp.aspd5 + "','" + temp.apst1 + "','" + temp.apst2 + "','" + temp.apst3 + "','" + temp.apst4 + "','" + temp.apst5 + "','" + temp.cpst1 + "','" + temp.cpst2 + "','" + temp.cpst3 + "','" + temp.cpst4 + "','" + temp.cpst5 + "','" + temp.load1 + "','" + temp.load2 + "','" + temp.load3 + "','" + temp.load4 + "','" + temp.load5 + "','" + temp.pd + "','" + temp.pn + "','" + temp.ps + "','" + temp.pl + "','" + temp.pm + "','" + temp.time + "')");
            }
            if (queue_err.Count != 0)
            {
                /*管理主界面的状态灯*/
                CGlbFunc.DbLinkFlag = 1;
                this.DBLink.BackColor = Color.Green;

                Data_Err_info temp = new Data_Err_info();
                queue_err.TryDequeue(out temp);
                //string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                //data_info.strJsonData = rundata;
                //data_info.bdatatype = Constants.ERRORINFO;
                //rundata = WebSreverce_PostJson.ConvertToJson(data_info);

                MyDataBase.Insert(MyDataBase.My_Conn, "insert into tb_error(id,time_flag,Err_num,Datatime,Err_txt) values('" + temp.id + "','" + temp.f + "','" + temp.no + "','" + temp.time + "','" + temp.ctt + "')");
            }
            if (queue_login.Count != 0)
            {
                /*管理主界面的状态灯*/
                CGlbFunc.DbLinkFlag = 1;
                this.DBLink.BackColor = Color.Green;

                Data_LogIn_info temp = new Data_LogIn_info();
                queue_login.TryDequeue(out temp);
                //string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                //data_info.strJsonData = rundata;
                //data_info.bdatatype = Constants.ERRORINFO;
                //rundata = WebSreverce_PostJson.ConvertToJson(data_info);

                MyDataBase.Insert(MyDataBase.My_Conn, "insert into tb_login(id,ontime,runtime,dt) values('" + temp.id + "','" + temp.ontime + "','" + temp.runtime + "','" + temp.time + "')");
            }
            if (queue_env.Count != 0)
            {
                /*管理主界面的状态灯*/
                CGlbFunc.DbLinkFlag = 1;
                this.DBLink.BackColor = Color.Green;

                Data_Env_info temp = new Data_Env_info();
                queue_env.TryDequeue(out temp);
                //string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                //data_info.strJsonData = rundata;
                //data_info.bdatatype = Constants.ERRORINFO;
                //rundata = WebSreverce_PostJson.ConvertToJson(data_info);

                MyDataBase.Insert(MyDataBase.My_Conn, "insert into tb_env(id,u,v,w,tp,hm,dt) values('" + temp.id + "','" + temp.u + "','" + temp.v + "','" + temp.w + "','" + temp.tep + "','" + temp.hmi + "','" + temp.time + "')");
            }

        }
        /*检查Web服务是否正常的定时器，每2s询问一次*/
        private void timer_checkCom_Tick(object sender, EventArgs e)
        {
            if (queue_state.Count != 0)
            {
                Data_Run_info temp = new Data_Run_info();
                queue_state.TryDequeue(out temp);
                string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                data_info.dt = "[" + rundata + "]";
                data_info.did = Constants.RUNSTATE;
                rundata = WebSreverce_PostJson.ConvertToJson(data_info);
                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, rundata);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
                Console.WriteLine(reponce);
                if (reponce == "ac")
                {
                    Console.WriteLine(rundata);
                    Console.WriteLine("返回值正确，服务器收到了请求");
                    //if 监测连接建立，就再次打开timer_pushdata 
                    timer_pushdata.Enabled = true;  //打开从本地缓存Push数据的定时器
                    timer_DBsave.Enabled = false;  //关闭存储数据库的定时器
                    timer_checkCom.Enabled = false;//关闭检查通信的定时器
                }
                else
                {
                    Console.WriteLine("返回值错误，服务器未建立连接");
                    /*管理主界面的状态灯*/
                    CGlbFunc.WebLinkFlag = 2;
                    this.WebLink.BackColor = Color.Red;

                    queue_state.Enqueue(temp);
                }
            }
            else/*如果队列为空，则使用空数据测试*/
            {
                Data_Run_info temp = new Data_Run_info();
                temp.id="0fffffffff";
                temp.time = "1988-02-29 10:10:10";
                string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                data_info.dt = "[" + rundata + "]";
                data_info.did = Constants.RUNSTATE;
                rundata = WebSreverce_PostJson.ConvertToJson(data_info);
                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, rundata);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
                Console.WriteLine(reponce);
                if (reponce == "ac")
                {
                    Console.WriteLine(rundata);
                    Console.WriteLine("返回值正确，服务器收到了请求");
                    //返回值正确，服务器收到了请求
                    //if 监测连接建立，就再次打开timer_pushdata 
                    timer_pushdata.Enabled = true;  //打开从本地缓存Push数据的定时器
                    timer_DBsave.Enabled = false;  //关闭存储数据库的定时器
                    timer_checkCom.Enabled = false;//关闭检查通信的定时器
                }
                else
                {
                    Console.WriteLine("返回值错误，服务器未建立连接");
                    /*管理主界面的状态灯*/
                    CGlbFunc.WebLinkFlag = 2;
                    this.WebLink.BackColor = Color.Red;

                }
            }

        }

        private void button36_Click(object sender, EventArgs e)  /*报警文本手动清除*/
        {
            Err_Log_All.Text = " ";
            textBox3.Text = " ";
        }


        private void button10_Click(object sender, EventArgs e)
        {
            listView_err.Items.Clear();
        }

        /* 实时状态按键响应函数                     */
        /*所有按键的响应函数是按sender的but名称确定的*/
        private void Real_statu_Click(object sender, EventArgs e)
        {
            using (NC_Status_Form status_detail = new NC_Status_Form())
            {
                Button but = sender as Button;
              
                if (but == NC1_realstatus_but)
                {
                    status_detail.intclientnum = 1;
                }
                if (but == NC2_realstatus_but)
                {
                    status_detail.intclientnum = 2;
                }
                if (but == NC3_realstatus_but)
                {
                    status_detail.intclientnum = 3;
                }
                if (but == NC4_realstatus_but)
                {
                    status_detail.intclientnum = 4;
                }
                    if (but == NC5_realstatus_but)
                {
                    status_detail.intclientnum = 5;
                }
                status_detail.ShowDialog();

            }
        }

        /* 实时状态按键响应函数                     */
        /*所有按键的响应函数是按sender的but名称确定的*/
        private void Real_graph_Click(object sender, EventArgs e)
        {
            using (frmRealGraph status_detail = new frmRealGraph())
            {
                Button but = sender as Button;
                

                if (but == NC1_xls_but)
                {
                    //status_detail.intclientnum = 1;
                    status_detail.getclientnum(0);
                }
                if (but == NC2_xls_but)
                {
                    //status_detail.intclientnum = 2;
                    status_detail.getclientnum(1);
                }
                if (but == NC3_xls_but)
                {
                   // status_detail.intclientnum = 3;
                    status_detail.getclientnum(2);
                }
                if (but == NC4_xls_but)
                {
                   // status_detail.NC2_xls_but = 4;
                    status_detail.getclientnum(3);
                }
                if (but == NC5_xls_but)
                {
                    //status_detail.intclientnum = 5;
                    status_detail.getclientnum(4);
                }
                status_detail.ShowDialog();

            }
        }
        ///*  图表生成按键响应函数  */
        //private void Data_xls_Click(object sender, EventArgs e)
        //{
        //    using (TwinCAT_ADS twincat_data = new TwinCAT_ADS())
        //    {
        //        twincat_data.ShowDialog();

        //    }
        //}
        /*显示历史报警履历的窗口*/
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string startime_txt = string.Empty;
            string endtime_txt = string.Empty;
           
            int i = 0;
            int hisAlarmNum = 0;
            listView_err.Items.Clear();
            switch (Selc_equi_comboBox1.Text)
            {
                case "华中1":  
                    int ret = HncApi.HNC_AlarmGetHistoryNum(ref hisAlarmNum, CGlbFunc.clientNo[1]);
                    if (ret == 0)   //获取成功
                    {
                        int index = 0;      //index取值范围为 0~num-1，num是历史报警个数
                        int count = hisAlarmNum;
                        AlarmHisData[] historyData = new AlarmHisData[AlarmDef.ALARM_HISTORY_MAX_NUM];
                        HncApi.HNC_AlarmGetHistoryData(index, //从第index个报警历史开始获取
                                                               ref count, //（传入）共获取count个报警历史
                            //（传出）实际获取的报警历史个数
                                                               historyData,//历史报警内容：包括报警号、产生时间、消除时间和文本
                                                                CGlbFunc.clientNo[2]);
                        for (i = 0; i < hisAlarmNum; i++)
                        {
                            ListViewItem lvi = new ListViewItem();
                            startime_txt = historyData[i].timeBegin.year.ToString() + "年" + historyData[i].timeBegin.month.ToString() + "月" +
                                         historyData[i].timeBegin.day.ToString() + "日" + historyData[i].timeBegin.hour.ToString() + ":" +
                                         historyData[i].timeBegin.minute.ToString();
                            endtime_txt = historyData[i].timeEnd.year.ToString() + "年" + historyData[i].timeEnd.month.ToString() + "月" +
                                         historyData[i].timeEnd.day.ToString() + "日" + historyData[i].timeEnd.hour.ToString() + ":" +
                                         historyData[i].timeEnd.minute.ToString();
                            lvi.Text = startime_txt;
                            lvi.SubItems.Add(endtime_txt);
                            lvi.SubItems.Add(historyData[i].alarmNo.ToString());
                            lvi.SubItems.Add(historyData[i].text);
                            listView_err.Items.Add(lvi);
                        }

                    }
                    break;
                case "华中2":
                     ret = HncApi.HNC_AlarmGetHistoryNum(ref hisAlarmNum, CGlbFunc.clientNo[2]);
                    if (ret == 0)   //获取成功
                    {
                        int index = 0;      //index取值范围为 0~num-1，num是历史报警个数
                        int count = hisAlarmNum;
                        AlarmHisData[] historyData = new AlarmHisData[AlarmDef.ALARM_HISTORY_MAX_NUM];
                        HncApi.HNC_AlarmGetHistoryData(index, //从第index个报警历史开始获取
                                                               ref count, //（传入）共获取count个报警历史
                            //（传出）实际获取的报警历史个数
                                                               historyData,//历史报警内容：包括报警号、产生时间、消除时间和文本
                                                                CGlbFunc.clientNo[1]);
                        for (i = 0; i < hisAlarmNum; i++)
                        {
                            ListViewItem lvi = new ListViewItem();
                            startime_txt = historyData[i].timeBegin.year.ToString() + "年" + historyData[i].timeBegin.month.ToString() + "月" +
                                         historyData[i].timeBegin.day.ToString() + "日" + historyData[i].timeBegin.hour.ToString() + ":" +
                                         historyData[i].timeBegin.minute.ToString();
                            endtime_txt = historyData[i].timeEnd.year.ToString() + "年" + historyData[i].timeEnd.month.ToString() + "月" +
                                         historyData[i].timeEnd.day.ToString() + "日" + historyData[i].timeEnd.hour.ToString() + ":" +
                                         historyData[i].timeEnd.minute.ToString();
                            lvi.Text = startime_txt;
                            lvi.SubItems.Add(endtime_txt);
                            lvi.SubItems.Add(historyData[i].alarmNo.ToString());
                            lvi.SubItems.Add(historyData[i].text);
                            listView_err.Items.Add(lvi);
                        }

                    }
                    break;
                case "华中3":
                    ret = HncApi.HNC_AlarmGetHistoryNum(ref hisAlarmNum, CGlbFunc.clientNo[3]);
                    if (ret == 0)   //获取成功
                    {
                        int index = 0;      //index取值范围为 0~num-1，num是历史报警个数
                        int count = hisAlarmNum;
                        AlarmHisData[] historyData = new AlarmHisData[AlarmDef.ALARM_HISTORY_MAX_NUM];
                        HncApi.HNC_AlarmGetHistoryData(index, //从第index个报警历史开始获取
                                                               ref count, //（传入）共获取count个报警历史
                            //（传出）实际获取的报警历史个数
                                                               historyData,//历史报警内容：包括报警号、产生时间、消除时间和文本
                                                                CGlbFunc.clientNo[2]);
                        for (i = 0; i < hisAlarmNum; i++)
                        {
                            ListViewItem lvi = new ListViewItem();
                            startime_txt = historyData[i].timeBegin.year.ToString() + "年" + historyData[i].timeBegin.month.ToString() + "月" +
                                         historyData[i].timeBegin.day.ToString() + "日" + historyData[i].timeBegin.hour.ToString() + ":" +
                                         historyData[i].timeBegin.minute.ToString();
                            endtime_txt = historyData[i].timeEnd.year.ToString() + "年" + historyData[i].timeEnd.month.ToString() + "月" +
                                         historyData[i].timeEnd.day.ToString() + "日" + historyData[i].timeEnd.hour.ToString() + ":" +
                                         historyData[i].timeEnd.minute.ToString();
                            lvi.Text = startime_txt;
                            lvi.SubItems.Add(endtime_txt);
                            lvi.SubItems.Add(historyData[i].alarmNo.ToString());
                            lvi.SubItems.Add(historyData[i].text);
                            listView_err.Items.Add(lvi);
                        }

                    }
                    break;
                case "华中4":
                    ret = HncApi.HNC_AlarmGetHistoryNum(ref hisAlarmNum, CGlbFunc.clientNo[4]);
                    if (ret == 0)   //获取成功
                    {
                        int index = 0;      //index取值范围为 0~num-1，num是历史报警个数
                        int count = hisAlarmNum;
                        AlarmHisData[] historyData = new AlarmHisData[AlarmDef.ALARM_HISTORY_MAX_NUM];
                        HncApi.HNC_AlarmGetHistoryData(index, //从第index个报警历史开始获取
                                                               ref count, //（传入）共获取count个报警历史
                            //（传出）实际获取的报警历史个数
                                                               historyData,//历史报警内容：包括报警号、产生时间、消除时间和文本
                                                                CGlbFunc.clientNo[2]);
                        for (i = 0; i < hisAlarmNum; i++)
                        {
                            ListViewItem lvi = new ListViewItem();
                            startime_txt = historyData[i].timeBegin.year.ToString() + "年" + historyData[i].timeBegin.month.ToString() + "月" +
                                         historyData[i].timeBegin.day.ToString() + "日" + historyData[i].timeBegin.hour.ToString() + ":" +
                                         historyData[i].timeBegin.minute.ToString();
                            endtime_txt = historyData[i].timeEnd.year.ToString() + "年" + historyData[i].timeEnd.month.ToString() + "月" +
                                         historyData[i].timeEnd.day.ToString() + "日" + historyData[i].timeEnd.hour.ToString() + ":" +
                                         historyData[i].timeEnd.minute.ToString();
                            lvi.Text = startime_txt;
                            lvi.SubItems.Add(endtime_txt);
                            lvi.SubItems.Add(historyData[i].alarmNo.ToString());
                            lvi.SubItems.Add(historyData[i].text);
                            listView_err.Items.Add(lvi);
                        }

                    }
                    break;
                case "华中5":
                    ret = HncApi.HNC_AlarmGetHistoryNum(ref hisAlarmNum, CGlbFunc.clientNo[5]);
                    if (ret == 0)   //获取成功
                    {
                        int index = 0;      //index取值范围为 0~num-1，num是历史报警个数
                        int count = hisAlarmNum;
                        AlarmHisData[] historyData = new AlarmHisData[AlarmDef.ALARM_HISTORY_MAX_NUM];
                        HncApi.HNC_AlarmGetHistoryData(index, //从第index个报警历史开始获取
                                                               ref count, //（传入）共获取count个报警历史
                            //（传出）实际获取的报警历史个数
                                                               historyData,//历史报警内容：包括报警号、产生时间、消除时间和文本
                                                                CGlbFunc.clientNo[2]);
                        for (i = 0; i < hisAlarmNum; i++)
                        {
                            ListViewItem lvi = new ListViewItem();
                            startime_txt = historyData[i].timeBegin.year.ToString() + "年" + historyData[i].timeBegin.month.ToString() + "月" +
                                         historyData[i].timeBegin.day.ToString() + "日" + historyData[i].timeBegin.hour.ToString() + ":" +
                                         historyData[i].timeBegin.minute.ToString();
                            endtime_txt = historyData[i].timeEnd.year.ToString() + "年" + historyData[i].timeEnd.month.ToString() + "月" +
                                         historyData[i].timeEnd.day.ToString() + "日" + historyData[i].timeEnd.hour.ToString() + ":" +
                                         historyData[i].timeEnd.minute.ToString();
                            lvi.Text = startime_txt;
                            lvi.SubItems.Add(endtime_txt);
                            lvi.SubItems.Add(historyData[i].alarmNo.ToString());
                            lvi.SubItems.Add(historyData[i].text);
                            listView_err.Items.Add(lvi);
                        }

                    }
                    break;
                default :
                    break;
            }


        }

        private void 删除网络ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmManageLine inf = new frmManageLine())
            {
                inf.ShowDialog();
            }
            //manageLine line = new manageLine();
            //line.Show();
            //manageLine 
        }

        /*twincat ads 读取数据过程*/
        //定义结构体类型
        #region 抽取ads模块
        /*
        public struct ADS_struct
        {
            public double tmp;  //温度
            public double humi;  //湿度
            public double u;  //1#电压
            public double v;   //2#电压
            public double w;   //3#电压

        }
        //实例化结构体
        private ADS_struct structtest = new ADS_struct();

        //定义句柄变量
        private int hvar = new int();
        //通讯数据定义
        private TcAdsClient tcclient;//定义通讯协议



        private void TwinCAT_ADS_LOAD()
        {
            //通讯协议
            tcclient = new TcAdsClient();
            tcclient.Connect(801);
           
        }
        private double Calc_temper(double adsdata)
        {
            return (50 * adsdata / 10);

        }
        private double Calc_humid(double adsdata)
        {
            return (100 / 10 * adsdata);

        }
        private double Calc_voge(double adsdata)
        {
            return (380 / 8 * adsdata);

        }

        private void  ADS_dataget( string sid,int num)
        {
            if (num == 4)
            {
                try
                {
                    hvar = tcclient.CreateVariableHandle("MAIN.adsstru");
                }
                catch (Exception err)
                {
                    MessageBox.Show("ADS get hvar error");

                }
                AdsStream datastream = new AdsStream(40);// ads 字节流 5*8=40
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;

                try
                {
                    tcclient.Read(hvar, datastream);
                    structtest.tmp = binread.ReadDouble();  //获取的5个信号量，由于电压传感器还未接入
                    structtest.humi = binread.ReadDouble();
                    structtest.u = binread.ReadDouble();
                    structtest.v = binread.ReadDouble();
                    structtest.w = binread.ReadDouble();
                    real_data.douTemperature = Calc_temper(structtest.tmp);
                    real_data.douHumidity = Calc_humid(structtest.humi);
                    real_data.douVoltage_1ch = Calc_voge(structtest.u);
                    real_data.douVoltage_2ch = Calc_voge(structtest.v);
                    real_data.douVoltage_3ch = Calc_voge(structtest.w);
                    env_info.hmi = (float)real_data.douHumidity;
                    env_info.tep = (float)real_data.douTemperature;
                    env_info.u = (float)real_data.douVoltage_1ch;
                    env_info.v = (float)real_data.douVoltage_2ch;
                    env_info.w = (float)real_data.douVoltage_3ch;
                    env_info.id = sid;
                    env_info.time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff");//获得系统时间，当作时间戳

                    queue_env.Enqueue(env_info);
                    //Console.WriteLine(env_info.hmi+"  "+env_info.tep+"   "+env_info.u);

                }

                catch (Exception err)
                {
                    MessageBox.Show("ADS read value error");
                }
                try
                {
                    tcclient.DeleteVariableHandle(hvar);
                }
                catch (Exception err)
                {
                    MessageBox.Show("ADS delect hvar error");
                }
            }
        }*/
        #endregion
         
        /*窗体控制 按照无边界方式加载窗体，并且开启keypreviou功能，当监测到Esc事件以后，退出最大化模式，显示关闭窗体图标*/
        private void FormMain_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            timer_update.Enabled = true;
#if MARCO_POSTWEB
            timer_pushdata.Enabled = true;     
             //开启发送定时器，20ms发送一次
#endif
#if MARCO_DB
           timer_DBsave.Enabled = true;  //开启DBsave定时器，20毫秒轮询一遍队列
#endif
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void 系统注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddnc cnc = new frmAddnc();
            cnc.Show();
        }

        private void 上传ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmdbPushData cnc = new frmdbPushData();
            cnc.Show();
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {   /*一般在需要关闭软件的时候，需要将内存里面的数据全部上传，保证数据的完整性*/
            timer_pushdata.Enabled = false;//停止上传数据
            timer_checkCom.Enabled = false; //停止通信检查定时器，每1min询问一次通断
            if (queue_state.Count != 0 || queue_login.Count != 0 || queue_err.Count != 0)
            {
                timer_DBsave.Enabled = true;//打开数据库存储定时器，将一级内存缓存的数据上传
            }
            else
            {
                MessageBox.Show("上传完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                                  
        }

        private void 系统升级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdatenc cnc = new frmUpdatenc();
            cnc.Show();
        }


        //每天自动登录登出
        public static int flag = 0;//未登录
        frmManageLine fr = new frmManageLine();
        private void timer_LogInOut_Tick(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;//现在的时间
            DateTime dtin = Convert.ToDateTime("08:20");
            DateTime dtout = Convert.ToDateTime("17:20");
            if (DateTime.Compare(date, dtin) > 0 && flag == 0)
            {
                flag = 1;
                Console.WriteLine("登入系统");
                fr.login_all();
                
            }
            if (DateTime.Compare(date, dtout) > 0 && flag == 1)
            {
                flag = 0;
                Console.WriteLine("登出系统");
                fr.logout_all();
            }
        }
      

      
        

        

        

       

        

       
       

       
        
       

       


      

       




   
        



        //private void button18_Click(object sender, EventArgs e)
        //{
        //    int ret = 0;
        //    int value = 0;
        //    listView_statue.Items.Clear();
        //    foreach (HncAxis type in Enum.GetValues(typeof(HncAxis)))
        //    {
        //        ret = HncApi.HNC_AxisGetValue((int)type, int.Parse(comboBox_axisnum.Text), ref value, CGlbFunc.clientNo[0]);
        //        if (ret == 0)
        //        {
        //            ListViewItem lvi = new ListViewItem();
        //            lvi.Text = type.ToString();
        //            lvi.SubItems.Add(value.ToString());
        //            listView_statue.Items.Add(lvi);
        //        }
        //        else
        //        {
        //            MessageBox.Show("IP" + CGlbFunc.ip_info[0] + "失败");
        //            break;

        //        }
        //        Axis_position.Text += (type.ToString() + value.ToString() + System.Environment.NewLine);
        //    }
        //}
      
        

    }
}
