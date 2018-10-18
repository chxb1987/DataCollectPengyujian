using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo
{
    public class Data_regist_info
    {
        //private int DataType_flag;    //数据类型标志位
        //public int intDataType_flag
        //{
        //    get { return DataType_flag; }
        //    set { DataType_flag = value; }
        //}
        private string NC_ID;    //数控系统ID（标识唯一NC）
        public string strNC_ID
        {
            get { return NC_ID; }
            set { NC_ID = value; }
        }
        private string NC_state;    //系统登录标识
        public string strNC_state
        {
            get { return NC_state; }
            set { NC_state = value; }
        }
        private string IP;    //通信IP地址
        public string strIP
        {
            get { return IP; }
            set { IP = value; }
        }
        private  int Port;    //通信端口号 
        public int  strPort 
        {
            get { return Port; }
            set { Port = value; }
        }
        private string NC_factr;    //数控系统厂商
        public string strNC_factr
        {
            get { return NC_factr; }
            set { NC_factr = value; }
        }
        private short NCno;    //数控系统bianhao
        public short shrNCno
        {
            get { return NCno; }
            set { NCno = value; }
        }
        private string NC_Num;  //数控系统型号
        public string strNC_Num
        {
            get { return NC_Num; }
            set { NC_Num = value; }
        }
        private string NC_Version_Num_1;  //数控系统各部分版本号
        public string strNC_Version_Num_1
        {
            get { return NC_Version_Num_1; }
            set { NC_Version_Num_1 = value; }
        }
        private string NC_Version_Num_2;  //数控系统各部分版本号
        public string strNC_Version_Num_2
        {
            get { return NC_Version_Num_2; }
            set { NC_Version_Num_2 = value; }
        }
        private string NC_Version_Num_3;  //数控系统各部分版本号
        public string strNC_Version_Num_3
        {
            get { return NC_Version_Num_3; }
            set { NC_Version_Num_3 = value; }
        }
        private string NC_Version_Num_4;  //数控系统各部分版本号
        public string strNC_Version_Num_4
        {
            get { return NC_Version_Num_4; }
            set { NC_Version_Num_4 = value; }
        }
        private string NC_Version_Num_5;  //数控系统各部分版本号
        public string strNC_Version_Num_5
        {
            get { return NC_Version_Num_5; }
            set { NC_Version_Num_5 = value; }
        }
        private string Datatime;      //时间戳
        public string tDatatime
        {
            get { return Datatime; }
            set { Datatime = value; }
        }

    }
}
