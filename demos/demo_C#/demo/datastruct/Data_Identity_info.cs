using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo
{
    public class NC_Version_Num
    {
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
    }
    public class Data_Identity_info
    {
        //private int DataType_flag;    //数据类型标志位
        //public int intDataType_flag
        //{
        //    get { return DataType_flag; }
        //    set { DataType_flag = value; }
        //}
        private string NC_ID;    //数控系统ID（标识唯一NC）
        public string id
        {
            get { return NC_ID; }
            set { NC_ID = value; }
        }
        private string NC_Num;  //数控系统型号
        public string tp
        {
            get { return NC_Num; }
            set { NC_Num = value; }
        }
        private string NC_Version_Num;  //数控系统各部分版本号
        public string ver
        {
            get { return NC_Version_Num; }
            set { NC_Version_Num = value; }
        }
        private string Datatime;      //时间戳
        public string time
        {
            get { return Datatime; }
            set { Datatime = value; }
        }

    }
}
