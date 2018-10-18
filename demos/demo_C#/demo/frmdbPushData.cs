using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
//using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace demo
{
    public partial class frmdbPushData : Form
    {
        private  string[] tblist=new string[]
        { 
            "运行数据表",
            "登入登出表",
            "报警表",
            "环境数据表"
        };
        private string[] tbname = new string[]
        { 
            "tb_runstate",
            "tb_login",
            "tb_error",
            "tb_env"
        };
        private  Int32[] tb_count =new Int32[4];
        public frmdbPushData()
        {
            InitializeComponent();
            DataBase pdata = new DataBase();
            //pdata.getcon();
            int i = 0;
            for (i = 0; i < 4; i++)
            {
                string comm = " select concat(round(sum(DATA_LENGTH)/1024,2),'KB')  as data from  information_schema.tables    where table_schema='db_data' AND table_name='"+tbname[i]+"'";   //查询表大小
                string comm2 = "select count(1) from " + tbname[i] + "";  //获取数据条数
                MySqlDataReader reader2 = pdata.Select(comm2);
                MySqlDataReader reader = pdata.Select(comm);
                ListViewItem lvi = new ListViewItem();
                lvi.Text = tblist[i];
                tb_count[i] = int.Parse(reader2[0].ToString());

                lvi.SubItems.Add(reader2[0].ToString());
                lvi.SubItems.Add(reader[0].ToString());
                db_tblist.Items.Add(lvi);

                /*关闭连接，关闭datareader*/
                reader.Close();
                reader2.Close();
                pdata.con_close();
            }
           
        }

        private void pushdata_Click(object sender, EventArgs e)
        {
            if (db_tblist.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选中数据表再上传！");
            }
            else
            {
                if (MessageBox.Show("确定上传选中数据表吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (db_tblist.SelectedItems[0].Text == "运行数据表")
                    {
                        if (tb_count[0] == 0)
                        {
                            MessageBox.Show("数据表为空，不需要上传");
                        }
                        else
                        {
                            db_progressBar.Value = 1;
                            db_progressBar.Minimum = 0;
                            db_progressBar.Maximum = tb_count[0];  

                            for (int i = 0; tb_count[0] - 3 * i > 0; i++)
                            {
                                List<Data_Run_info> result;
                                DataBase pdata = new DataBase();

                                /*MySqlDataReader  to list */

                                string comm3 = "select id,cas,ccs,aload,aspd1,aspd2,aspd3,aspd4,aspd5,apst1,apst2,apst3,apst4,apst5,cpst1,cpst2,cpst3,cpst4,cpst5,load1,load2,load3,load4,load5,pd,pn,ps,pl,pm,time  from tb_runstate limit 10 ";  //获取前2000条数据,其中数据数据项名称必须与要转换的LIst数据项名称一样，否则无法正确赋值
                                MySqlConnection Conn = new MySqlConnection(pdata.M_str_sqlcon);
                                Conn.Open();
                                MySqlCommand cmd = new MySqlCommand(comm3, Conn);
                                MySqlDataReader reader = cmd.ExecuteReader();
                                //MySqlDataReader reader = pdata.Select(comm3);
                                result = DataBase.Fabricate.FillList<Data_Run_info>(reader);
                                /*close the connection*/
                                reader.Close();
                                Conn.Close();
                                /*list to json string*/

                                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
                                timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                                String listStr = JsonConvert.SerializeObject(result, Formatting.None, timeConverter);

                                /*进一步转换数据*/

                                Data_json data_info = new Data_json();
                                data_info.dt = listStr;  //打包发送
                                data_info.did = Constants.RUNSTATE;

                                /*push data*/

                                listStr = WebSreverce_PostJson.ConvertToJson(data_info);
                                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, listStr);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
                               // Console.WriteLine(reponce);
                                if (reponce == "ac")
                                {
                                    //Console.WriteLine(rundata);
                                    //返回值正确，服务器收到了请求,执行删除数据库数据的操作
                                    MySqlConnection sqlcon = new MySqlConnection(pdata.M_str_sqlcon);
                                    sqlcon.Open();
                                    string comm4 = " delete from tb_runstate limit 10 ";  //获取前2000条数据
                                    pdata.Delete(sqlcon, comm4);
                                    sqlcon.Close();

                                    /*操作Processbar*/
                                    if (db_progressBar.Value + 3 <= db_progressBar.Maximum)  
                                {
                                    db_progressBar.Value+=3;
                                    this.pbar_state.Text = "进行中   [" + db_progressBar.Value.ToString() + "/" + db_progressBar.Maximum + "]....";  
                               }  
                               else  
                               {
                                   this.pbar_state.Text = "上传已完成！";
                                   if (MessageBox.Show("上传完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                   {
                                       return;
                                   }
 
                               }  

                                }
                                else
                                {
                                    if (MessageBox.Show("与Web服务器连接异常，请检查后重试！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                                    {
                                        break;
                                    }
                                }
                            }
                           
                        }

                    }
                    if (db_tblist.SelectedItems[0].Text == "登入登出表")
                    {
                        if (db_tblist.SelectedItems[0].SubItems[1].Text == "0")
                        {
                            MessageBox.Show("数据表为空，不需要上传");
                        }
                        else
                        {
                            db_progressBar.Value = 1;
                            db_progressBar.Minimum = 0;
                            db_progressBar.Maximum = tb_count[1];

                            for (int i = 0; tb_count[1] - i > 0; i++)
                            {
                                /*采用单条数据上传*/
                                Data_LogIn_info temp = new Data_LogIn_info();
                                DataBase pdata = new DataBase();
                                string comm3 = "select * from tb_login limit 1 ";  //获取1条数据
                                MySqlDataReader reader = pdata.Select(comm3);
                                temp.id = reader[1].ToString();
                                temp.ontime = (long)Int32.Parse(reader[2].ToString());
                                temp.runtime = (long)Int32.Parse(reader[3].ToString());
                                temp.time = reader[4].ToString();
                                /*close the connection*/
                                reader.Close();
                                pdata.con_close();
                                /*进一步打包数据，转json*/
                                string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                                Data_json data_info = new Data_json();
                                data_info.dt = rundata;
                                data_info.did = Constants.LOGIN;

                                rundata = WebSreverce_PostJson.ConvertToJson(data_info);
                                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, rundata);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
                                if (reponce == "ac")
                                {
                                    //Console.WriteLine(rundata);
                                    //返回值正确，服务器收到了请求
                                    MySqlConnection sqlcon = new MySqlConnection(pdata.M_str_sqlcon);
                                    sqlcon.Open();
                                    string comm4 = " delete from tb_login limit 1 ";  //获取前1条数据
                                    pdata.Delete(sqlcon, comm4);
                                    sqlcon.Close();

                                    /*操作Processbar*/
                                    if (db_progressBar.Value < db_progressBar.Maximum)
                                    {
                                        db_progressBar.Value += 1;
                                        this.pbar_state.Text = "上传进行中   [" + db_progressBar.Value.ToString() + "/" + db_progressBar.Maximum + "]....";
                                    }
                                    else
                                    {
                                        this.pbar_state.Text = "上传已完成！";
                                        if (MessageBox.Show("上传完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                        {
                                            return;
                                        }
                                    } 
                                }
                                else
                                {
                                    if (MessageBox.Show("与Web服务器连接异常，请检查后重试！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                    }
                    if (db_tblist.SelectedItems[0].Text == "报警表")
                    {
                        if (db_tblist.SelectedItems[0].SubItems[1].Text == "0")
                        {
                            MessageBox.Show("数据表为空，不需要上传");
                        }
                        else
                        {
                            db_progressBar.Value = 1;
                            db_progressBar.Minimum = 0;
                            db_progressBar.Maximum = tb_count[2];

                            for (int i = 0; tb_count[2] - i > 0; i++)
                            {
                                /*采用单条数据上传*/
                                Data_Err_info temp = new Data_Err_info();
                                DataBase pdata = new DataBase();
                                string comm3 = "select * from tb_error limit 1 ";  //获取1条数据
                                MySqlDataReader reader = pdata.Select(comm3);
                                temp.id = reader[1].ToString();
                                temp.f = (byte)Int16.Parse(reader[2].ToString());
                                temp.no = reader[3].ToString();
                                temp.time = reader[4].ToString();
                                temp.ctt = reader[5].ToString();
                                /*close the connection*/
                                reader.Close();
                                pdata.con_close();
                                /*进一步打包数据，转json*/
                                string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                                Data_json data_info = new Data_json();
                                data_info.dt = rundata;
                                data_info.did = Constants.ERRORINFO;

                                rundata = WebSreverce_PostJson.ConvertToJson(data_info);
                                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, rundata);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
                               // Console.WriteLine(reponce);
                                if (reponce == "ac")
                                {
                                    //Console.WriteLine(rundata);
                                    //返回值正确，服务器收到了请求
                                    MySqlConnection sqlcon = new MySqlConnection(pdata.M_str_sqlcon);
                                    sqlcon.Open();
                                    string comm4 = " delete from tb_error limit 1 ";  //获取前1条数据
                                    pdata.Delete(sqlcon, comm4);
                                    sqlcon.Close();

                                    /*操作Processbar*/
                                    if (db_progressBar.Value < db_progressBar.Maximum)
                                    {
                                        db_progressBar.Value += 1;
                                        this.pbar_state.Text = "上传进行中   [" + db_progressBar.Value.ToString() + "/" + db_progressBar.Maximum + "]....";
                                    }
                                    else
                                    {
                                        this.pbar_state.Text = "上传已完成！";
                                        if (MessageBox.Show("上传完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                        {
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (MessageBox.Show("与Web服务器连接异常，请检查后重试！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                                    {
                                        return;
                                    }
                                }
                            }
                        } 
                        

                    }
                    if (db_tblist.SelectedItems[0].Text == "环境数据表")
                    {
                        if (db_tblist.SelectedItems[0].SubItems[1].Text == "0")
                        {
                            MessageBox.Show("数据表为空，不需要上传");
                        }
                        else
                        {
                            db_progressBar.Value = 1;
                            db_progressBar.Minimum = 0;
                            db_progressBar.Maximum = tb_count[3];

                            for (int i = 0; tb_count[3] - i > 0; i++)
                            {
                                /*采用单条数据上传*/
                                Data_Env_info temp = new Data_Env_info();
                                DataBase pdata = new DataBase();
                                string comm3 = "select * from tb_env limit 1 ";  //获取1条数据
                                MySqlDataReader reader = pdata.Select(comm3);
                                temp.id = reader[1].ToString();
                                temp.u = Convert.ToSingle(reader[2].ToString());
                                temp.v = Convert.ToSingle(reader[3].ToString());
                                temp.w = Convert.ToSingle(reader[4].ToString());
                                temp.tep = Convert.ToSingle(reader[5].ToString());
                                temp.hmi = Convert.ToSingle(reader[6].ToString());
                                temp.time = reader[7].ToString();
                                /*close the connection*/
                                reader.Close();
                                pdata.con_close();
                                /*进一步打包数据，转json*/
                                string rundata = WebSreverce_PostJson.ConvertToJson(temp);
                                Data_json data_info = new Data_json();
                                data_info.dt = rundata;
                                data_info.did = Constants.ENVINFO;

                                rundata = WebSreverce_PostJson.ConvertToJson(data_info);
                                string reponce = WebSreverce_PostJson.Post_Jsonstr(WebSreverce_PostJson.urladd_inn, rundata);  //url表示数据服务器的地址及接口名称，可以定义成全局变量
                                // Console.WriteLine(reponce);
                                if (reponce == "ac")
                                {
                                    //Console.WriteLine(rundata);
                                    //返回值正确，服务器收到了请求
                                    MySqlConnection sqlcon = new MySqlConnection(pdata.M_str_sqlcon);
                                    sqlcon.Open();
                                    string comm4 = " delete from tb_env limit 1 ";  //获取前1条数据
                                    pdata.Delete(sqlcon, comm4);
                                    sqlcon.Close();

                                    /*操作Processbar*/
                                    if (db_progressBar.Value < db_progressBar.Maximum)
                                    {
                                        db_progressBar.Value += 1;
                                        this.pbar_state.Text = "上传进行中   [" + db_progressBar.Value.ToString() + "/" + db_progressBar.Maximum + "]....";
                                    }
                                    else
                                    {
                                        this.pbar_state.Text = "上传已完成！";
                                        if (MessageBox.Show("上传完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                        {
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (MessageBox.Show("与Web服务器连接异常，请检查后重试！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}
