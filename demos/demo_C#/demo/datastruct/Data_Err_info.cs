using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo
{
    public class Data_Err_info
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
        private Byte Err_time_flag;  //报警时间标志位 0：表示报警发生时间 1：表示报警消除时间
        public Byte f
        {
            get { return Err_time_flag; }
            set { Err_time_flag = value; }
        }
        private string Err_num;     //报警代码
        public string no
        {
            get { return Err_num; }
            set { Err_num = value; }
        }
        private string Datatime;      //报警发生时间或者消除时间
        public string  time
        {
            get { return Datatime; }
            set { Datatime = value; }
        }
        private string Err_txt;  //报警文本
        public string ctt
        {
            get { return Err_txt; }
            set { Err_txt = value; }
        }

    }
}
