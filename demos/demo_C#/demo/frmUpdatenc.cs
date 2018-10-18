#define MARCO_POSTWEB
//#undef  MARCO_POSTWEB

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
using System.Runtime.InteropServices;


namespace demo
{
    public partial class frmUpdatenc : Form
    {
        static public string ip = " ";
        static public ushort port = 21;
        private short clientno = -1;
        private Int32 gjclient = -1;
        private IntPtr gskTocket=IntPtr.Zero;
        Data_regist_info newnc = new Data_regist_info();

        public frmUpdatenc()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIP.Text.Trim()) || string.IsNullOrWhiteSpace(txtPort.Text.Trim()))
            {
                MessageBox.Show("请输入IP地址和端口号！");
                return;
            }
            else
            {
                ip = txtIP.Text;
                port = Convert.ToUInt16(txtPort.Text);
                if (this.txtFac.Text == "华中数控")
                {
                    clientno = HncApi.HNC_NetConnect(ip, port);
                    if (clientno >= 0)
                    {
                        this.Test.Text = "连接成功";
                        this.Test.BackColor = Color.Green;
                        UpdateTheText_hz();
                    }
                    else
                    {
                        this.Test.Text = "测试连接";
                        this.Test.BackColor = Color.WhiteSmoke;
                        MessageBox.Show("连接失败，请检查后重试！");
                    }
                }
                else if (this.txtFac.Text == "广州数控")
                {
                    IPAddress addr = IPAddress.Parse(ip);
                    byte[] ip_by = addr.GetAddressBytes();
                    //Byte[] ipadd = System.Text.Encoding.Default.GetBytes("192.168.188.121");
                    gskTocket = GskApi.GSKRM_CreateInstance(ref ip_by[0], 1);
                    if (gskTocket == IntPtr.Zero)
                    {
                        this.Test.Text = "测试连接";
                        this.Test.BackColor = Color.WhiteSmoke;
                        MessageBox.Show("连接失败，请检查后重试！");

                    }
                    else
                    {
                        Console.WriteLine("Connect successed");
                        this.Test.Text = "连接成功";
                        this.Test.BackColor = Color.Green;
                        UpdateTheText_gsk();

                    }
                }
                else if (this.txtFac.Text == "沈阳高精")
                {
                    char[] ipaddr = ip.ToCharArray();
                    Int32 retval = GjApi.connect430ToNC(ref gjclient, ipaddr, port);
                    if (retval == 0)
                    {
                        Console.WriteLine("gaojing Connect successed");
                        this.Test.Text = "连接成功";
                        this.Test.BackColor = Color.Green;
                        UpdateTheText_gj();
                    }
                    else
                    {
                        this.Test.Text = "测试连接";
                        this.Test.BackColor = Color.WhiteSmoke;
                        MessageBox.Show("连接失败，请检查后重试！");
                    }
                }
                else
                {
                    MessageBox.Show("请输入正确的厂商名称");
                }
            }

        }

        private void UpdateTheText_hz()
        {
            int ret32 = 0;
            int value32 = 0;
            string strrusult = "";
            this.txtnum.Text = clientno.ToString();
            newnc.shrNCno = clientno;
            /*系统ID*/
            ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_SN_NUM, ref strrusult, clientno);
            if (ret32 == 0)
            {
                this.txtid.Text = strrusult;
                newnc.strNC_ID=strrusult;
            }
            /*版本号*/
            ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_NCK_VER, ref strrusult, clientno);
            if (ret32 == 0)
            {
                this.txtver1.Text = strrusult;
                newnc. strNC_Version_Num_1=strrusult;
            }
            ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_DRV_VER, ref strrusult, clientno);
            if (ret32 == 0)
            {
                this.txtver2.Text = strrusult;
                newnc.strNC_Version_Num_2=strrusult;
            }
            ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_PLC_VER, ref strrusult, clientno);
            if (ret32 == 0)
            {
                this.txtver3.Text = strrusult;
                newnc.strNC_Version_Num_3=strrusult;
            }
            ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_CNC_VER, ref value32, clientno);
            if (ret32 == 0)
            {
                this.txtver4.Text = value32.ToString();
                newnc.strNC_Version_Num_4 = value32.ToString();
            }
            newnc.tDatatime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss:ffff");
            this.txtdt.Text = newnc.tDatatime;
            newnc.strNC_Num = this.txtxinhao.Text;
            newnc.strNC_factr = this.txtFac.Text;
            newnc.strIP = ip;
            newnc.strPort = port;
            newnc.strNC_state = "未登录";
        }

        private void UpdateTheText_gsk()
        {
            Byte[] aCNCtype=new Byte[20];
            IntPtr retptr = new IntPtr();
            Int32 ret = GskApi.GSKRM_GetConnectState(gskTocket);
            if (ret == 1)
            {
                GskApi.GSKRM_GetCncTypeName(gskTocket, aCNCtype);  //型号读取接口会报错
                retptr = InfoApi.GSKRM_GetVersionInfo(gskTocket);
                try
                {
                    VERS_INFO structure = new VERS_INFO();
                    //int size = Marshal.SizeOf(typeof(VERS_INFO));
                    //IntPtr allocIntPtr = Marshal.AllocHGlobal(size);          //分配内存，返回一个指针
                    structure = (VERS_INFO)Marshal.PtrToStructure(retptr, typeof(VERS_INFO));
                    newnc.strNC_ID = structure.softWareNumber;
                    this.txtid.Text = newnc.strNC_ID;
                    newnc.strNC_Version_Num_1 = structure.armVersion;
                    this.txtver1.Text = newnc.strNC_Version_Num_1;
                    newnc.strNC_Version_Num_2 = structure.dspVersion;
                    this.txtver2.Text = newnc.strNC_Version_Num_2;
                    newnc.strNC_Version_Num_3 = structure.FPGAVersion;
                    this.txtver3.Text = newnc.strNC_Version_Num_3;
                    newnc.strNC_Version_Num_4 = structure.hardVersion;
                    this.txtver4.Text = newnc.strNC_Version_Num_4;
                    newnc.strNC_Version_Num_5 = structure.sysVersion;
                    this.txtver5.Text = newnc.strNC_Version_Num_5;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    GskApi.GSKRM_CloseInstance(gskTocket);
                }
                this.txtxinhao.Text = System.Text.Encoding.Default.GetString(aCNCtype);
                newnc.tDatatime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff");
                this.txtdt.Text = newnc.tDatatime;
                newnc.strNC_Num = this.txtxinhao.Text;
                newnc.strNC_factr = this.txtFac.Text;
                newnc.strIP = ip;
                newnc.strPort = port;
                newnc.strNC_state = "未登录";
            }
            else
            {
                MessageBox.Show("连接断开");
            }

            
 
        }

        private void  UpdateTheText_gj()
        {
            int ret32 = 0;
            StringBuilder strrusult = new StringBuilder();
            this.txtnum.Text = gjclient.ToString();
            newnc.shrNCno = (short)gjclient;
            /*系统ID*/
            this.txtid.Text = "G" + ip.Remove(0, 4);
            newnc.strNC_ID = "G" + ip.Remove(0, 4);
            /*版本号1  软件版本号*/
            ret32 = GjApi.get430StatusStaticVal(gjclient, 2541, strrusult);
            if (ret32 == 0)
            {
                this.txtver1.Text = strrusult.ToString();
                newnc.strNC_Version_Num_1 = strrusult.ToString();
            }
            /*版本号2  硬件版本号*/
            ret32 = GjApi.get430StatusStaticVal(gjclient, 2543, strrusult);
            if (ret32 == 0)
            {
                this.txtver2.Text = strrusult.ToString();
                newnc.strNC_Version_Num_2 = strrusult.ToString();
            }
            /*版本号3  系统内核版本号*/
            ret32 = GjApi.get430StatusStaticVal(gjclient, 2545, strrusult);
            if (ret32 == 0)
            {
                this.txtver3.Text = strrusult.ToString();
                newnc.strNC_Version_Num_3 = strrusult.ToString();
            }
            //ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_DRV_VER, ref strrusult, clientno);
            //if (ret32 == 0)
            //{
            //    this.txtver2.Text = strrusult;
            //    newnc.strNC_Version_Num_2 = strrusult;
            //}
            //ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_PLC_VER, ref strrusult, clientno);
            //if (ret32 == 0)
            //{
            //    this.txtver3.Text = strrusult;
            //    newnc.strNC_Version_Num_3 = strrusult;
            //}
            //ret32 = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_CNC_VER, ref value32, clientno);
            //if (ret32 == 0)
            //{
            //    this.txtver4.Text = value32.ToString();
            //    newnc.strNC_Version_Num_4 = value32.ToString();
            //}
            /*型号*/
            ret32 = GjApi.get430StatusStaticVal(gjclient, 2541, strrusult);
            if (ret32 == 0)
            {
                this.txtxinhao.Text = strrusult.ToString();
                newnc.strNC_Num = strrusult.ToString();
            }
            newnc.tDatatime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff");
            this.txtdt.Text = newnc.tDatatime;

            newnc.strNC_factr = this.txtFac.Text;
            newnc.strIP = ip;
            newnc.strPort = port;
            newnc.strNC_state = "未登录";
        }
        private void reset_Click(object sender, EventArgs e)
        {
            this.txtIP.Text = "192.168.10.0";
            this.txtPort.Text = "21";
            this.txtid.Text = "";
            this.txtFac.Text = "";
            this.txtnum.Text = "";
            this.txtver1.Text = "";
            this.txtver2.Text = "";
            this.txtver3.Text = "";
            this.txtver4.Text = "";
            this.txtver5.Text = "";
            this.txtxinhao.Text = "";
            this.Test.BackColor = Color.WhiteSmoke;
            this.Test.Text = "测试连接";
            this.txtdt.Text = "";

        }

        private void update_Click(object sender, EventArgs e)
        {
            DataBase addnc = new DataBase();
            NC_Version_Num version = new NC_Version_Num();
            MySqlConnection sqlcon = new MySqlConnection(addnc.M_str_sqlcon);
            //MySqlDataReader reader = null;
           // string select = "select * from tb_nc where id='"+newnc.strNC_ID+"'";
            //MySqlCommand cmd = new MySqlCommand(select, sqlcon);
            sqlcon.Open();
            //reader = cmd.ExecuteReader();
            //if (reader.Read())
            //{
             //   reader.Close();
             //   sqlcon.Close();
              //  MessageBox.Show("该ID已经注册,确定进行系统升级？");
              //  reset_Click();
            //}
           // else
            {
               // reader.Close();//NCnum编号为short ,采用人工写入，后期可以考虑删除
                string comm = "update tb_nc set state='" + newnc.strNC_state + "',id='" + newnc.strNC_ID + "',ip='" + newnc.strIP + "',port='" + newnc.strPort + "',fctr='" + newnc.strNC_factr + "',cltno='" + newnc.shrNCno + "',tp='" + this.txtxinhao.Text + "',ver1='" + newnc.strNC_Version_Num_1 + "',ver2='" + newnc.strNC_Version_Num_2 + "',ver3='" + newnc.strNC_Version_Num_3 + "',ver4='" + newnc.strNC_Version_Num_4 + "',ver5='" + newnc.strNC_Version_Num_5 + "',dt='" + newnc.tDatatime + "' where id='" + newnc.strNC_ID + "'";
                MySqlCommand cmd = new MySqlCommand(comm, sqlcon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                sqlcon.Close();
#if (MARCO_POSTWEB)
                PushToWeb();
#else
                Console.WriteLine(newnc.strNC_ID);
                MessageBox.Show("注册成功");
#endif
             
            }
            MessageBox.Show("系统升级成功");
            
          
        }
        private void PushToWeb()
        {
            NC_Version_Num version = new NC_Version_Num();  //保存版本信息的jason字符串
            Data_json data_info = new Data_json();         //上传WEB SERVICE的一级数据结构
            Data_Identity_info data_idten = new Data_Identity_info(); //上传WEB SERVICE的二级数据结构
            string jsonIdentity = "";                                 //最终上传WEB 的JSON字符串

            version.strNC_Version_Num_1 = newnc.strNC_Version_Num_1;
            version.strNC_Version_Num_2 = newnc.strNC_Version_Num_2;
            version.strNC_Version_Num_3 = newnc.strNC_Version_Num_3;
            version.strNC_Version_Num_4 = newnc.strNC_Version_Num_4;
            version.strNC_Version_Num_5 = newnc.strNC_Version_Num_5;
            string strversion = WebSreverce_PostJson.ConvertToJson(version);
            data_idten.id = newnc.strNC_ID;
            data_idten.tp = newnc.strNC_Num;
            data_idten.ver = strversion;
            strversion = WebSreverce_PostJson.ConvertToJson(data_idten);
            data_info.dt = strversion; 
            data_info.did = Constants.IDENTITY;

            jsonIdentity = WebSreverce_PostJson.ConvertToJson(data_info);
            string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, jsonIdentity);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
            //Console.WriteLine(reponce);
            if (reponce == "ac")
            {
                Console.WriteLine(jsonIdentity);
                this.Test.BackColor = Color.WhiteSmoke;;
                //返回值正确，服务器收到了请求
                MessageBox.Show("Web服务器注册成功");
                reset_Click();
            }
            else
            {
                MessageBox.Show("注册失败，请检查与Web服务器的连接");
                reset_Click();
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            //this.txtIP.Text = "";
            //this.txtPort.Text = "";
            //this.txtid.Text = "";
            //this.txtFac.Text = "";
            //this.txtnum.Text = "";
            //this.txtver1.Text = "";
            //this.txtver2.Text = "";
            //this.txtver3.Text = "";
            //this.txtver4.Text = "";
            //this.txtver5.Text = "";
            //this.txtxinhao.Text = "";

            //newnc = null;

            this.Close();
        }

        private void reset_Click()
        {
            this.txtIP.Text = "192.168.188.0";
            this.txtPort.Text = "21";
            this.txtid.Text = "";
            this.txtFac.Text = "";
            this.txtnum.Text = "";
            this.txtver1.Text = "";
            this.txtver2.Text = "";
            this.txtver3.Text = "";
            this.txtver4.Text = "";
            this.txtver5.Text = "";
            this.txtxinhao.Text = "";
            this.Test.BackColor = Color.WhiteSmoke;
            this.Test.Text = "测试连接";
            this.txtdt.Text = "";
        }


       
    }
}
