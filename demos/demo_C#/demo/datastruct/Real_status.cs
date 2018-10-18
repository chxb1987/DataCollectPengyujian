using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo
{
    public class Real_status
    {
        private int Comm_flag;    //通信标志位，控制灯的颜色
        public int intComm_flag
        {
            get { return Comm_flag; }
            set { Comm_flag = value; }
        }
        private int Err_flag;    //报警标志位
        public int intErr_flag
        {
            get { return Err_flag; }
            set { Err_flag = value; }
        }
        private int Power_flag;  //上电标志位
        public int intPower_flag
        {
            get { return Power_flag; }
            set { Power_flag = value; }
        }

        private string Err_num;   //报警号
        public string intErr_num
        {
            get { return Err_num; }
            set { Err_num = value; }
        }
        private string Err_txt;  //报警文本
        public string strErr_txt
        {
            get { return Err_txt; }
            set { Err_txt = value; }
        }
        private string IPv4_txt; //机床IP地址
        public string strIPv4_txt
        {
            get { return IPv4_txt; }
            set { IPv4_txt = value; }
        }
        private int Sys_num;   //系统编号1-20,可以考虑添加数控系统ID，即唯一识别码20161220
        public int intSys_num
        {
            get { return Sys_num; }
            set { Sys_num = value; }
        }
        private int State_nc; //系统状态量
        public int strState_nc
        {
            get { return State_nc; }
            set { State_nc = value; }
        }
        private string Err_time;//报警时间，在报警发生以后取的
        public string strErr_time
        {
            get { return Err_time; }
            set { Err_time = value; }
        }
        private string Power_time;//本次开机时间
        public string strPower_time
        {
            get { return Power_time; }
            set { Power_time = value; }
        }
        private Double Feed_v;  //进给速度
        public Double intFeed_v
        {
            get { return Feed_v; }
            set { Feed_v = value; }
        }
        private Double Spindle_v;//主轴转速
        public Double intSpindle_v
        {
            get { return Spindle_v; }
            set { Spindle_v = value; }
        }
        private Double Axis_Curr_1;     //增加负载电流的项目，X轴
        public Double intAxis_Curr_1
        {
            get { return Axis_Curr_1; }
            set { Axis_Curr_1 = value; }
        }
        private Double Axis_Curr_2;   //Y轴负载电流
        public Double intAxis_Curr_2
        {
            get { return Axis_Curr_2; }
            set { Axis_Curr_2 = value; }
        }
        private Double Axis_Curr_3;   //Z轴负载电流
        public Double intAxis_Curr_3
        {
            get { return Axis_Curr_3; }
            set { Axis_Curr_3 = value; }
        }
        private Double Axis_Curr_4;   //主轴负载电流
        public Double intAxis_Curr_4
        {
            get { return Axis_Curr_4; }
            set { Axis_Curr_4 = value; }
        }
        private Double Axis_1;   //X轴实际转速
        public Double intAxis_1
        {
            get { return Axis_1; }
            set { Axis_1 = value; }
        }
        private Double Axis_2;   //Y轴实际转速
        public Double intAxis_2
        {
            get { return Axis_2; }
            set { Axis_2 = value; }
        }
        private Double Axis_3;  //Z轴实际转速
        public Double intAxis_3
        {
            get { return Axis_3; }
            set { Axis_3 = value; }
        }
        private int Axis_1_err;   //X轴跟踪误差
        public int intAxis_1_err
        {
            get { return Axis_1_err; }
            set { Axis_1_err = value; }
        }
        private int Axis_2_err;   //Y轴跟踪误差
        public int intAxis_2_err
        {
            get { return Axis_2_err; }
            set { Axis_2_err = value; }
        }
        private int Axis_3_err;   //Z轴跟踪误差
        public int intAxis_3_err
        {
            get { return Axis_3_err; }
            set { Axis_3_err = value; }
        }
        private string Prog_Gcode;   //当前G代码标志,在v2.4.9.2中，因为只有华中提供G代码标志，仅仅是一个中间转换量，所以改为具体G代码内容，由代码行号检索Gcode字符串数组。
        public string strProg_Gcode
        {
            get { return Prog_Gcode; }
            set { Prog_Gcode = value; }
        }
        private int Prog_num;       //运行程序编号
        public int intProg_num
        {
            get { return Prog_num; }
            set { Prog_num = value; }
        }
        private int Prog_running;  //运行行
        public int intProg_running
        {
            get { return Prog_running; }
            set { Prog_running = value; }
        }
        private int Prog_decode;   //译码行
        public int intProg_decode
        {
            get { return Prog_decode; }
            set { Prog_decode = value; }
        }
        
        private string PC_time;      //用作时间戳，不显示在界面
        public string dataPC_time
        {
            get { return PC_time; }
            set { PC_time = value; }
        }
        private uint PC_time_Mill;   //可用于检测是否已经断连
        public uint dataPC_time_Mill
        {
            get { return PC_time_Mill; }
            set { PC_time_Mill = value; }
        }
        /*增加环境变量*/
        private Double Temperature;  //温度
        public Double douTemperature
        {
            get { return Temperature; }
            set { Temperature = value; }
        }
        private Double Humidity;//湿度
        public Double douHumidity
        {
            get { return Humidity; }
            set { Humidity = value; }
        }
        private Double Voltage_1ch;//1#电压
        public Double douVoltage_1ch
        {
            get { return Voltage_1ch; }
            set { Voltage_1ch = value; }
        }
        private Double Voltage_2ch;//2#电压
        public Double douVoltage_2ch
        {
            get { return Voltage_2ch; }
            set { Voltage_2ch = value; }
        }
        private Double Voltage_3ch;//3#电压
        public Double douVoltage_3ch
        {
            get { return Voltage_3ch; }
            set { Voltage_3ch = value; }
        }

        private string Err_log;  //报警记录日志，包含时间和发生消除时间
        public string strErr_log
        {
            get { return Err_log; }
            set { Err_log = value; }
        }

        /*需要添加每秒采集的数据项，但不作为界面显示参数，仅仅是用于Post给WebServer*/

         





    }


}
