using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo
{
    public class Data_LogIn_info
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
        private long Power_time;     //累计运行时间
        public long ontime
        {
            get { return Power_time; }
            set { Power_time = value; }
        }
        private long Work_time;     //累计加工时间
        public long runtime
        {
            get { return Work_time; }
            set { Work_time = value; }
        }
        private string Datatime;      //报警发生时间或者消除时间
        public string  time
        {
            get { return Datatime; }
            set { Datatime = value; }
        } 

    }
}
