using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace demo
{
    class HtApi
    {
        //连接服务器
        [DllImport("RunDll.dll", EntryPoint = "ClientConnectServer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ClientConnectServer(int robot_num, string ip_num, string port_num);
        //断开服务器连接
        [DllImport("RunDll.dll", EntryPoint = "DeleteServer", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteServer(int robot_num);
        //重新连接服务器
        [DllImport("RunDll.dll", EntryPoint = "ReConnectServer", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ReConnectServer(int robot_num);
        //获取当前轴的信息
        [DllImport("RunDll.dll", EntryPoint = "GetAxisinfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetAxisinfo(int tag);
        //获取当前系统信息
        [DllImport("RunDll.dll", EntryPoint = "GetSysteminfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetSysteminfo(int tag);
        //获取当前系统报警信息
        [DllImport("RunDll.dll", EntryPoint = "GetAlarminfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetAlarminfo(int tag);
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct SYSTEM_INFO
    {
        public SByte type;               //通讯时结构体编号，默认3
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string systemid;		  //数控系统ID，参数D222
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string systemtype;	  //数控系统型号，CASNUC 6000
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string systemver;		  //NC总版本号, 参数D223
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string ggroup;		  //G代码模态
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public string pn;			  //程序名
        public int ps;				  //代码运行状态，0：停止、非零：运行
        public int pl;				  //运行代码行号
        public Int32 s_loadcurrent;		  //主轴负载电流
        //  //进给轴负载电流
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public Int32[] axis_loadcurrent;			    //进给轴负载电流
        public Int32 ontime;			  //数控系统累计运行时间，单位：秒
        public Int32 runtime;			  //数控系统累计加工时间，单位：秒
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string timestamp;	  //时间戳yyyy-mm-dd hh:mm:ss.zzz
        //年-月-日 时:分:秒:毫秒
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct HT_AXIS_INFO
    {
        public SByte type;                    //通讯时结构体编号，默认1
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] c_axis;				    //进给轴指令位置，单位：0.0001mm
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] a_axis_work;			//进给轴实际位置（工件坐标），单位：0.0001mm
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] a_axis_relative;       	//进给轴实际位置（相对坐标），单位：0.0001mm
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] a_axis_machine; 		//进给轴实际位置（机床坐标），单位：0.0001mm
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] a_axis_remainder;	    //进给轴实际位置（剩余坐标），单位：0.0001mm
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public Int32[] a_f_value;			    //进给轴实际速度，单位：转/分
        public Int32 c_s_value;				//主轴指令速度，单位：转/分
        public Int32 a_s_value;			    //主轴实际速度，单位：转/分
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string timestamp;			//时间戳yyyy-mm-dd hh:mm:ss.zzz
        //年-月-日 时:分:秒:毫秒

    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct HT_ALARM_INFO
    {
        public SByte type;                         //通讯时结构体编号，默认2
        public int alarmcode;					//报警代码  ok  非零
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string alarmtime_occur;			//报警发生时间    ok
        //年-月-日 时:分:秒
        public Int32 alarmtime_remove;			    //报警消除时间
        public SByte alarmnum;					//报警个数
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string timestamp;				//时间戳yyyy-mm-dd hh:mm:ss.zzz  ok
        //年-月-日 时:分:秒:毫秒
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024 * 100)]
        public string warn;            //报警内容                                ok
    };
}
