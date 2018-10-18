using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo
{
    public class Data_Env_info
    {
        private string NC_ID;    //数控系统ID（标识唯一NC）
        public string id
        {
            get { return NC_ID; }
            set { NC_ID = value; }
        }

        private float Voltage_1ch;//1#电压
        public float u
        {
            get { return Voltage_1ch; }
            set { Voltage_1ch = value; }
        }
        private float Voltage_2ch;//2#电压
        public float v
        {
            get { return Voltage_2ch; }
            set { Voltage_2ch = value; }
        }
        private float Voltage_3ch;//3#电压
        public float w
        {
            get { return Voltage_3ch; }
            set { Voltage_3ch = value; }
        }
        /*增加环境变量*/
        private float Temperature;  //温度
        public float tep
        {
            get { return Temperature; }
            set { Temperature = value; }
        }
        private float Humidity;//湿度
        public float hmi
        {
            get { return Humidity; }
            set { Humidity = value; }
        }
        private string Datatime;      //时间戳
        public string time
        {
            get { return Datatime; }
            set { Datatime = value; }
        }

   }


}
