using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo
{
    public class Data_Run_info
    {
        private string NC_ID;    //数控系统ID（标识唯一NC）
        public string id
        {
            get { return NC_ID; }
            set { NC_ID = value; }
        }
        private float Spindle_Act_speed;  //主轴实际转速
        public float cas
        {
            get { return Spindle_Act_speed; }
            set { Spindle_Act_speed = value; }
        }
        private float Spindle_Cmd_speed;  //主轴指令转速
        public float ccs
        {
            get { return Spindle_Cmd_speed; }
            set { Spindle_Cmd_speed = value; }
        }
        private float Spindle_Curr;   //主轴负载电流
        public float aload
        {
            get { return Spindle_Curr; }
            set { Spindle_Curr = value; }
        }
        private float Axis1_Act_Speed;   //轴1实际转速(一般为X轴)
        public float aspd1
        {
            get { return Axis1_Act_Speed; }
            set { Axis1_Act_Speed = value; }
        }
        private float Axis2_Act_Speed;   //轴2实际转速(一般为Y轴)
        public float aspd2
        {
            get { return Axis2_Act_Speed; }
            set { Axis2_Act_Speed = value; }
        }
        private float Axis3_Act_Speed;   //轴3实际转速(一般为Z轴)
        public float aspd3
        {
            get { return Axis3_Act_Speed; }
            set { Axis3_Act_Speed = value; }
        }
        private float Axis4_Act_Speed;   //轴4实际转速(一般为A轴)
        public float aspd4
        {
            get { return Axis4_Act_Speed; }
            set { Axis4_Act_Speed = value; }
        }
        private float Axis5_Act_Speed;   //轴5实际转速(一般为B轴)
        public float aspd5
        {
            get { return Axis5_Act_Speed; }
            set { Axis5_Act_Speed = value; }
        }
        private float Axis1_Act_Location;   //轴1实际位置(一般为X轴)
        public float apst1
        {
            get { return Axis1_Act_Location; }
            set { Axis1_Act_Location = value; }
        }
        private float Axis2_Act_Location;   //轴2实际位置(一般为Y轴)
        public float apst2
        {
            get { return Axis2_Act_Location; }
            set { Axis2_Act_Location = value; }
        }
        private float Axis3_Act_Location;   //轴3实际位置(一般为Z轴)
        public float apst3
        {
            get { return Axis3_Act_Location; }
            set { Axis3_Act_Location = value; }
        }
        private float Axis4_Act_Location;   //轴4实际位置(一般为A轴)
        public float apst4
        {
            get { return Axis4_Act_Location; }
            set { Axis4_Act_Location = value; }
        }
        private float Axis5_Act_Location;   //轴5实际位置(一般为B轴)
        public float apst5
        {
            get { return Axis5_Act_Location; }
            set { Axis5_Act_Location = value; }
        }
        private float Axis1_Cmd_Location;   //轴1指令位置(一般为X轴)
        public float cpst1
        {
            get { return Axis1_Cmd_Location; }
            set { Axis1_Cmd_Location = value; }
        }
        private float Axis2_Cmd_Location;   //轴2指令位置(一般为Y轴)
        public float cpst2
        {
            get { return Axis2_Cmd_Location; }
            set { Axis2_Cmd_Location = value; }
        }
        private float Axis3_Cmd_Location;   //轴3指令位置(一般为Z轴)
        public float cpst3
        {
            get { return Axis3_Cmd_Location; }
            set { Axis3_Cmd_Location = value; }
        }
        private float Axis4_Cmd_Location;   //轴4指令位置(一般为A轴)
        public float cpst4
        {
            get { return Axis4_Cmd_Location; }
            set { Axis4_Cmd_Location = value; }
        }
        private float Axis5_Cmd_Location;   //轴5指令位置(一般为B轴)
        public float cpst5
        {
            get { return Axis5_Cmd_Location; }
            set { Axis5_Cmd_Location = value; }
        }
        private float Axis_Curr_1;     //增加负载电流的项目，轴1
        public float load1
        {
            get { return Axis_Curr_1; }
            set { Axis_Curr_1 = value; }
        }
        private float Axis_Curr_2;   //轴2负载电流
        public float load2
        {
            get { return Axis_Curr_2; }
            set { Axis_Curr_2 = value; }
        }
        private float Axis_Curr_3;   //轴3负载电流
        public float load3
        {
            get { return Axis_Curr_3; }
            set { Axis_Curr_3 = value; }
        }
        private float Axis_Curr_4;   //轴4负载电流
        public float load4
        {
            get { return Axis_Curr_4; }
            set { Axis_Curr_4 = value; }
        }
        private float Axis_Curr_5;   //轴5负载电流
        public float load5
        {
            get { return Axis_Curr_5; }
            set { Axis_Curr_5 = value; }
        }
        private short Prog_num;       //运行程序编号
        public short pd
        {
            get { return Prog_num; }
            set { Prog_num = value; }
        }
        private string Prog_name;       //程序名
        public string pn
        {
            get { return Prog_name; }
            set { Prog_name = value; }
        }
        private string Run_state;       //代码运行状态
        public string ps
        {
            get { return Run_state; }
            set { Run_state = value; }
        }
        private int Prog_running;  //运行行
        public int pl
        {
            get { return Prog_running; }
            set { Prog_running = value; }
        }
        private short G_code_state;  //通道模态
        public short pm
        {
            get { return G_code_state; }
            set { G_code_state = value; }
        }
        private string Datatime;      //时间戳
        public string time
        {
            get { return Datatime; }
            set { Datatime = value; }
        }
       
    }


}
