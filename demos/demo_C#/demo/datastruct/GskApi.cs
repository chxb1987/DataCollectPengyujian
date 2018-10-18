
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace demo
{
   public class GskApi
    {
        public  const Int32  GSK25I_OK = 0x0000;
        public const Int32 GSK25I_ERR_HINST = 0x8000;
        public const Int32 GSK25I_ERR_REQ = 0x8001;
        public const Int32 GSK25I_ERR_AXIS = 0x0002;
        public const Int32 GSK25I_ERR_TYPE = 0x0003;
        public const Int32 NET_AXIS_NUM = 8;
        public const Int32 NET_GGROUP_NUM = 22;

        private IntPtr HINSGSKRM;


        [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_CreateInstance", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GSKRM_CreateInstance(ref byte ipadd, int type);
        [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_GetConnectState", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 GSKRM_GetConnectState(IntPtr hInst);
        [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_CloseInstance", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 GSKRM_CloseInstance(IntPtr hInst);

        [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_SetOvertime", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 GSKRM_SetOvertime(IntPtr hInst, UInt32 overtime);
        [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_GetCncTypeName", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 GSKRM_GetCncTypeName(IntPtr hInst, Byte[] typeName);
        


    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct VERS_INFO
        {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string sysVersion;//系统版本
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string armVersion;//应用版本
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dspVersion;//插补版本
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FPGAVersion;//位控版本
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string plcfileName;//梯图文件名
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string hardVersion;//硬件版本  保留
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string softWareNumber;//软件序号  保留
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string hardWareNumber;//硬件序号  保留
        };
      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct RUNSTAT_INFO
    {
	   public SByte OpMode;							//操作模式
	   public SByte RunMode;							//运行模式
	   public SByte EmergFlag;							//急停信号输入
	   public SByte currentalarmflag;					//当前是否处于报警状态，报警个数
	   public UInt16 currentAlarmNum;			//当前报警号
	   public UInt16 PartCount;				//加工零件数
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_GGROUP_NUM)]
	   public byte[] Gmode;	// 当前G代码模态
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_GGROUP_NUM)]
	  public  byte[] GmodeNext;//下段G代码模态
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_AXIS_NUM)]
	  public  byte[] SvCurrent;	//进给轴负载
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
      public  byte[] SpdCurrent;			//主轴负载
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	  public  string progfileName;					//当前文件
	  public  UInt32 run_time;					  // 运行时间
	  public  UInt32 process_time;				// 加工时间
	  public  UInt32 CNC_time;					//系统时间
	  public  UInt32 BlockNumber;				//行号
	  public  Int32 run_time_cnt;						// 累计运行时间
	  public  Int32 process_time_cnt;					// 累计加工时间
	  public  Int32 powerup_times_all;					// 累计总次数
	  public  Int32 powerup_times_month;				// 累计本月次数
	  public  Byte loginLevel;		//当前登陆等级
	  public  Byte opt_error;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
	  public  SByte[] reserver;		//保留
    };
     [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
     public struct AXIS_INFO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_AXIS_NUM)]
	   public Double[] Abs_Coord;//绝对坐标
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_AXIS_NUM)]
	   public Double[] Rel_Coord;//相对坐标
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_AXIS_NUM)]
	   public Double[] Mac_Coord;//机床坐标
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_AXIS_NUM)]
	   public Double[] Addi_Coord;//剩余距离
	   public Double Frd;
	   public Int32 Spd;
       public Int32 F;//进给
       public Int32 S;//转速!
       public Int32 J;//快速倍率
       [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_AXIS_NUM)]
       public Double[] ActiveAxisSpeed;//增加轴速度 
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct ALARM_INFO
    {
	   public Int32 index;//索引号
	   public Int32 axisNo;	//报警轴号或从站号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
	   public String ErrorNoStr;//报警号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public String ErrorTime;//报警时间
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public String ErrorMessage;//报警信息
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct ALARMLIST
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public ALARM_INFO[] alarm;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    struct PROGRAM_INFO
    {
	    public Int32 index;//索引号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	    public string programName;//加工程序名
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	    public string programTime;//加工时间(结束时间+加工耗时)
    };
    /*新结构体*/
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    struct BHSAMPLE_STATIC
    {
        public float cas;
        public float ccs;//主轴指令转速
        public float aload;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_AXIS_NUM)]
        public float[] aspd;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_AXIS_NUM)]
        public float[] apst;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_AXIS_NUM)]
        public float[] cpst;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GskApi.NET_AXIS_NUM)]
        public float[] load;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string  progname;  //程序名
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
        public string  runstatus;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 320)]//16*20
        public string almhead;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 608)]
        public string  almtime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1120)]
        public string  alminfor;
        public int runtime;
        public int ontime;
        public int gclinenum;  //
        public short prognum;  //程序编号
        public short gcmode;
        public short almflag;  
        public short reserved;
 
    };
    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    //struct OPERATE_INFO
    //{
    //    Int32	index;//索引号
    //    SByte[][] optstr[5][32];
    //    //0//操作类型
    //    //1//操作号或操作位
    //    //2//操作前数值
    //    //3//操作后数值
    //    //4//操作时间
    //};
   public class InfoApi
   {
       [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_GetVersionInfo", CallingConvention = CallingConvention.Cdecl)]
       public static extern IntPtr GSKRM_GetVersionInfo(IntPtr hInst);
       [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_GetRunInfo", CallingConvention = CallingConvention.Cdecl)]
       public static extern IntPtr GSKRM_GetRunInfo(IntPtr hInst);
       [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_GetAxisInfo", CallingConvention = CallingConvention.Cdecl)]
       public static extern IntPtr GSKRM_GetAxisInfo(IntPtr hInst);
       [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_GetAlarmInfo", CallingConvention = CallingConvention.Cdecl)]
       public static extern IntPtr GSKRM_GetAlarmInfo(IntPtr hInst,ref Int32 retcnt);
       [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_GetProgramInfo", CallingConvention = CallingConvention.Cdecl)]
       public static extern IntPtr GSKRM_GetProgramInfo(IntPtr hInst, ref Int32 retcnt);
       //[DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_GetOperateInfo", CallingConvention = CallingConvention.Cdecl)]
       //public static extern IntPtr GSKRM_GetOperateInfo(IntPtr hInst, ref Int32 retcnt);
       [DllImport("gsk25inetfun.dll", EntryPoint = "GSKRM_GetbhRunInfo", CallingConvention = CallingConvention.Cdecl)]
       public static extern IntPtr GSKRM_GetbhRunInfo(IntPtr hInst);
   }
    
}
