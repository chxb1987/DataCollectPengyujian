using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace HNCAPI
{
    public class HncDataDef
    {
        public const Int32 PAR_PROP_DATA_LEN = 68;
        public const Int32 MAX_FILE_NUM_PER_DIR = 128;
        public const Int32 DATA_STR_LEN = 8;
        public const Int32 AXIS_DATA_LEN = 32;
        public const Int32 SYS_DATA_LEN = 64;
        public const Int32 SAMPLEDATA_MAX_NUM = 10000;
        public const Int32 PROG_NAME_LEN = 60;
        public const Int32 PAR_FILE_NAME_LEN = 16;
        public const Int32 ALARM_DATA_LEN = 64;
        public const Int32 SDATAPROPOTY_LEN = 64;
        public const Int32 SPARAMVALUE_LEN = 8;
        public const Int32 MAX_RESERVE_DATA_LEN = 128;

        public const Int32 PARAMAN_FILE_NCU = 0;				// NC参数
        public const Int32 PARAMAN_FILE_MAC = 1;				// 机床用户参数
        public const Int32 PARAMAN_FILE_CHAN = 2;				// 通道参数
        public const Int32 PARAMAN_FILE_AXIS = 3;				// 坐标轴参数
        public const Int32 PARAMAN_FILE_ACMP = 4;				// 误差补偿参数
        public const Int32 PARAMAN_FILE_CFG = 5;				// 设备接口参数
        public const Int32 PARAMAN_FILE_TABLE = 6;				// 数据表参数
        public const Int32 PARAMAN_FILE_BOARD = 7;				// 主站参数
        public const Int32 PARAMAN_MAX_FILE_LIB = 7;			// 参数结构文件最大分类数
        public const Int32 PARAMAN_MAX_PARM_PER_LIB = 1000;	// 各类参数最大条目数 
        public const Int32 PARAMAN_MAX_PARM_EXTEND = 1000;		// 分支扩展参数最大条目数
        public const Int32 PARAMAN_LIB_TITLE_SIZE = 16;		// 分类名字符串最大长度
        public const Int32 PARAMAN_REC_TITLE_SIZE = 16;		// 子类名字符串最大长度
        public const Int32 PARAMAN_ITEM_NAME_SIZE = 64;		// 参数条目字符串最大长度
        public const Int32 SERVO_PARM_START_IDX = 200;			// 伺服参数起始参数号
        // 0x000F：0,用第1编码器反馈 1,用第2编码器反馈  2,无反馈
        // 0x00F0：0,用第1编码器指令 1,用第2编码器指令
        // 0x0100：0,跟随误差由伺服驱动器反馈 1,跟踪误差由系统计算
        // 0x1000：0,默认采用32位脉冲计数 1,采用64位脉冲计数
        public const Int32 AX_ENCODER_MASK = 0x00FF;
        public const Int32 AX_NC_CMD_MASK = 0x00F0;
        public const Int32 AX_NC_TRACK_ERR = 0x0100;
        public const Int32 AX_NC_CYC64_MODE = 0x1000;
        //
        public const Int32 VAR_SYS_VNUM = 10000;
        public const Int32 VAR_CHAN_VNUM = 2000;
        public const Int32 VAR_AXIS_VNUM	= 100;
    };

    public class EventDef
    {
        //事件来源
        public const Int16 EV_SRC_SYS = 0x010;		// 系统事件
        public const Int16 EV_SRC_CH0 = 0x100;		// 通道0事件 0x100~0x10f
        public const Int16 EV_SRC_MDI = 0x110;		// MDI的事件 
        public const Int16 EV_SRC_KBD = 0x200;		// 键盘事件
        public const Int16 EV_SRC_AX0 = 0x300;		// 轴事件
        public const Int16 EV_SRC_NET = 0x400;		//网络事件
        //事件代码
        // 2. 定义通道事件
        public const UInt16 ncEvtPrgStart = 0xa001;	// 程序启动
        public const UInt16 ncEvtPrgEnd = 0xa002;	// 程序结束
        public const UInt16 ncEvtPrgHold = 0xa003;	// Hold完成	
        public const UInt16 ncEvtPrgBreak = 0xa004;	// break完成	
        public const UInt16 ncEvtG92Fin = 0xa005;	// G92完成
        public const UInt16 ncEvtRstFin = 0xa006;	// 上电复位完成
        public const UInt16 ncEvtRwndFin = 0xa007;	// 重运行完成
        public const UInt16 ncEvtMdiRdy = 0xa008;	// MDI准备好
        public const UInt16 ncEvtMdiExit = 0xa009;	// MDI退出
        public const UInt16 ncEvtMdiAck = 0xa00a;	// MDI行解释完成
        public const UInt16 ncEvtRunStart = 0xa00b;// 程序运行

        public const UInt16 ncEvtRunRowAck = 0xa00d;	// 任意行请求应答
        public const UInt16 ncEvtRunRowRdy = 0xa00e;	// 任意行准备好

        public const UInt16 ncEvtBpSaved = 0xa011;	// 断点保存完成
        public const UInt16 ncEvtBpResumed = 0xa012;	// 断点恢复完成
        public const UInt16 ncEvtIntvHold = 0xa013;	// 执行到M92等待用户干预
        public const UInt16 ncEvtEstop = 0xa014;	// 外部急停
        public const UInt16 ncEvtLoadOK = 0xa015;	// 程序加载完成

        public const UInt16 ncEvtSyntax1 = 0xa016;	// 第一类语法错【修改后可接着运行】
        public const UInt16 ncEvtSyntax2 = 0xa017;	// 第二类语法错【修改后从头运行】

        public const UInt16 ncEvtGcodeSave = 0xa018;	// 程序中的数据保存指令
        public const UInt16 ncEvtLoadData = 0xa019;	// 程序中的数据加载指令
        public const UInt16 ncEvtChgTool = 0xa01a;	// G代码修改了刀具数据
        public const UInt16 ncEvtChgCrds = 0xa01b;	// G代码修改了坐标系数据
        public const UInt16 ncEvtChgAxes = 0xa01c;	// 通道轴组发生了改变

        public const UInt16 ncEvtNckNote = 0xa01e;	// 通道提示
        public const UInt16 ncEvtNckAlarm = 0xa01f;	// 通道报警
        public const UInt16 ncEvtStopAck = 0xa020;	// sys_stop_prog完成
        public const UInt16 ncEvtFaultIrq = 0xa030;	// 故障中断
        public const UInt16 ncEvtPackFin = 0xa040;	// 数据打包完成

        public const UInt16 ncEvtChFin0 = 0xa050;	// 通道0加工完成
        public const UInt16 ncEvtChFin1 = 0xa051;	// 通道1加工完成
        public const UInt16 ncEvtChFin2 = 0xa052;	// 通道2加工完成
        public const UInt16 ncEvtChFin3 = 0xa053;	// 通道3加工完成
        public const UInt16 ncEvtChFin = 0xa054;
        public const UInt16 ncEvtAlarmChg = 0xa055;	// 报警产生或消除
        public const UInt16 ncEvtConnect = 0xa060;	// nc连接
        public const UInt16 ncEvtDisConnect = 0xa061;	// nc断开连接

        // 3. 定义轴事件
        public const UInt16 ncEvtMaxEncPos = 0xa201;	// 轴编码器初始位置过大

        // 4. 定义系统事件
        public const UInt16 ncEvtPoweroff = 0xa800;	// 系统断电
        public const UInt16 ncEvtSaveData = 0xa801;	// 保存系统数据
        public const UInt16 ncEvtSysExit = 0xa802;	// 系统退出

        public const UInt16 ncEvtUserStart = 0xb000;	// 用户自定义事件
        public const UInt16 ncEvtUserReqChn = (ncEvtUserStart + 1);	// 请求切换通道
        public const UInt16 ncEvtUserReqMsk = (ncEvtUserStart + 2);	// 请求屏蔽通道
        public const UInt16 ncEvtUserFunc1 = (ncEvtUserStart + 100);	// event 100 对应用户按键调用指定程序
        public const UInt16 ncEvtUserFunc2 = (ncEvtUserStart + 101);	// event 100 对应用户按键调用指定程序
        public const UInt16 ncEvtHardRstFin = (ncEvtUserStart + 102);	// 硬复位完成
    };

    // 2.1 基本数据范围
    public class ToolNumDef
    {
        public const Int32 MAX_GEO_PARA = 24;		// 刀具几何参数个数
        public const Int32 MAX_WEAR_PARA = 24;		// 刀具磨损参数个数
        public const Int32 MAX_TECH_PARA = 24;		// 刀具工艺相关参数个数
        public const Int32 MAX_TOOL_EXPARA = 24;	// 刀具扩展参数个数
        public const Int32 MAX_TOOL_MONITOR = 24;	// 刀具监控参数个数
        public const Int32 MAX_TOOL_MEASURE = 24;	// 刀具测量参数个数
        public const Int32 MAX_TOOL_BASE = 24;		// 刀具一般信息参数个数
        public const Int32 MAX_TOOL_PARA = 200;	// 刀具基本参数个数 (24+24+24+24+24+24+24 = 168)
        public const Int32 MAGZ_HEAD_SIZE = 16;	// 刀库数据表头大小
    };

    // 刀具参数具体化（兼容以前的刀具宏）
    public class ToolDetail
    {
        // 铣刀参数索引
        public const Int32 MTOOL_RAD = (Int32)ToolParaIndex.GTOOL_RAD1;	// 刀具半径
        public const Int32 MTOOL_LEN = (Int32)ToolParaIndex.GTOOL_LEN1;	// 刀具长度
        public const Int32 MTOOL_RAD_WEAR = (Int32)ToolParaIndex.WTOOL_RAD1;	// 铣刀:半径磨损补偿（径向）
        public const Int32 MTOOL_LEN_WEAR = (Int32)ToolParaIndex.WTOOL_LEN1;	// 铣刀:长度磨损补偿（轴向）

        // 车刀参数索引
        public const Int32 LTOOL_RAD = (Int32)ToolParaIndex.GTOOL_RAD1;		// 刀尖半径
        public const Int32 LTOOL_DIR = (Int32)ToolParaIndex.GTOOL_DIR;		// 刀尖方向
        public const Int32 LTOOL_RAD_WEAR = (Int32)ToolParaIndex.WTOOL_RAD1;// 车刀:刀具磨损值（径向）（相对值）
        public const Int32 LTOOL_LEN_WEAR = (Int32)ToolParaIndex.WTOOL_LEN1;// 车刀:刀具磨损值（轴向）（相对值）
        public const Int32 LTOOL_XOFF = (Int32)ToolParaIndex.GTOOL_LEN1;	// 车刀：刀具偏置值（径向）（绝对值） =  试切时X值 - 试切直径/2
        public const Int32 LTOOL_YOFF = (Int32)ToolParaIndex.GTOOL_LEN2;
        public const Int32 LTOOL_ZOFF = (Int32)ToolParaIndex.GTOOL_LEN3;	// 车刀：刀具偏置值（轴向）（绝对值） =  试切时Z值 - 试切长度
        public const Int32 LTOOL_XDONE = (Int32)ToolParaIndex.TETOOL_PARA0;	// X试切标志
        public const Int32 LTOOL_YDONE = (Int32)ToolParaIndex.TETOOL_PARA0;
        public const Int32 LTOOL_ZDONE = (Int32)ToolParaIndex.TETOOL_PARA1;	// Z试切标志

        public const Int32 SPDL_RESOLUTION = 1000;			// 主轴转速分辨率
        public const Int32 SMPL_CHAN_NUM = 16;				// 采样通道数
        public const Int32 SMPL_DATA_NUM = 10000;			// 每采样通道的采样点数
    };

    public class AlarmDef
    {
        public const Int32 EHNC_INVAL = -101;			// 无效的参数
        public const Int32 EHNC_FUNC = -102;			// 功能无法执行
        public const Int32 ALARM_TXT_LEN = 64;			// 报警内容文本长度
        public const Int32 ALARM_HISTORY_MAX_NUM = 512; // 最大报警历史数
    };

    //系统定义的各类坐标系数目
    public enum HncCrdsMaxNum
    {
        G5EXT_MAX_NUM,		//	G54.X扩展坐标系的数目
        CHG_WCS_MAX_NUM,	//	进给保持时的临时工件坐标系数目
        TCS_MAX_NUM,			//	TCS坐标系数目
    }
    //寄存器类型
    public enum HncRegType
    {
        REG_TYPE_X = 0,	// X寄存器 Bit8
        REG_TYPE_Y,		// Y寄存器 Bit8
        REG_TYPE_F,		// F寄存器 Bit16
        REG_TYPE_G,		// G寄存器 Bit16
        REG_TYPE_R,		// R寄存器 Bit8
        REG_TYPE_W,		// W寄存器 Bit16
        REG_TYPE_D,		// D寄存器 Bit32
        REG_TYPE_B,		// B寄存器 Bit32
        REG_TYPE_P,		// P寄存器 Bit32
        REG_TYPE_TOTAL
    }

    // 坐标系数据类型
    // 未标注类型的是Bit32
    public enum HncCRDS
    {
        HNC_CRDS_CH_G5X_ZERO = 0, // 指定通道轴坐标系零点
        HNC_CRDS_CH_G5X_ID,       // 指定通道轴坐标系ID
        HNC_CRDS_CH_WCS_ZERO,     // 指定通道轴工件坐标系零点
        HNC_CRDS_CH_REL_ZERO,     // 指定通道轴相对坐标系零点
        HNC_CRDS_CH_FRAME_ZERO,   // 指定通道轴基架坐标系零点
        HNC_CRDS_G68_PNT1,        // 特性坐标系辅助点1坐标
        HNC_CRDS_G68_PNT2,        // 特性坐标系辅助点2坐标
        HNC_CRDS_G68_ZERO,        // 特性坐标系零点
        HNC_CRDS_G68_VCT,         // 特性坐标系向量 fBit64
	HNC_CRDS_CH_G5X_OFFSET_ZERO,  //指定通道轴坐标系偏置零点
        HNC_CRDS_TOTAL
    }
    //参数类型
    public enum PAR_STORE_TYPE
    {
        TYPE_NULL = 0,	//空类型
        TYPE_INT,		//整型
        TYPE_FLOAT,		//实型
        TYPE_EXPR,		//表达式
        TYPE_VAR,		//简单变量
        TYPE_STRING,	//字符串
        TYPE_UINT,		//无符号整型
        TYPE_BOOL,		//布尔型
        TYPE_FUNC,		//函数表达式
        TYPE_ARR,		//数组表达式
        TYPE_HEX4,		//十六进制格式
        TYPE_BYTE		//字节类型
    }

    // 子类属性定义
    public enum ParaSubClassProp
    {
        SUBCLASS_NAME,		// 子类名
        SUBCLASS_ROWNUM,	// 子类行数
        SUBCLASS_NUM		// 子类数
    }

    public enum PAR_ACT_TYPE
    {
        PARA_ACT_SAVE, //保存生效
        PARA_ACT_NOW,  //立即生效
        PARA_ACT_RST,  //复位生效
        PARA_ACT_PWR,  //重启生效
        PARA_ACT_HIDE  //隐藏未启用
    }

    public enum ParNcu
    {
        PAR_NCU_TYPE,			// NCU控制器类型
        PAR_NCU_CYCLE,			// 插补周期
        PAR_NCU_PLC2_CMDN,		// PLC2周期执行语句数

        PAR_NCU_ANG_RESOL = 5,	// 角度计算分辨率
        PAR_NCU_LEN_RESOL,		// 长度计算分辨率
        PAR_NCU_TIME_RESOL,		// 时间编程分辨率
        PAR_NCU_VEL_RESOL,		// 线速度编程分辨率
        PAR_NCU_SPDL_RESOL,		// 角速度编程分辨率
        PAR_NCU_ARC_PROFILE,	// 圆弧插补轮廓允许误差
        PAR_NCU_MAX_RAD_ERR,	// 圆弧编程端点半径允许偏差
        PAR_NCU_G43_SW_MODE,	// 刀具轴选择方式[0,固定z向;1,G17/18/19切换;2,G43指令轴切换]
        PAR_NCU_G41_G00_G01,	// G00插补使能
        PAR_NCU_G53_LEN_BACK,	// G53之后自动恢复刀具长度补偿[0,不恢复 1 恢复]
        PAR_NCU_CRDS_NUM,	    // 允许联动轴数
        PAR_NCU_LAN_EN,			// 局域网使能
        PAR_NCU_POWER_SAFE,		// 断电保护使能
        PAR_NCU_TIME_EN,		// 系统时间显示使能
        PAR_NCU_PSW_CHECK,		// 权限检查使能
        PAR_NCU_ALARM_POP,		// 报警窗口自动显示使能
        PAR_NCU_KBPLC_EN,       // 键盘PLC使能
        PAR_NCU_GRAPH_ERAS_EN,  // 图形自动擦除使能	
        PAR_NCU_FSPD_DISP,		// F进给速度显示方式	
        PAR_NCU_GLNO_DISP,		// G代码行号显示方式
        PAR_NCU_INCH_DISP,	    // 公制/英制选择
        PAR_NCU_DISP_DIGITS,	// 位置小数点后显示位数
        PAR_NCU_FEED_DIGITS,	// 速度小数点后显示位数
        PAR_NCU_SPINDLE_DIGITS,	// 转速小数点后显示位数
        PAR_NCU_LANGUAGE,		// 语言选择
        PAR_NCU_LCD_TIME,		// 进入屏保等待时间
        PAR_NCU_DISK_TYPE,		// 外置程序存储类型
        PAR_NCU_REFRE_INTERV,   // 界面刷新间隔时间
        PAR_NCU_SAVE_TYPE,      // 是否外接UPS
        PAR_NCU_PROG_RST,       // 重运行是否提示
        PAR_NCU_SERVER_NAME,
        PAR_NCU_SERVER_IP1,
        PAR_NCU_SERVER_IP2,
        PAR_NCU_SERVER_IP3,
        PAR_NCU_SERVER_IP4,
        PAR_NCU_SERVER_PORT,    // 服务器端口号
        PAR_NCU_SERVER_LOGIN,   // 服务器访问用户名
        PAR_NCU_SERVER_PASSWD,  // 服务器访问密码
        PAR_NCU_FTP_ADMIT,      // FTP是否验证权限
        PAR_NCU_NET_TYPE,       // 网盘映射类型
        PAR_NCU_IP1,            // IP地址段1
        PAR_NCU_IP2,            // IP地址段2
        PAR_NCU_IP3,            // IP地址段3
        PAR_NCU_IP4,            // IP地址段4
        PAR_NCU_PORT,           // 本地端口号
        PAR_NCU_NET_START,		// 是否开启网络
        PAR_NCU_SERIAL_TYPE,	// 串口类型
        PAR_NCU_SERIAL_NO = 52,	// 串口号
        PAR_NCU_DATA_LEN,       // 收发数据长度
        PAR_NCU_STOP_BIT,       // 停止位
        PAR_NCU_VERIFY_BIT,     // 校验位
        PAR_NCU_BAUD_RATE,      // 波特率
        PAR_NCU_IP_TYPE,        // 静态IP/动态IP
        
        PAR_NCU_TOOL_NUM = 60,	// 最大刀具数
        PAR_NCU_TOFF_DIGIT,     // 刀补有效位数
        PAR_NCU_MAGZ_NUM,		// 最大刀库数
        PAR_NCU_TOOL_LOCATION,	// 最大刀座数
        PAR_NCU_TABRA_ADD_EN,   // 刀具磨损累加使能
        PAR_NCU_TDIA_SHOW_EN,   // 车刀直径显示使能
        PAR_NCU_SUB_PROG_EN,    // 全局子程序调用使能
        PAR_NCU_TRANS_ORDER,	// 镜像缩放旋转嵌套次序
        // 【0 旋转->缩放->镜像 1 镜像->缩放->旋转 2 自由编程，自动整理成按镜像->缩放->旋转的次序实施变换 
        // 3 按照实际的编程次序实施变换； 0/1/2三种选择时，都会按照镜像->缩放->旋转的次序实施变换】
        PAR_NCU_CYCLE_OPT,		// 复合循环路径选项【0x00FF: 0 常规  1 退刀段效率优先 2 FANUC兼容  &0xFF00 = =  0x0000 : 45度退刀 0x0100: 径向退刀 
        // &0x0200 = =  0x0200时最后一刀直接退到循环起点，凹槽中有台阶时不要用此选项】
        PAR_NCU_HOLD_DECODE_EN,	// 进给保持后重新解释使能
        PAR_NCU_G28_LEN_BACK,	// G28后是否自动恢复刀长补
        PAR_NCU_SPEEDUP_EN,    // 内部调试用，加速使能

        PAR_NCU_LOG_SAVE_TYPE = 80, // 日志文件保存类型

        PAR_NCU_INTERNET_IP1,	// 网络平台服务器IP
        PAR_NCU_INTERNET_IP2,
        PAR_NCU_INTERNET_IP3,
        PAR_NCU_INTERNET_IP4,
        PAR_NCU_INTERNET_PORT,	//网络平台服务器端口

        PAR_NCU_HMI = 100,					// 界面设置参数基地址
        PAR_NCU_ISSU_EDITION = PAR_NCU_HMI,	// 发布版本号
        PAR_NCU_TEST_EDITION,				// 测试版本号
        PAR_NCU_SHOW_LIST,					// 示值列，40
        PAR_NCU_GRAPH = PAR_NCU_SHOW_LIST + 40,	// 图形参数，90

        PAR_NCU_ALARM_LOG_NUM_LIMIT = 280,	//	日志条目限制
        PAR_NCU_WORKINFO_LOG_NUM_LIMIT,
        PAR_NCU_FILECHANGE_LOG_NUM_LIMIT,
        PAR_NCU_PANEL_LOG_NUM_LIMIT,
        PAR_NCU_DEFINE_LOG_NUM_LIMIT,
        PAR_NCU_EVENT_LOG_NUM_LIMIT,

        PAR_NCU_ALARM_LOG_TIME_LIMIT = 290,	//	日志时间限制
        PAR_NCU_WORKINFO_LOG_TIME_LIMIT,
        PAR_NCU_FILECHANGE_LOG_TIME_LIMIT,
        PAR_NCU_PANEL_LOG_TIME_LIMIT,
        PAR_NCU_DEFINE_LOG_TIME_LIMIT,
        PAR_NCU_EVENT_LOG_TIME_LIMIT,

        PAR_NCU_PROG_PATH = 300,	// 加工代码程序路径
        PAR_NCU_PLC_PATH,		// PLC程序路径
        PAR_NCU_PLC_NAME,		// PLC程序名
        PAR_NCU_DRV_PATH,		// 驱动程序路径
        PAR_NCU_DRV_NAME,		// 驱动程序名
        PAR_NCU_PARA_PATH,		// 参数文件路径
        PAR_NCU_PARA_NAME,		// 参数文件名
        PAR_NCU_SIMU_PATH,		// 仿真配置文件路径
        PAR_NCU_SIMU_NAME,		// 仿真配置文件名
        PAR_NCU_DLGP_PATH,		// 对话编程配置文件路径
        PAR_NCU_DLGP_NAME,		// 对话编程配置文件名
        PAR_NCU_VIDEO_DEV,      // 视频外设驱动

        PAR_NCU_SUB_DBG = 320,	// 宏程序单段使能【WIN】
        PAR_NCU_USER_LOGIN = 321,	// 	是否开启用户登录?
        PAR_NCU_G16_OPT = 350,	// G16的极点定义模式选择 0：FANUC模式 1：HNC-8模式
        PAR_NCU_GEDIT_FRAME = 351,	// 编辑界面框架选择 0：HNC-8模式 1：宁江专机模式
        PAR_NCU_TOTAL = 500		// NC参数总数
    }

    // 机床用户参数
    public enum ParMac
    {
        PAR_MAC_CHAN_NUM,	// 通道数：1~SYS_CHAN_NUM
        PAR_MAC_CHAN_TYPE,	// 机床通道加工类型【车、铣床、磨】
        PAR_MAC_CHAN_FLAG = PAR_MAC_CHAN_TYPE + 8,	// 通道选择标志
        PAR_MAC_CHAN_AX_FLAG = PAR_MAC_CHAN_FLAG + 8,	// 通道的轴显示标志，每个通道占用两个参数，共8*2 = 16个参数
        PAR_MAC_CHAN_CUR_FLAG = PAR_MAC_CHAN_AX_FLAG + 16,	// 通道的负载电流显示轴定制
        PAR_MAC_SHOW_AXES = 41,		// 是否动态显示坐标轴
        PAR_MAC_CALIB_TYPE,			// 刀具测量仪类型
        PAR_MAC_HOME_BLOCK = 48,	// 机床是否安装回零挡块
        PAR_MAC_AXES_NUM,			// 机床总轴数
        PAR_MAC_SMX_AXIS_NUM,		// 运动控制通道轴数（耦合从轴+PMC轴）
        PAR_MAC_SMX_AXIS_IDX,		// PMC及耦合从轴编号，预留32个

        PAR_MAC_TOOL = 100,	// 刀具处理参数预留

        PAR_MAC_FZONE_IN_MASK = 110,	//机床保护区内部禁止掩码
        PAR_MAC_FZONE_EX_MASK,		//机床保护区外部禁止掩码
        PAR_MAC_FZONE_BND,		//机床保护区边界-x +x -y +y -z +z 6*6=36

        PAR_MAC_HOME_DWELL = 165,	// 回参考点延时时间，单位：ms
        PAR_MAC_PLCACK_CYCLE,		// PLC应答最长时间
        PAR_MAC_G32_HOLD_DX,		// 螺纹加工中止的退刀距离
        PAR_MAC_G32_HOLD_ANG,		// 螺纹加工中止的退刀角度
        PAR_MAC_G64_CORNER_CHK,		// G64拐角准停校验检查使能

        PAR_MAC_MCODE_FLAG = 170,	// M代码属性表

        PAR_MAC_WKP_GMODE_SHOW = 220,	// 模态G指令显示定制，每个工位占用3个参数，共8*3 = 24个参数

        PAR_MAC_MEAS_SPD = 250,	// 测量速度
        PAR_MAC_MEAS_DIST,		// 测量最小行程

        PAR_MAC_IPSYNC_FUN = 260,	// 插补同步函数注册

        PAR_MAC_SPECIAL = 270,		// 专机预留参数起始地址

        PAR_MAC_CHECK_ENCRYPT = 298,	// 是否检查文件加密属性
        PAR_MAC_PROG_SKEY = 299,		// G代码文件密钥

        PAR_MAC_USER = 300,	// 用户参数基地址

        PAR_MAC_TOTAL = 500	// 机床用户参数总数
    }

    // 通道参数
    public enum ParCh
    {
        PAR_CH_NAME = 0,	// 名称
        PAR_CH_XINDEX,		// X轴编号
        PAR_CH_YINDEX,		// Y轴编号
        PAR_CH_ZINDEX,		// Z轴编号
        PAR_CH_AINDEX,		// A轴编号
        PAR_CH_BINDEX,		// B轴编号
        PAR_CH_CINDEX,		// C轴编号
        PAR_CH_UINDEX,		// U轴编号
        PAR_CH_VINDEX,		// V轴编号
        PAR_CH_WINDEX,		// W轴编号
        PAR_CH_SPDL0,		// 主轴0编号
        PAR_CH_SPDL1,		// 主轴1编号
        PAR_CH_SPDL2,		// 主轴2编号
        PAR_CH_SPDL3,		// 主轴3编号
        PAR_CH_X_NAME,		// X轴名
        PAR_CH_Y_NAME,		// Y轴名
        PAR_CH_Z_NAME,		// Z轴名
        PAR_CH_A_NAME,		// A轴名
        PAR_CH_B_NAME,		// B轴名
        PAR_CH_C_NAME,		// C轴名
        PAR_CH_U_NAME,		// U轴名
        PAR_CH_V_NAME,		// V轴名
        PAR_CH_W_NAME,		// W轴名
        PAR_CH_S0_NAME,		// 主轴0名
        PAR_CH_S1_NAME,		// 主轴1名
        PAR_CH_S2_NAME,		// 主轴2名
        PAR_CH_S3_NAME,		// 主轴3名
        PAR_CH_SVEL_SHOW,	// 主轴转速显示方式
        PAR_CH_S_SHOW,		// 主轴显示定制

        PAR_CH_DEFAULT_F = 30,	// 通道的缺省进给速度
        PAR_CH_DRYRUN_SPD,		// 空运行进给速度
        PAR_CH_DIAPROG,			// 直径编程使能
        PAR_CH_UVW_INC_EN,		// UVW增量编程使能
        PAR_CH_CHAMFER_EN,		// 倒角使能
        PAR_CH_ANGLEP_EN,		// 角度编程使能
        PAR_CH_CYCLE_OPTION,	// 复合循环选项屏蔽字[位]：0x0001 粗加工圆弧转直线 0x0002：凹槽轴向余量报警屏蔽 0x0004: 无精加工

        PAR_CH_MAC_FRAME = 40,	// 机床结构类型【0, 一般直角系机床 1, 通用五轴机床；2+其它机床】

        PAR_CH_FOLLOW_ROTATE_RAD = 60, //工具跟随的摆动半径
        PAR_CH_FOLLOW_CHORD_LEN, //弦线跟随的弦长

        PAR_CH_VPLAN_MODE = 69,	// 速度规划模式0-9:曲面模式  10+高速模式【激光、木工】 

        PAR_CH_MICR_MAX_LEN,	// 微线段上限长度
        PAR_CH_CORNER_MAX_ANG,	// 工艺尖角最大夹角
        PAR_CH_VEL_FILTER_LEN,	// 微线段速度滤波长度
        PAR_CH_PATH_TOLERANCE,	// 轨迹轮廓允差
        PAR_CH_GEO_CNTR_NUM,	// 微线段特征滤波段数
        PAR_CH_HSPL_MIN_LEN,	// 微线段下限长度
        PAR_CH_HSPL_MAX_ANG,	// 样条过渡夹角
        PAR_CH_HSPL_MAX_RAT,	// 样条平滑的相邻段最大长度比
        PAR_CH_HSPL_MAX_LEN,	// 样条平滑的最大线段长度
        PAR_CH_PATH_TYPE,		// 切削轨迹类型 79

        PAR_CH_LOOKAHEAD_NUM,	// 速度规划前瞻段数
        PAR_CH_CURVATURE_COEF,	// 曲率半径调整系数【0.3~100.0】
        PAR_CH_RECTIFY_NUM,		// 速度整定段数
        PAR_CH_POS_SMTH_NUM,	// 平滑段数
        PAR_CH_MAX_ECEN_ACC,	// 向心加速度
        PAR_CH_MAX_TANG_ACC,	//切向加速度

        PAR_CH_CYL_RAX = 90,	// 圆柱插补旋转轴号【缺省5、C轴】
        PAR_CH_CYL_LAX,			// 圆柱插补直线【轴向】轴号【缺省2、Z轴】
        PAR_CH_CYL_PAX,			// 圆柱插补平行【周向、纬线】轴号【缺省1、Y轴】

        PAR_CH_POLAR_LAX = 95,	// 极坐标插补的直线轴轴号
        PAR_CH_POLAR_RAX,		// 极坐标插补的旋转轴轴号
        PAR_CH_POLAR_VAX,		// 极坐标插补的假想轴轴号
        PAR_CH_POLAR_CX,		// 极坐标插补的旋转中心直线轴坐标
        PAR_CH_POLAR_CY,		// 极坐标插补假想轴偏心量

        PAR_CH_THREAD_TOL = 105,	// 螺纹起点允许偏差
        PAR_CH_THREAD_WAY,			// 螺纹加工方式


        PAR_CH_G61_DEFAULT,		// 系统上电时G61/G64模态设置
        PAR_CH_G00_DEFAULT,		// 系统上电时G00/G01模态设置
        PAR_CH_G90_DEFAULT,		// 系统上电时G90/G91模态设置
        PAR_CH_G28_ZTRAP_EN,	// G28搜索Z脉冲使能
        PAR_CH_G28_POS_EN,		// G28不寻Z脉冲时快移使能 【0 就进给速度定位 1 快移速度定位】
        PAR_CH_G28_ONE_SHOT,	// G29
        PAR_CH_SKIP_MODE,		// 任意行模式[0,扫描 1,跳转]

        // PAR_CH_G95_DEFAULT,		// 系统上电时G95/G94模态设置

        PAR_CH_MAG_START_NO = 125,	// 	起始刀库号
        PAR_CH_MAG_NUM,				// 	刀库数目
        PAR_CH_TOOL_START_NO,		// 	起始刀具号
        PAR_CH_TOOL_NUM,			// 	刀具数目
        PAR_CH_LIFE_ON,				// 	刀具寿命功能开启

        //  = 140 第2套小线段参数
        PAR_CH_MICR_MAX_LEN2 = 140,	// 微线段上限长度
        PAR_CH_CORNER_MAX_ANG2,		// 工艺尖角最大夹角
        PAR_CH_VEL_FILTER_LEN2,		// 微线段速度滤波长度
        PAR_CH_PATH_TOLERANCE2,		// 轨迹轮廓允差
        PAR_CH_GEO_CNTR_NUM2,		// 微线段特征滤波段数
        PAR_CH_HSPL_MIN_LEN2,		// 微线段下限长度
        PAR_CH_HSPL_MAX_ANG2,		// 样条过渡夹角
        PAR_CH_HSPL_MAX_RAT2,		// 样条平滑的相邻段最大长度比
        PAR_CH_HSPL_MAX_LEN2,		// 样条平滑的最大线段长度
        PAR_CH_PATH_TYPE2,			// 切削轨迹类型
        PAR_CH_LOOKAHEAD_NUM2,		// 速度规划前瞻段数
        PAR_CH_CURVATURE_COEF2,		// 曲率半径调整系数【0.3~100.0】
        PAR_CH_RECTIFY_NUM2,		// 速度整定段数
        PAR_CH_POS_SMTH_NUM2,		// 位置平滑段数

        //  = 160 第3套小线段参数
        PAR_CH_MICR_MAX_LEN3 = 160,	// 微线段上限长度
        PAR_CH_CORNER_MAX_ANG3,		// 工艺尖角最大夹角
        PAR_CH_VEL_FILTER_LEN3,		// 微线段速度滤波长度
        PAR_CH_PATH_TOLERANCE3,		// 轨迹轮廓允差
        PAR_CH_GEO_CNTR_NUM3,		// 微线段特征滤波段数
        PAR_CH_HSPL_MIN_LEN3,		// 微线段下限长度
        PAR_CH_HSPL_MAX_ANG3,		// 样条过渡夹角
        PAR_CH_HSPL_MAX_RAT3,		// 样条平滑的相邻段最大长度比
        PAR_CH_HSPL_MAX_LEN3,		// 样条平滑的最大线段长度
        PAR_CH_PATH_TYPE3,			// 切削轨迹类型
        PAR_CH_LOOKAHEAD_NUM3,		// 速度规划前瞻段数
        PAR_CH_CURVATURE_COEF3,		// 曲率半径调整系数【0.3~100.0】
        PAR_CH_RECTIFY_NUM3,		// 速度整定段数
        PAR_CH_POS_SMTH_NUM3,		// 位置平滑段数

        //  = 180 第4套小线段参数
        PAR_CH_MICR_MAX_LEN4 = 180,	// 微线段上限长度
        PAR_CH_CORNER_MAX_ANG4,		// 工艺尖角最大夹角
        PAR_CH_VEL_FILTER_LEN4,		// 微线段速度滤波长度
        PAR_CH_PATH_TOLERANCE4,		// 轨迹轮廓允差
        PAR_CH_GEO_CNTR_NUM4,		// 微线段特征滤波段数
        PAR_CH_HSPL_MIN_LEN4,		// 微线段下限长度
        PAR_CH_HSPL_MAX_ANG4,		// 样条过渡夹角
        PAR_CH_HSPL_MAX_RAT4,		// 样条平滑的相邻段最大长度比
        PAR_CH_HSPL_MAX_LEN4,		// 样条平滑的最大线段长度
        PAR_CH_PATH_TYPE4,			// 切削轨迹类型
        PAR_CH_LOOKAHEAD_NUM4,		// 速度规划前瞻段数
        PAR_CH_CURVATURE_COEF4,		// 曲率半径调整系数【0.3~100.0】
        PAR_CH_RECTIFY_NUM4,		// 速度整定段数
        PAR_CH_POS_SMTH_NUM4,		// 位置平滑段数

        PAR_CH_WTZONE_NUM = 200,	// 工件及刀具保护区总个数0~10
        PAR_CH_WTZONE_TYPE,			// 工件及刀具保护区类型
        PAR_CH_WTZONE_FLAG,			// 工件及刀具保护区属性
        PAR_CH_WTZONE_BND,			// 工件及刀具保护区边界

        PAR_CH_RESONA_DAMP_AMP = 300, // 主轴转速避振波幅【百分比 0.05】
        PAR_CH_RESONA_DAMP_PRD = 301, // 主轴转速避振周期【秒】

        PAR_CH_TAX_ENABLE = 310,	// 倾斜轴控制使能
        PAR_CH_TAX_ORTH_AX_INDEX,	// 正交轴轴号
        PAR_CH_TAX_TILT_AX_INDEX,	// 倾斜轴轴号
        PAR_CH_TAX_TILT_ANGLE,		// 倾斜角度

        //五轴参数
        PAR_CH_RTCPARA_OFF = 50,		//RTCP参数偏移值

        PAR_CH_TOOL_INIT_DIR_X = 400,	//刀具初始方向(X)
        PAR_CH_TOOL_INIT_DIR_Y,		//刀具初始方向(Y)
        PAR_CH_TOOL_INIT_DIR_Z,		//刀具初始方向(Z)
        PAR_CH_ANG_OUTPUT_MODE = 405,	//旋转轴角度输出判定方式
        PAR_CH_ANG_OUTPUT_ORDER,	//旋转轴角度输出判定顺序
        PAR_CH_POLE_TOLERANCE,		//极点角度范围

        PAR_CH_RTCP_SWIVEL_TYPE = 410,	//摆头结构类型
        PAR_CH_RTCP_SWIVEL_RAX1_DIR_X,	//摆头第1旋转轴方向(X)
        PAR_CH_RTCP_SWIVEL_RAX1_DIR_Y,	//摆头第1旋转轴方向(Y)
        PAR_CH_RTCP_SWIVEL_RAX1_DIR_Z,	//摆头第1旋转轴方向(Z)
        PAR_CH_RTCP_SWIVEL_RAX2_DIR_X,	//摆头第2旋转轴方向(X)
        PAR_CH_RTCP_SWIVEL_RAX2_DIR_Y,	//摆头第2旋转轴方向(Y)
        PAR_CH_RTCP_SWIVEL_RAX2_DIR_Z,	//摆头第2旋转轴方向(Z)
        PAR_CH_RTCP_SWIVEL_RAX1_OFF_X,	//摆头第1旋转轴偏移矢量(X)
        PAR_CH_RTCP_SWIVEL_RAX1_OFF_Y,	//摆头第1旋转轴偏移矢量(Y)
        PAR_CH_RTCP_SWIVEL_RAX1_OFF_Z,	//摆头第1旋转轴偏移矢量(Z)
        PAR_CH_RTCP_SWIVEL_RAX2_OFF_X,	//摆头第2旋转轴偏移矢量(X)
        PAR_CH_RTCP_SWIVEL_RAX2_OFF_Y,	//摆头第2旋转轴偏移矢量(Y)
        PAR_CH_RTCP_SWIVEL_RAX2_OFF_Z,	//摆头第2旋转轴偏移矢量(Z)

        PAR_CH_RTCP_TABLE_TYPE = 425,		//转台结构类型
        PAR_CH_RTCP_TABLE_RAX1_DIR_X,	//转台第1旋转轴方向(X)
        PAR_CH_RTCP_TABLE_RAX1_DIR_Y,	//转台第1旋转轴方向(Y)
        PAR_CH_RTCP_TABLE_RAX1_DIR_Z,	//转台第1旋转轴方向(Z)
        PAR_CH_RTCP_TABLE_RAX2_DIR_X,	//转台第2旋转轴方向(X)
        PAR_CH_RTCP_TABLE_RAX2_DIR_Y,	//转台第2旋转轴方向(Y)
        PAR_CH_RTCP_TABLE_RAX2_DIR_Z,	//转台第2旋转轴方向(Z)
        PAR_CH_RTCP_TABLE_RAX1_OFF_X,	//转台第1旋转轴偏移矢量(X)
        PAR_CH_RTCP_TABLE_RAX1_OFF_Y,	//转台第1旋转轴偏移矢量(Y)
        PAR_CH_RTCP_TABLE_RAX1_OFF_Z,	//转台第1旋转轴偏移矢量(Z)
        PAR_CH_RTCP_TABLE_RAX2_OFF_X,	//转台第2旋转轴偏移矢量(X)
        PAR_CH_RTCP_TABLE_RAX2_OFF_Y,	//转台第2旋转轴偏移矢量(Y)
        PAR_CH_RTCP_TABLE_RAX2_OFF_Z,	//转台第2旋转轴偏移矢量(Z)

        PAR_CH_TOTAL = 500
    }

    public enum ParaPropType
    {
        PARA_PROP_VALUE,	// 参数值 参数结构文件定义
        PARA_PROP_MAXVALUE,	// 最大值 参数结构文件定义
        PARA_PROP_MINVALUE,	// 最小值 参数结构文件定义
        PARA_PROP_DFVALUE,	// 缺省值 参数结构文件定义
        PARA_PROP_NAME,		// 名字  STRING
        PARA_PROP_ACCESS,	// 权限  Int32
        PARA_PROP_ACT,		// 生效方式 Int32
        PARA_PROP_STORE,	// 存储类型  Int32
        PARA_PROP_ID,		// 参数编号 Int32
        PARA_PROP_SIZE		// 大小 Int32
    }

    // 坐标轴参数
    public enum ParAxis
    {
        PAR_AX_NAME = 0,	// 轴名[显示用]
        PAR_AX_TYPE,		// 轴类型[直线、摆动、回转、主轴]
        PAR_AX_INDEX,		// 轴编号 暂时预留
        PAR_AX_MODN,		// 设备号 暂时预留
        PAR_AX_DEV_I = PAR_AX_MODN,
        PAR_AX_PM_MUNIT,	// 电子齿轮比分子(位移量)[每转位移量nm]
        PAR_AX_PM_PULSE,	// 电子齿轮比分母(脉冲数)[每转指令脉冲数]
        PAR_AX_PLMT,		// 正软极限
        PAR_AX_NLMT,		// 负软极限
        PAR_AX_PLMT2,		// 第2正软极限
        PAR_AX_NLMT2,		// 第2负软极限

        PAR_AX_HOME_WAY = 10,	// 回参考点方式
        PAR_AX_HOME_DIR,		// 回参考点方向
        PAR_AX_ENC_OFF,			// 编码器反馈偏置量【手动零点、绝对式编码器】
        PAR_AX_HOME_OFF,		// 回参考点后的偏移量
        PAR_AX_HOME_MASK,		// Z脉冲屏蔽角度
        PAR_AX_HOME_HSPD,		// 回参考点高速
        PAR_AX_HOME_LSPD,		// 回参考点低速
        PAR_AX_HOME_CRDS,		// 参考点坐标值
        PAR_AX_HOME_CODSPACE,	// 距离码参考点间距
        PAR_AX_HOME_CODOFF,		// 间距编码偏差

        PAR_AX_HOME_RANGE = 20,	// 搜Z脉冲最大移动距离
        PAR_AX_HOME_CRDS2,		// 第2参考点坐标值
        PAR_AX_HOME_CRDS3,		// 第3参考点坐标值
        PAR_AX_HOME_CRDS4,		// 第4参考点坐标值
        PAR_AX_HOME_CRDS5,		// 第5参考点坐标值
        PAR_AX_REF_RANGE,		// 参考点范围偏差
        PAR_AX_HOME_CYCLE_OFF,	// 非整传动比回转轴偏差
        PAR_AX_ENC2_OFF,		// 第2编码器反馈偏置量【手动零点、绝对式编码器】
        PAR_AX_PM2_MUNIT,		// 第2编码器电子齿轮比分子(位移量)[每转位移量nm]
        PAR_AX_PM2_PULSE,		// 第2编码器电子齿轮比分母(脉冲数)[每转指令脉冲数]

        PAR_AX_G60_OFF = 30,	// 单向定位(G60)偏移量
        PAR_AX_ROT_RAD,			// 转动轴当量半径
        PAR_AX_JOG_LOWSPD,		// 慢速点动速度
        PAR_AX_JOG_FASTSPD,		// 快速点动速度
        PAR_AX_RAPID_SPD,		// 快移速度
        PAR_AX_FEED_SPD,		// 最高进给速度
        PAR_AX_RAPID_ACC,		// 快移加速度
        PAR_AX_RAPID_JK,		// 快移捷度
        PAR_AX_FEED_ACC,		// 进给加速度
        PAR_AX_FEED_JK,			// 进给捷度
        PAR_AX_THREAD_ACC,		// 螺纹加速度
        PAR_AX_THREAD_DEC,		// 螺纹减速度
        PAR_AX_MPG_UNIT_SPD,	// 手摇单位速度比例系数
        PAR_AX_MPG_RESOL,		// 手摇脉冲分辨率
        PAR_AX_MPG_INTE_RATE,	// 手摇缓冲系数
        PAR_AX_MPG_INTE_TIME,	// 手摇缓冲周期 [45]
        PAR_AX_MPG_OVER_RATE,	// 手摇过冲系数
        PAR_AX_MPG_VEL_GAIN,	// 手摇速度调节系数

        PAR_AX_DEFAULT_S = 50,	// 主轴缺省转速
        PAR_SPDL_MAX_SPEED,		// 主轴最大转速
        PAR_SPDL_SPD_TOL,		// 主轴转速允许转速波动率
        PAR_SPDL_SPD_TIME,		// 主轴转速到达允许最大时间
        PAR_SPDL_THREAD_TOL,	// 螺纹加工时的转速允差
        PAR_AX_SP_ORI_POS,		// 进给主轴定向角度
        PAR_AX_SP_ZERO_TOL,		// 进给主轴零速允差【脉冲】
        PAR_AX_MAX_EXT_PINC,	// 外部指令最大周期叠加量

        PAR_AX_POS_TOL = 60,	// 定位允差
        PAR_AX_MAX_LAG,			// 最大跟随误差
        PAR_AX_LAG_CMP_EN,		// 龙门轴同步误差补偿使能
        PAR_AX_LAG_CMP_COEF,	// 跟随误差补偿调整系数
        PAR_AX_LAG_CMP_CNT,		// 动态补偿系数整定周期数

        PAR_AX_ATEETH = 65,	// 传动比分子[轴侧齿数]
        PAR_AX_MTEETH,		// 传动比分母[电机侧齿数]
        PAR_AX_MT_PPR,		// 电机每转脉冲数
        PAR_AX_PITCH,		// 丝杆导程
        PAR_AX_RACK_NUM,	// 齿条齿数
        PAR_AX_RACK_SPACE,	// 齿条齿间距
        PAR_AX_WORM_NUM,	// 蜗杆头数
        PAR_AX_WORM_SPACE,	// 蜗杆齿距
        PAR_RAX_VEL_RATE,   // 旋转轴速度系数
        PAR_AX_RATING_CUR,  // 额定电流
        PAR_AX_POWER_RATE,  // 功率系数
        PAR_AX_ENC2_PPR,	// 第2编码器每转脉冲数
        PAR_AX_INDEX_TYPE, //分度轴类型：1、鼠牙盘；2，分度轴
        PAR_AX_INDEX_POS,//分度起点
        PAR_AX_INDEX_DIVIDE, //分度间隔

        PAR_ZAX_LOCK_EN = 80,	// Z轴锁允许使能
        PAR_RAX_ROLL_EN,		// 旋转轴循环使能
        PAR_RAX_SHORTCUT,		// 旋转轴短路径选择使能
        PAR_RAX_CYCLE_RANGE,	// 旋转轴循环行程
        PAR_RAX_DISP_RANGE,		// 旋转轴显示角度范围
        PAR_LAX_PROG_UNIT,		// 直线轴编程指令最小单位
        PAR_RAX_PROG_UNIT,		// 旋转轴编程指令最小单位

        PAR_AX_ENC_MODE = 90,	// 编码器工作模式

        PAR_AX_EC1_TYPE,		// 1号编码器类型【增量、距离码、绝对】
        PAR_AX_EC1_OUTP,		// 反馈电子齿轮比分子[输出脉冲数]
        PAR_AX_EC1_FBKP,		// 反馈电子齿轮比分母[反馈脉冲数]
        PAR_AX_EC1_BIT_N,		// 1号编码器计数位数【绝对式必填】
        PAR_AX_EC2_TYPE,		// 2号编码器类型【增量、距离码、绝对】
        PAR_AX_EC2_OUTP,		// 反馈电子齿轮比分子[输出脉冲数] 
        PAR_AX_EC2_FBKP,		// 反馈电子齿轮比分母[反馈脉冲数]
        PAR_AX_EC2_BIT_N,		// 2号编码器计数位数【绝对式必填】

        PAR_AX_SMX_TYPE = 100,	// 运动控制(MC)轴类型
        PAR_AX_SMX_LEAD_IDX,
        PAR_AX_COMPEN_LAG = 106,
        PAR_AX_ALARM_LAG,
        PAR_AX_ALARM_VDIFF,
        PAR_AX_ALARM_CDIFF,
        PAR_AX_SMX_PARA,		// MC轴运动系数，16



        PAR_AX_COMP_MAX_COEF = 130,	// 最大误差补偿率
        PAR_AX_COMP_MAX_VALUE,		// 最大误差补偿值
        PAR_AX_CODOFF_VALUE,		// 轴反馈偏差

        PAR_AX_DYNAMIC_PM,			//允许动态切换电子齿轮比
        PAR_AX_SHELF1_ATEETH,		//1档齿轮比分子
        PAR_AX_SHELF1_MTEETH,		//1档齿轮比分母
        PAR_AX_SHELF2_ATEETH,		//2档齿轮比分子
        PAR_AX_SHELF2_MTEETH,		//2档齿轮比分母
        PAR_AX_SHELF3_ATEETH,		//3档齿轮比分子
        PAR_AX_SHELF3_MTEETH,		//3档齿轮比分母
        PAR_AX_SHELF4_ATEETH,		//4档齿轮比分子
        PAR_AX_SHELF4_MTEETH,		//4档齿轮比分母
        PAR_AX_SHELF1_ORI_POS,		//1档定向角度
        PAR_AX_SHELF2_ORI_POS,		//2档定向角度
        PAR_AX_SHELF3_ORI_POS,		//3档定向角度
        PAR_AX_SHELF4_ORI_POS,		//4档定向角度

        PAR_AX_TANG_NO,				//切线控制随动轴轴号0，1，2代表A，B，C轴
        PAR_AX_TANG_ANGLE,			//切线控制偏移角

        PAR_AX_ENC_TOLERANCE = 197,	// 断电位置允差
        PAR_AX_OUT_ACTVEL = 198,	// 实际速度超速判断周期
        PAR_AX_INTEG_PRD = 199,		// 显示速度积分周期数

        // 伺服参数（进给轴，预留100个）
        PAR_SV_POSITION_GAIN = HncDataDef.SERVO_PARM_START_IDX,	// 位置比例增益
        PAR_SV_POS_FF_GAIN,				// 位置前馈增益
        PAR_SV_SPEED_GAIN,				// 速度比例增益
        PAR_SV_SPEED_KI,				// 速度积分时间常数
        PAR_SV_SPEED_FB_FILTER,			// 速度反馈滤波因子
        PAR_SV_MOTOR_TYPE = 243,		// 驱动器电机类型代码
        PAR_SV_MOTOR_RATING_CUR = 286,	// 电机额定电流

        // 伺服参数（主轴，预留100个）
        PAR_SP_POSITION_GAIN = HncDataDef.SERVO_PARM_START_IDX,	// 位置控制位置比例增益
        PAR_SP_MOTOR_RATING_CUR = 253,	// IM电机额定电流
        PAR_SP_MOTOR_TYPE = 259,		// 驱动器电机类型代码

        PAR_AX_TOTAL = 500
    }

    // 误差补偿参数
    public enum ParCmp
    {
        PAR_CMP_BL_ENABLE = 0,		// 反向间隙补偿类型
        PAR_CMP_BL_VALUE,			// 反向间隙补偿值
        PAR_CMP_BL_RATE,			// 反向间隙补偿率
        PAR_CMP_BL_VALUE2,			// 第2反向间隙补偿值（快移反向间隙补偿值）

        PAR_CMP_HEAT_TYPE = 5,		// 热误差补偿类型
        PAR_CMP_HEAT_REFN,			// 热误差补偿参考点坐标
        PAR_CMP_HEAT_WARP_START,	// 热误差偏置表起始温度
        PAR_CMP_HEAT_WARP_NUM,		// 热误差偏置表温度点数
        PAR_CMP_HEAT_WARP_STEP,		// 热误差偏置表温度间隔
        PAR_CMP_HEAT_WARP_SENSOR,	// 热误差偏置表传感器编号
        PAR_CMP_HEAT_WARP_TABLE,	// 热误差偏置表起始参数号
        PAR_CMP_HEAT_SLOPE_START,	// 热误差斜率表起始温度
        PAR_CMP_HEAT_SLOPE_NUM,		// 热误差斜率表温度点数
        PAR_CMP_HEAT_SLOPE_STEP,	// 热误差斜率表温度间隔
        PAR_CMP_HEAT_SLOPE_SENSOR,	// 热误差斜率表传感器编号
        PAR_CMP_HEAT_SLOPE_TABLE,	// 热误差斜率表起始参数号
        PAR_CMP_HEAT_RATE,			// 热误差补偿率

        PAR_CMP_PITCH_TYPE = 20,	// 螺距误差补偿类型
        PAR_CMP_PITCH_START,		// 螺距误差补偿起点坐标
        PAR_CMP_PITCH_NUM,			// 螺距误差补偿点数
        PAR_CMP_PITCH_STEP,			// 螺距误差补偿点间距
        PAR_CMP_PITCH_MODULO,		// 螺距误差取模补偿使能
        PAR_CMP_PITCH_FACTOR,		// 螺距误差补偿倍率
        PAR_CMP_PITCH_TABLE,		// 螺距误差补偿表起始参数号

        PAR_CMP_SQU1_ENABLE = 30,	// 第1项垂直度补偿使能
        PAR_CMP_SQU1_INPUT_AX,	    // 第1项垂直度补偿基准轴号
        PAR_CMP_SQU1_REFN,	        // 第1项垂直度补偿基准点坐标
        PAR_CMP_SQU1_ANG,	        // 第1项垂直度补偿角度

        PAR_CMP_SQU2_ENABLE = 40,	// 第2项垂直度补偿使能
        PAR_CMP_SQU2_INPUT_AX,	    // 第2项垂直度补偿基准轴号
        PAR_CMP_SQU2_REFN,	        // 第2项垂直度补偿基准点坐标
        PAR_CMP_SQU2_ANG,	        // 第2项垂直度补偿角度

        PAR_CMP_STRA1_INPUT_AX = 50,	// 第1项直线度补偿基准轴号
        PAR_CMP_STRA1_TYPE,	        // 第1项直线度补偿类型
        PAR_CMP_STRA1_START,	    // 第1项直线度补偿起点坐标
        PAR_CMP_STRA1_NUM,	        // 第1项直线度补偿点数
        PAR_CMP_STRA1_STEP,	        // 第1项直线度补偿点间距
        PAR_CMP_STRA1_MODULO,	    // 第1项直线度取模补偿使能
        PAR_CMP_STRA1_FACTOR,	    // 第1项直线度补偿倍率
        PAR_CMP_STRA1_TABLE,	    // 第1项直线度补偿表起始参数号

        PAR_CMP_STRA2_INPUT_AX = 65,	// 第2项直线度补偿基准轴号
        PAR_CMP_STRA2_TYPE,	        // 第2项直线度补偿类型
        PAR_CMP_STRA2_START,	    // 第2项直线度补偿起点坐标
        PAR_CMP_STRA2_NUM,	        // 第2项直线度补偿点数
        PAR_CMP_STRA2_STEP,	        // 第2项直线度补偿点间距
        PAR_CMP_STRA2_MODULO,	    // 第2项直线度取模补偿使能
        PAR_CMP_STRA2_FACTOR,	    // 第2项直线度补偿倍率
        PAR_CMP_STRA2_TABLE,    	// 第2项直线度补偿表起始参数号

        PAR_CMP_ANG1_INPUT_AX = 80,	// 第1项角度补偿基准轴号
        PAR_CMP_ANG1_ASSO_AX,	    // 第1项角度补偿关联轴号
        PAR_CMP_ANG1_REFN,          // 第1项角度补偿参考点坐标
        PAR_CMP_ANG1_TYPE,	        // 第1项角度补偿类型
        PAR_CMP_ANG1_START,	        // 第1项角度补偿起点坐标
        PAR_CMP_ANG1_NUM,	        // 第1项角度补偿点数
        PAR_CMP_ANG1_STEP,	        // 第1项角度补偿点间距
        PAR_CMP_ANG1_MODULO,	    // 第1项角度取模补偿使能
        PAR_CMP_ANG1_FACTOR,	    // 第1项角度补偿倍率
        PAR_CMP_ANG1_TABLE,	        // 第1项角度补偿表起始参数号

        PAR_CMP_ANG2_INPUT_AX = 95,	// 第2项角度补偿基准轴号
        PAR_CMP_ANG2_ASSO_AX,	    // 第2项角度补偿关联轴号
        PAR_CMP_ANG2_REFN,          // 第2项角度补偿参考点坐标
        PAR_CMP_ANG2_TYPE,	        // 第2项角度补偿类型
        PAR_CMP_ANG2_START,	        // 第2项角度补偿起点坐标
        PAR_CMP_ANG2_NUM,	        // 第2项角度补偿点数
        PAR_CMP_ANG2_STEP,	        // 第2项角度补偿点间距
        PAR_CMP_ANG2_MODULO,	    // 第2项角度取模补偿使能
        PAR_CMP_ANG2_FACTOR,	    // 第2项角度补偿倍率
        PAR_CMP_ANG2_TABLE,	        // 第2项角度补偿表起始参数号

        PAR_CMP_ANG3_INPUT_AX = 110,	// 第3项角度补偿基准轴号
        PAR_CMP_ANG3_ASSO_AX,	    // 第3项角度补偿关联轴号
        PAR_CMP_ANG3_REFN,          // 第3项角度补偿参考点坐标
        PAR_CMP_ANG3_TYPE,	        // 第3项角度补偿类型
        PAR_CMP_ANG3_START,	        // 第3项角度补偿起点坐标
        PAR_CMP_ANG3_NUM,	        // 第3项角度补偿点数
        PAR_CMP_ANG3_STEP,	        // 第3项角度补偿点间距
        PAR_CMP_ANG3_MODULO,	    // 第3项角度取模补偿使能
        PAR_CMP_ANG3_FACTOR,	    // 第3项角度补偿倍率
        PAR_CMP_ANG3_TABLE,	        // 第3项角度补偿表起始参数号

        PAR_CMP_QUAD_ENABLE = 125,	// 过象限突跳补偿类型
        PAR_CMP_QUAD_VALUE,         // 过象限突跳补偿值
        PAR_CMP_QUAD_DELAY_T,		// 过象限突跳补偿延时时间，单位：ms
        PAR_CMP_QUAD_MIN_VEL,		// 过象限突跳补偿最低速度
        PAR_CMP_QUAD_MAX_VEL,		// 过象限突跳补偿最高速度
        PAR_CMP_QUAD_ACC_T,			// 过象限突跳补偿加速时间，单位：ms
        PAR_CMP_QUAD_DEC_T,			// 过象限突跳补偿减速时间，单位：ms
        PAR_CMP_QUAD_TRQ_VAL,		// 静摩擦补偿扭矩值，取值范围：-10000~10000

        PAR_CMP_MULHT_TYPE = 135,		// 多元线性补偿类型
        PAR_CMP_MULHT_REFN,				// 多元线性补偿参考点坐标
        PAR_CMP_MULHT_BASE_WARP,		// 主轴偏置模型常量
        PAR_CMP_MULHT_WARP_SEN_NUM,		// 主轴偏置模型传感器接入个数
        PAR_CMP_MULHT_WARP_SEN_LIST,    // 主轴偏置模型传感器编号序列
        PAR_CMP_MULHT_WARP_COEF_TABLE,  // 主轴偏置模型系数表起始参数号
        PAR_CMP_MULHT_BASE_SLOPE,       // 丝杆斜率模型常量
        PAR_CMP_MULHT_SLOPE_SEN_NUM,    // 丝杆斜率模型传感器接入个数
        PAR_CMP_MULHT_SLOPE_SEN_LIST,   // 丝杆斜率模型传感器编号序列
        PAR_CMP_MULHT_SLOPE_COEF_TABLE, // 丝杆斜率模型系数表起始参数号

        PAR_CMP_SPECIAL = 150,	// 专机预留/扩展参数起始地址
        PAR_CMP_TOTAL = 200		// 轴补偿参数总个数
    }

    // 设备接口参数
    public enum ParDevInfo
    {
        PAR_DEV_NAME,				// 设备名称
        PAR_DEV_INDEX,				// 设备的系统序号，在系统全部设备中的序号
        PAR_DEV_TYPE,				// 设备类型
        PAR_DEV_GRP_IDX,			// 在同组设备中的序号
        PAR_DEV_ID,					// 设备ID[生产唯一]
        PAR_DEV_VENDOR,				// 生产商
        PAR_DEV_READONLY_NUM = 8,	// 保留
        PAR_DEV_MODE,				// 设备数据字长
        PAR_DEV_GNL_NUM				// [设备通用参数的个数]
    }

    // 设备--主站设备
    public enum ParDevBrd
    {
        // 系统固化参数
        PAR_DEV_BRD_FPGA_VER = ParDevInfo.PAR_DEV_GNL_NUM,	// FPGA固件程序版本号
        PAR_DEV_BRD_CARD_VER, // 主站卡版本号

        PAR_DEV_BRD_SYS_OBJ_NUM = (ParDevInfo.PAR_DEV_GNL_NUM + 10),	// 本地控制对象个数，保留+追加
        PAR_DEV_BRD_NET_OBJ_NUM,	// 总线从站控制对象个数
        PAR_DEV_BRD_OBJ_NUM,		// 控制对象总个数，本地+总线从站

        // 用户配置参数
        PAR_DEV_BRD_BUS_CYCLE = (ParDevInfo.PAR_DEV_GNL_NUM + 40),	// 总线通讯周期
        PAR_DEV_BRD_BUS_REQ_TIMES,	// 总线通讯请求次数
        PAR_DEV_BRD_BUS_TOPO,		// 拓扑结构（保留）

        PAR_DEV_BRD_SP_ADD_NUM = (ParDevInfo.PAR_DEV_GNL_NUM + 50),	// 追加模拟量主轴数

        PAR_DEV_BRD_RESV_TYPE = (ParDevInfo.PAR_DEV_GNL_NUM + 60)	// 本地保留设备类型，预留10个，可扩展

    }

    // 设备--AX
    public enum ParDevAx
    {
        PAR_DEV_AX_MODE = ParDevInfo.PAR_DEV_GNL_NUM,	// 工作模式
        PAR_DEV_AX_IDX,			// 设备对应的逻辑轴号
        PAR_DEV_AX_ENCOD_DIR,	// 编码器反馈取反标志
        PAR_DEV_AX_CMD_TYPE,	// 主轴DA输出类型
        PAR_DEV_AX_CYC_EN,      // 反馈位置循环使能
        PAR_DEV_AX_MT_PPR,		// 反馈位置循环脉冲数
        PAR_DEV_AX_ENCOD_TYPE,  // 编码器类型
        PAR_DEV_AX_RESERVE1,	// 保留1
        PAR_DEV_AX_RESERVE2,	// 保留2
        PAR_DEV_AX_RESERVE3     // 保留3
    }

    // 设备--MPG
    public enum ParDevMpg
    {
        PAR_DEV_MPG_TYPE = ParDevInfo.PAR_DEV_GNL_NUM,	// MPG类型
        PAR_DEV_MPG_IDX,					// MPG编号
        PAR_DEV_MPG_IN,						// 档位输入点组号
        PAR_DEV_MPG_DIR_FLAG,				// 各轴方向取反标志
        PAR_DEV_MPG_MULT_FACTOR,			// 倍率放大系数
        PAR_DEV_MPG_PAR_NUM					// MPG实际配置参数数目
    }

    // 设备--NCKB
    public enum ParDevNck
    {
        PAR_DEV_NCKB_TYPE = ParDevInfo.PAR_DEV_GNL_NUM,	// NCKB键盘类型
        PAR_DEV_NCKB_IDX,		// NCKB编号
        PAR_DEV_NCKB_CYCLE,		// NCKB扫描周期
        PAR_DEV_NCKB_PAR_NUM	// NCKB实际配置参数数目
    }

    // 设备--IO_LOC/IO_NET
    public enum ParDevIo
    {
        PAR_DEV_IO_WATCHDOG = ParDevInfo.PAR_DEV_GNL_NUM,	// 是否包含看门狗（保留）
        PAR_DEV_IO_AXIS_CTRL,	// 是否是轴控制板（保留）
        PAR_DEV_IN_X_BASE,		// 输入点X寄存器起始组号(PAR_DEV_GNL_NUM+2)
        PAR_DEV_IN_X_GRPN,		// 输入点组数
        PAR_DEV_OUT_Y_BASE,		// 输出点Y寄存器起始组号
        PAR_DEV_OUT_Y_GRPN,		// 输出点组数
        PAR_DEV_ENCOD1_TYPE,	// 编码器1类型
        PAR_DEV_ENCOD1_PPR,		// 编码器1每转脉冲数
        PAR_DEV_ENCOD2_TYPE,	// 编码器2类型
        PAR_DEV_ENCOD2_PPR,		// 编码器2每转脉冲数
    }

    // 设备--MCP_LOC/MCP_NET
    public enum ParDevMcp
    {
        PAR_DEV_MCP_TYPE = ParDevInfo.PAR_DEV_GNL_NUM,	// MCP类型：1-A / 2-B /3-C
        PAR_DEV_MCP_MPG_IDX,	// 手摇编号
        PAR_DEV_MCP_X_BASE,     // 输入点X寄存器起始组号(PAR_DEV_GNL_NUM+2)
        PAR_DEV_MCP_X_GRPN,     // 输入点占用X寄存器组数
        PAR_DEV_MCP_Y_BASE,     // 输出点寄存器起址
        PAR_DEV_MCP_Y_GRPN,     // 输出点占用Y寄存器组数
        PAR_DEV_MCP_MPG_DIR,    // 手摇方向取反标志
        PAR_DEV_MCP_MPG_MULT,   // 手摇倍率放大系数
        PAR_DEV_MCP_CODE_TYPE,  // 波段开关编码类型
        PAR_DEV_MCP_SPDL_NUM    // 追加模拟量主轴数（temp）
    }

    // 设备--SERIAL
    public enum ParSerial
    {
        PAR_SERIAL_BIT_LEN = ParDevInfo.PAR_DEV_GNL_NUM,   // 收发数据位长度
        PAR_SERIAL_STOP,		// 停止位
        PAR_SERIAL_PARITY,		// 奇偶校验位
        PAR_SERIAL_BAUDRATE,	// 波特率
        PAR_SERIAL_PAR_NUM		// SERIAL实际配置参数数目
    }

    // 设备--LAN
    public enum ParLan
    {
        PAR_LAN_IP0 = ParDevInfo.PAR_DEV_GNL_NUM, // IP0
        // PAR_LAN_IP1,	// IP1
        // PAR_LAN_IP2,	// IP2
        // PAR_LAN_IP3,	// IP3
        PAR_LAN_GATE0,	// GATE0
        // PAR_LAN_GATE1,	// GATE1
        // PAR_LAN_GATE2,	// GATE2
        // PAR_LAN_GATE3,	// GATE3
        PAR_LAN_MASK0,	    // MASK0
        // PAR_LAN_MASK1,	// MASK1
        // PAR_LAN_MASK2,	// MASK2
        // PAR_LAN_MASK3,	// MASK3
        PAR_LAN_PAR_NUM     // LAN实际配置参数数目
    }

    // 设备--WCOM无线通讯接口
    public enum ParWcom
    {
        PAR_WCOM_TYPE = ParDevInfo.PAR_DEV_GNL_NUM, // WCOM类型
        PAR_WCOM_RESERVE,	// 保留
        PAR_WCOM_PAR_NUM	// WCOM实际配置参数数目
    }

    // 设备--GATHER采集卡
    public enum ParGather
    {
        PAR_GATHER_TYPE = ParDevInfo.PAR_DEV_GNL_NUM,	// 采集卡类型 1:KZM-6000
        PAR_GATHER_SERIAL_IDX,    // 串口设备号
        PAR_GATHER_SENSOR_NUM,    // 传感器数
        PAR_GATHER_IN_X_BASE,     // 输入点起始组号
        PAR_GATHER_PAR_NUM        // GATHER实际配置参数数目
    }

    //系统变量
    public enum SystemVar
    {
        VAR_DECODER_SIZE,
        VAR_CHANCTRL_SIZE,
        VAR_AXISCTRL_SIZE,
        VAR_SMXCTRL_SIZE,
        VAR_SYS_CRDS_CHAN = 4,//Monitor正在设置的坐标系所属通道[最多4个Monitor]
        VAR_SYS_DISP_CHAN = 8,//Monitor的显示通道记录[最多4个]
        //	VAR_SYS_ACT_CHAN=12,//8个工位的活动通道
        //VAR_SYS_MUTEX_FLAG=20,//系统全局锁4*32=128个
        VAR_SYS_SEMPHORE = 20,//系统全局信号量40个【16位】

        VAR_SYS_G5EX_IDCH = 40, //60个扩展工件坐标系的ID及通道号,低16位为轴掩码，高16位为通道号
        VAR_SYS_G5EX_ZERO = 100,	//60个扩展工件坐标系零点60*18[64位]=1080

        VAR_SYS_G68_POINT = 1180, //12*20 G68的两个辅助点
        VAR_SYS_G68_ZERO = 1420, //6*20 [Bit64]
        VAR_SYS_G68_VCT = 1540, //18*20个[fBit64] x y z三个轴的向量

        //MDI 信息
        VAR_SYS_MDI_MODE = 1900, //MDI模式:mode ret
        VAR_SYS_MDI_ROW,		//MDI输入程序行数
        VAR_SYS_MDI_CHAN,		//MDI运行的通道
        VAR_SYS_SMX_NUM0,		//静态耦合轴个数
        VAR_SYS_SMX_NUM,		//总耦合轴个数
        VAR_DCDVAR_OFFSET,		//解释器中的局部变量偏移量

        VAR_SYS_ALARM_COPY = 1910,	//1910~1917
        VAR_SYS_ALARM_FLAG = 1918,	//1918~1925
        VAR_SYS_NOTE_COPY = 1926,	//1926~1937
        VAR_SYS_NOTE_FLAG = 1938,	//1938~1949

        VAR_SYS_CHG_WCS_N = 1950,	//进给保持时，修改的工件坐标系数
        VAR_SYS_CHG_WCS_I,	//1951~1958，进给保持时，修改的工件坐标系所属的通道号及工件坐标编号
        VAR_SYS_CHG_TOOL_N = 1959,	//进给保持时，修改的刀具个数
        VAR_SYS_CHG_TOOL_I = 1960,	//1960~1969 进给保持时，修改的刀具编号 
        VAR_SYS_G5X_TEMP = 1970,		//1970~2113 八个进给保持时的临时工件坐标系 8*18 = 144

        //列表信息
        VAR_SYS_TAB_NUM = 2144, //列表个数 50个列表的信息
        VAR_SYS_TAB_COL,		//第一个列表的列数及主列数
        VAR_SYS_TAB_OFF,		//第一个列表数据的起始地址偏移
        //	VAR_SYS_TAB_COL,		//第n个列表的列数及主列数
        //	VAR_SYS_TAB_OFF,		//2145 ~ 2244 第n个列表数据的起始地址偏移

        VAR_SYS_G31_8 = 2245,		//G31.8测量点缓存 #42245 ~ #42499	
        VAR_SYS_DEBUG_INF = 2500,	//调试信息42500 ~ 43499 
        VAR_SMPL_OFFSET = 3500,		//3500~3515采样偏移量设置,按采样通道1~16排序
        VAR_SMPL_DATA_LEN = 3516,		//3516~3531 采样数据长度设置,按采样通道1~16排序
        VAR_SMPL_PERIOD = 3532,		//采样周期设置,为插补周期的自然数倍
        VAR_SMPL_CHN_STATE = 3548,	//3548采样通道状态
        VAR_SMPL_LMT = 3564,			//3564~3579采样截止条件 -1 信号截止；0 循环采样； 1~32766：个数,按采样通道1~16排序
        VAR_SMPL_IDX = 3580,			//3580~3595	采样通道写指针，按采样通道1~16排序
        VAR_SMPL_READ_PT = 3596,	//3596~3601 采样通道读指针，按采样通道1~16排序

        VAR_SYS_TEACH_IN = 5000,	//示教记录 #45000 ~ #49999	
        VAR_SYS_TOTAL = HncDataDef.VAR_SYS_VNUM //10000
    }

    // 通道变量
    public enum ChanVar
    {
        VAR_CHAN_SEL_PROG = 0,//选择的程序编号
        VAR_CHAN_DEST_ROW,//运行/跳转到的目标程序行
        VAR_CHAN_DCD_ROW,//正在解释的行
        VAR_CHAN_DCD_OROW,	//解释输出的程序行
        VAR_CHAN_RUN_ROW,	//正在运行的程序行
        VAR_CHAN_FIN_ROW,	//已经完成的程序行
        VAR_CHAN_DCD_PROG,	//正在解释的程序
        VAR_CHAN_RUN_PROG,	//正在运行的程序
        VAR_CHAN_MAIN_ROW,	//运行的主程序行
        VAR_CHAN_PART_STATI,	//加工件数统计

        VAR_CHAN_CMD_FEED,	//通道合成指令速度
        VAR_CHAN_ACT_FEED,	//通道实际合成速度
        VAR_CHAN_SYNC_FLAG, //同步标记	
        VAR_CHAN_BP_ID,	//断点轴标记
        VAR_CHAX_G5X_ID,	//G5x的标志位
        VAR_CHAX_G55_ID,
        VAR_CHAX_G56_ID,
        VAR_CHAX_G57_ID,
        VAR_CHAX_G58_ID,
        VAR_CHAX_G59_ID,

        VAR_CHAX_G54_ZERO = 20,	//40 轴G5X零点，以下是64位整型变量
        VAR_CHAX_G55_ZERO = 38,
        VAR_CHAX_G56_ZERO = 56,
        VAR_CHAX_G57_ZERO = 74,
        VAR_CHAX_G58_ZERO = 92,
        VAR_CHAX_G59_ZERO = 110,
        VAR_CHAX_G92_ZERO = 128,  //轴G92零点 90
        VAR_CHAX_REL_ZERO = 146,	//轴相对坐标系零点
        VAR_CHAN_LEFT_TOGO = 164,	//九个逻辑轴剩余进给[插补后值]
        VAR_CHAX_BP_POS = 182,	//断点位置[插补后值]

        //通道译码器模态
        VAR_DCD_MDL_DATA = 200,
        VAR_DCD_AX_FLAG = VAR_DCD_MDL_DATA, //axis1,axis2,axis3,resvb8;	//坐标平面两个轴标号，第3轴号,当前活动坐标系：53 54~59 92,
        VAR_DCD_G928_FLAG, //uBit16 g92_flag,g28_axes;//G92、G28定义的轴标记
        VAR_DCD_SPDL_IDX,//Bit16 s_i,reserved;	//当前主轴号，用于螺纹
        VAR_DCD_RAAX_FLAG, //Bit16 r_ax,a_ax;	//径向轴，轴向轴，用于支持纵切机
        VAR_DCD_CMD_FLAG,//uBit16 flag1,flag2;
        VAR_DCD_SPDL_SPD,//Bit32 s[CHAN_SPDL_NUM],t,t_r,t_l;
        VAR_DCD_G93_L = 212,//Bit16 g93_l,mem_grp,mem_g1,mem_g2;//G93的参数
        VAR_DCD_POLAR_CYL = 214,//Bit64 polar_x0,polar_y0,cyl_r;//极坐标原点，柱坐标插补的半径
        VAR_DCD_WCS_ZERO = 220,//Bit64 wzero[9]当前的工件坐标系零点 含G92
        VAR_DCD_PRG_POS = 238, //Bit64 prg_pos[9]编程坐标位置
        VAR_DCD_TRNS_POS = 256, //Bit64 trns_pos[9]变换坐标位置
        VAR_DCD_MCS_POS = 274, //Bit64 mcs_zero[9]机床坐标位置
        VAR_DCD_G28_POS = 292, //Bit64 g28[9]G28中间点位置
        VAR_DCD_G52_ZERO = 310, //Bit64 g52[9]G52原点位置
        VAR_DCD_G106_POS = 328, //Bit64 g106[9]G106回退点位置
        VAR_DCD_G106_FLAG = 346, //uBit16 g106_axes,cyl_ax,m345,gvel_ax;
        VAR_DCD_F_CMD = 348,//fBit64 g94_f,reserv_f,g32_f,g95_f;	F指令，乘了单位倍率的 
        VAR_DCD_CRDS_I = 356,
        VAR_DCD_CYC_AX = 358,
        VAR_DCD_CYC_REG,
        VAR_DCD_MDL_GRP = 360, //G组模态，解释器模态 360~399 80组
        VAR_DCD_MDL_REG = 400, //~599 SDataUnion regvar[LOCAL_VAR_NUM];

        //通道内的交互操作数据区
        VAR_IIP_CMD_TYPE = 600,//当前执行的指令/加工特征类型，用于显示

        VAR_SMPL_AX_FLAG,//低16位 采样轴标记 高16位 G31状态

        VAR_DATA_FILE,	//保存或加载数据文件名，不超过7字符
        VAR_DATA_FILE2,
        VAR_DATA_TYPE,	//保存或加载数据的类型[0 G代码程序（只对加载有效） 1 变量 2 刀具 3 参数 4 寄存器 5 采样数据]
        VAR_DATA_START,	//保存或加载数据起始变量号
        VAR_DATA_NUM,	//保存或加载数据的变量个数

        VAR_INTP_POS = 610,	//610~627: 插补器的当前输出位置：机床坐标
        VAR_INTP_ZERO = 628,	//628~645: 插补器的工件零点
        VAR_CHAN_NOTE_COPY = 646, //646~647
        VAR_CHAN_ALARM_COPY = 648, //648~649
        VAR_CHAN_NOTE_FLAG = 650, //650~651
        VAR_CHAN_ALARM_FLAG = 652, //652~653
        VAR_CHAN_G95_F = 654,
        VAR_CHAN_G43_AX,//当前刀长补的轴
        VAR_CHAN_G43_L,//当前刀长补 656~657

        VAR_CRDS_FRAME = 658,//658~675基架坐标系
        VAR_HOLD_BLK_EPOS = 676,//676~694暂停段终点
        VAR_RUN_START_POS = 694,//694~712运行起点位置

        VAR_INTP_WCS = 800,	//800~817: 插补器的当前输出位置：工件坐标
        VAR_INTP_VCS_FLAG = 818, //虚拟轴坐标系标志
        VAR_TAX_ENABLE = 850,         //倾斜轴控制使能
        VAR_TAX_ORTH_AX,            //正交轴通道轴号
        VAR_TAX_TILT_AX,            //倾斜轴通道轴号
        VAR_TAX_ANGLE,              //倾斜角度
        VAR_TAX_COMPEN,             //脉冲补偿值

        VAR_TANGENT_ABC = 870,//870~875当前插补点的角度位置【用于切线跟随】

        VAR_INTP_MODAL = 900,	//900~979 插补器的G、M模态
        VAR_LOADFILE_STATUS = 980,//动态装载磁盘文件的状态
        VAR_DCD_TYPE, //解释器机床类型
        VAR_INTP_BLKI,//插补器执行的段号
        VAR_DCD_BLKI,//解释器解释出的段号

        VAR_CHAN_TOTAL = HncDataDef.VAR_CHAN_VNUM //1000
    }

    //物理轴
    public enum AxisVar
    {//#[50000 + ax*100 + ?]
        VAR_AXIS_CHN_INF = 0,	//低16位：在通道内的逻辑轴号 高16位：数据采样标志
        VAR_AXIS_MEAN_VEL,	//轴的平均速度
        VAR_AXIS_THREAD_POS0,	//加工螺纹时轴的启动加速位置
        VAR_AXIS_THREAD_POS0_,	//
        VAR_AXIS_THREAD_POS1,	//加工螺纹时轴的同步位置
        VAR_AXIS_THREAD_POS1_,	//
        VAR_AXIS_THREAD_POS2,	//加工螺纹时轴的减速位置
        VAR_AXIS_THREAD_POS2_,	//
        VAR_AXIS_THREAD_POS3,	//加工螺纹时轴的停止位置
        VAR_AXIS_THREAD_POS3_,	//
        VAR_AXIS_MEA_CMD_POS,	//测量信号获得时的指令位置 G31
        VAR_AXIS_MEA_CMD_POS_,	//
        VAR_AXIS_MEA_ACT_POS,	//测量信号获得时的实际位置
        VAR_AXIS_MEA_ACT_POS_,	//
        VAR_AXIS_MEA_ACT_POS2,	//测量信号获得时的2号编码器位置
        VAR_AXIS_MEA_ACT_POS2_,	//
        VAR_AXIS_MEA_VEL,	//测量信号获得时的速度
        VAR_AXIS_MEA_TRQ,	//测量信号获得时的电流
        VAR_AXIS_DIST_POS1, //距离码回零第一个零点的实际位置
        VAR_AXIS_DIST_POS1_,
        VAR_AXIS_DIST_POS2, //距离码第2个零点的绝对位置
        VAR_AXIS_DIST_POS2_,
        VAR_AXIS_SYNC_ZOFF0, //同步轴零点初始偏移量
        VAR_AXIS_SYNC_ZOFF0_,
        VAR_AXIS_SPDL_NO, //轴的主轴编号
        VAR_AXIS_0_RESERV_,
        VAR_AXIS_1_RESERV,//引导轴在从轴零点时的位置
        VAR_AXIS_1_RESERV_, //
        VAR_AXIS_LEAD_DIST,//从轴检查引导距离
        VAR_AXIS_LEAD_DIST_, //29
        VAR_AXIS_EG_DIST,
        VAR_AXIS_EG_PULSE,
        VAR_AXIS_IN_PULSE1,
        VAR_AXIS_OUT_PULSE1,
        VAR_AXIS_IN_PULSE2,
        VAR_AXIS_OUT_PULSE2, //35
        VAR_AXIS_EG_DIST2,
        VAR_AXIS_EG_PULSE2,
        VAR_AXIS_2_RESERV,//从轴的标准同步偏差
        VAR_AXIS_2_RESERV_,
        VAR_AXIS_PINC_SUM,//轴的积分时间内的周期累积增量
        VAR_AXIS_PINC_SUM_,
        VAR_AXIS_HOME_CRDS, //参考点坐标
        VAR_AXIS_HOME_CRDS_, //43
        VAR_AXIS_LOCK_POS, //轴锁定时的指令位置
        VAR_AXIS_LOCK_POS_, //45
        VAR_AXIS_LOCK_PULSE, //轴锁定时的指令脉冲位置
        VAR_AXIS_LOCK_PULSE_, //47
        VAR_AXIS_NOTE_COPY, //48
        VAR_AXIS_NOTE_COPY_, //49
        VAR_AXIS_ALARM_COPY, //50
        VAR_AXIS_ALARM_COPY_, //51
        VAR_AXIS_NOTE_FLAG, //52
        VAR_AXIS_NOTE_FLAG_, //53
        VAR_AXIS_ALARM_FLAG, //54
        VAR_AXIS_ALARM_FLAG_, //55
        VAR_AXIS_ENCOFF_PULSE, //编码器偏置的脉冲量56
        VAR_AXIS_ENCOFF_PULSE_, //57
        VAR_AXIS_ENCOFF_PULSE2, //校正的编码器偏置脉冲量58
        VAR_AXIS_ENCOFF_PULSE2_, //59

        VAR_AXIS_SPDL_MIN, //螺纹加工时的主轴最小周期增量 60
        VAR_AXIS_SPDL_MAX, //螺纹加工时的主轴最大周期增量 61
        VAR_AXIS_SPDL_MEAN, //螺纹加工时的主轴平均周期增量 62
        VAR_AXIS_THREAD_ACCL,//螺纹加工时的长轴加速距离 63
        VAR_AXIS_THREAD_NACC,//螺纹加工时的加速周期数 64
        VAR_AXIS_THREAD_LVEL,//螺纹加工时的长轴周期增量 65
        VAR_AXIS_POWEROFF_POS,	//轴关机前的位置
        VAR_AXIS_POWEROFF_POS_,	//轴关机前的位置
        VAR_AXIS_MPG_SPOS,	//手摇开始位置
        VAR_AXIS_MPG_SPOS_,

        VAR_AXIS_MPG_CNT0 = 70,
        VAR_AXIS_MPG_CNTN = 79,

        VAR_AXIS_TOTAL_EXT_CMD, //外部叠加指令总量
        VAR_AXIS_TOTAL_EXT_CMD_,
        VAR_AXIS_LAST_EXT_CMD,//已经输出的外部叠加指令
        VAR_AXIS_LAST_EXT_CMD_,

        //64位脉冲计数，目前仅用于长行程直线轴配绝对式编码器
        VAR_AXIS_ABSCYC_CNT = 90,
        VAR_AXIS_ABSCYC_PUL,
        VAR_AXIS_ABSCYC_ROTPUL, //旋转轴反馈脉冲循环计数

        VAR_AXIS_TOTAL = HncDataDef.VAR_AXIS_VNUM //100
    }

    //5 Device部分
    public enum DEV_MODE_STATE
    {
        DEV_STATE_IDLE,		//空闲
        DEV_STATE_PROBE,	//检测编址[侦测拓扑结构]
        DEV_STATE_CONFIG,	//配置模式
        DEV_STATE_IDENTIFY,	//辨识
        DEV_STATE_UPDATE,	//更新参数[从控制对象中读数据]
        DEV_STATE_CHECK,    //对比检查参数
        DEV_STATE_READY,	//就绪
        DEV_STATE_RESET,	//复位
        DEV_STATE_TEST,		//测试
        DEV_STATE_RCONF,	//重构
        DEV_STATE_RUN,		//运行
        DEV_STATE_FAULT,	//故障
        DEV_STATE_EXIT		//退出
    }

    //数控主控制板类型定义
    public enum DEV_NCBRD_TYPE
    {
        DEV_NCBRD_NULL, //0
        DEV_NCBRD_5301,
        DEV_NCBRD_5311A,
        DEV_NCBRD_5311B,
        DEV_NCBRD_5311C,
        DEV_NCBRD_NCUC1, //5
        //此处扩展新的类型
        DEV_BRD_TYPE_NUM
    }

    //用于LINUX 用户态的命令调用
    public enum USER_CALL_ID
    {
        USER_NULL_CMD,
        USER_SYS_INIT,	//1
        USER_SYS_EXIT,	//2
        USER_SEL_PROG,	//3
        USER_SKIP_ROW,	//4
        USER_RUN_ROW,	//5
        USER_VERIFY,	//6
        USER_PROG_STOP,
        USER_PROG_RERUN,
        USER_CHAN_RESET,
        USER_SYS_RESET,
        USER_MDI_OPEN,
        USER_MDI_DECODE,
        USER_MDI_GOTO_BP,
        USER_MDI_CLOSE,
        USER_BUS_INIT = 20,	//20 以下用于总线调试
        USER_BUS_RESET,	//21
        USER_BUS_PROBE,	//22
        USER_BUS_SORT,	//23
        USER_BUS_READ,	//24 读从站信息
        USER_BUS_MAP,	//25 BUS-NC数据地址映射
        USER_BUS_DISCONNECT,	//25
        USER_BUS_RUN,	//26
        USER_BUS_START,	//20
        USER_PLC_INIT	// PLC初始化
    }

    // G代码模态组定义
    public enum GModel
    {
        MODAL_G04_92 = 0,
        MODAL_G00_03,	// 1
        MODAL_G17_19,	// 2
        MODAL_G24_25,	// 3
        MODAL_G50_51,	// 4
        MODAL_G68_69,	// 5
        MODAL_G71_82,	// 6
        MODAL_G11_12,	// 7
        MODAL_G20_22,	// 8
        MODAL_G41_42,	// 9
        MODAL_G43_44,	// 10
        MODAL_G54_59,	// 11
        MODAL_G61_64,	// 12
        MODAL_G90_91,	// 13
        MODAL_G94_95,	// 14
        MODAL_G98_99,	// 15
        MODAL_G15_16,	// 16 极坐标编程
        MODAL_G36_37,	// 17 
        MODAL_G12_13,	// 18 极坐标插补
        MODAL_G96_97,	// 19
        MODAL_G38_39,	// 20 同步标志开关
        MODAL_G38_39_,	// 20 同步标志开关2
        MODAL_G66_67,	// 22 模态调用 ?
        MODAL_G125_126, // 优劣弧选择
        MODAL_G135_136, // 24 倾斜轴虚轴坐标系编程
        MODAL_G140_143,	// 25 定向插补类型

        FLAG_G15_16 = 70,	// 极坐标编程标识字
        FLAG_G71_82,		// 固定循环切换标识字
        FLAG_H_IDX,			// 长度补偿寄存器编号
        FLAG_D_IDX,			// 半径补偿寄存器编号
        FLAG_T_OFF,			// 模态刀偏号
        FLAG_INDEX_AX_MASK, //记录轴状态

        MAX_GGRP_NUM = 80	// 模态指令组数，Simens是近60组
    }

    // 系统数据类型
    // Get(Bit32)表示Get数据时void *为Bit32 *
    // Set(NULL)表示参数为NULL的控制命令
    public enum HncSystem
    {
        HNC_SYS_CHAN_NUM = 0,		// 获取系统通道数 {Get(Bit32)}
        HNC_SYS_MOVE_UNIT,			// 长度分辨率 {Get(Bit32)}
        HNC_SYS_TURN_UNIT,			// 角度分辨率 {Get(Bit32)}
        HNC_SYS_METRIC_DISP,		// 公英制 {Get(Bit32)}
        HNC_SYS_SHOW_TIME,			// 显示时间 {Get(Bit32)}
        HNC_SYS_POP_ALARM,			// 报警自动显示 {Get(Bit32)}
        HNC_SYS_GRAPH_ERASE,		// 图形自动擦除 {Get(Bit32)}
        HNC_SYS_MAC_TYPE,			// 机床类型
        HNC_SYS_PREC,				// 坐标系精度 {Get(Bit32)}
        HNC_SYS_F_PREC,				// F精度 {Get(Bit32)}
        HNC_SYS_S_PREC,				// S精度 {Get(Bit32)}
        HNC_SYS_NCK_VER,			// NCK版本 {Get(Bit8[32])}
        HNC_SYS_DRV_VER,			// DRV版本 {Get(Bit8[32])}
        HNC_SYS_PLC_VER,			// PLC版本 {Get(Bit8[32])} 13
        HNC_SYS_CNC_VER,			// CNC版本 {Get(Bit32) Set(Bit32)}
        HNC_SYS_MCP_KEY,			// MCP面板钥匙开关 {Get(Bit32)}
        HNC_SYS_ACTIVE_CHAN,		// 活动通道 {Get(Bit32) Set(Bit32)}
        HNC_SYS_REQUEST_CHAN,		// 请求通道 {Get(Bit32)}
        HNC_SYS_MDI_CHAN,			// MDI运行通道 {Get(Bit32)}
        HNC_SYS_REQUEST_CHAN_MASK,	// 请求的通道屏蔽字 {Get(Bit32)}
        HNC_SYS_CHAN_MASK,			// 通道屏蔽字 {Set(Bit32)}
        HNC_SYS_PLC_STOP,			// plc停止 {Set(NULL)}
        HNC_SYS_POWEROFF_ACT,		// 断电应答 {Set(NULL)}
        HNC_SYS_IS_HOLD_REDECODE,	// 进给保持后是否重新解释 {Get(Bit32)}
        HNC_SYS_NC_VER,             // NC版本 {Get(Bit8[32])} 24
        HNC_SYS_SN_NUM,             // CF卡SN号 {Get(Bit8[32]) Set(Bit8[32])}
        HNC_SYS_MACHINE_TYPE,		//机床型号 {Get(Bit8[48]) Set(Bit8[48])}
        HNC_SYS_MACHINE_INFO,		//机床信息 {Get(Bit8[48]) Set(Bit8[48])}
        HNC_SYS_MACFAC_INFO,		//机床厂信息 {Get(Bit8[48]) Set(Bit8[48])}
        HNC_SYS_USER_INFO,			//用户信息 {Get(Bit8[48]) Set(Bit8[48])}
        HNC_SYS_MACHINE_NUM,		//机床编号 {Get(Bit8[48]) Set(Bit8[48])}
        HNC_SYS_EXFACTORY_DATE,     //出厂时间 {Get(Bit8[32])}
        HNC_SYS_ACCESS_LEVEL,
        HNC_SYS_TOTAL
    }

    // 刀具参数索引
    // INFTOOL_开头的为Bit32，其它为fBit64
    public enum ToolParaIndex
    {
        // 刀具几何相关参数索引
        GTOOL_DIR = 0,	// 方向 
        GTOOL_LEN1,	// 长度1(铣：刀具长度；车：X偏置)
        GTOOL_LEN2,	// 长度2(车：Y偏置)
        GTOOL_LEN3,	// 长度3(车：Z偏置)
        GTOOL_LEN4,	// 长度4
        GTOOL_LEN5,	// 长度5
        GTOOL_RAD1,	// 半径1(铣：刀具半径；车：刀尖半径)
        GTOOL_RAD2,	// 半径2
        GTOOL_ANG1,	// 角度1
        GTOOL_ANG2,	// 角度2

        GTOOL_TOTAL,

        // 刀具磨损相关参数索引
        WTOOL_LEN1 = ToolNumDef.MAX_GEO_PARA, // (铣：长度磨损；车：Z磨损)
        WTOOL_LEN2,	// 长度2
        WTOOL_LEN3,	// 长度3
        WTOOL_LEN4,	// 长度4
        WTOOL_LEN5,	// 长度5
        WTOOL_RAD1,	// 半径1(铣：半径磨损；车：X磨损)
        WTOOL_RAD2,	// 半径2
        WTOOL_ANG1,	// 角度1
        WTOOL_ANG2,	// 角度2

        WTOOL_TOTAL,

        // 刀具工艺相关参数索引
        TETOOL_PARA0 = ToolNumDef.MAX_GEO_PARA + ToolNumDef.MAX_WEAR_PARA, // 工艺相关参数0～参数MAX_TECH_PARA_NUM-1
        TETOOL_PARA1,
        TETOOL_PARA2,
        TETOOL_PARA3,
        TETOOL_PARA4,
        TETOOL_PARA5,
        TETOOL_PARA6,
        TETOOL_PARA7,
        TETOOL_PARA8,
        TETOOL_PARA9,
        // 暂用10个，今后再加

        TETOOL_TOTAL,

        // 刀具扩展参数--刀具管理参数，各刀具类型通用
        EXTOOL_S_LIMIT = ToolNumDef.MAX_GEO_PARA + ToolNumDef.MAX_WEAR_PARA + ToolNumDef.MAX_TECH_PARA,    // S转速限制
        EXTOOL_F_LIMIT,    // F转速限制
        EXTOOL_LARGE_LEFT,		// 大刀具干涉左刀位
        EXTOOL_LARGE_RIGHT,		// 大刀具干涩右刀位

        EXTOOL_TOTAL,

        // 刀具监控参数
        MOTOOL_TYPE = ToolNumDef.MAX_GEO_PARA + ToolNumDef.MAX_WEAR_PARA + ToolNumDef.MAX_TECH_PARA + ToolNumDef.MAX_TOOL_EXPARA, // 刀具监控类型，按位有效，寿命/计件/磨损，可选多种监控方式同时监控
        MOTOOL_SEQU,		// 	优先级
        MOTOOL_MULTI,		// 	倍率

        MOTOOL_MAX_LIFE,	// 最大寿命
        MOTOOL_ALM_LIFE,	// 预警寿命
        MOTOOL_ACT_LIFE,	// 实际寿命

        MOTOOL_MAX_COUNT,	// 最大计件数
        MOTOOL_ALM_COUNT,	// 预警计件数
        MOTOOL_ACT_COUNT,	// 实际计件数

        MOTOOL_MAX_WEAR,	// 最大磨损
        MOTOOL_ALM_WEAR,	// 预警磨损
        MOTOOL_ACT_WEAR,	// 实际磨损

        MOTOOL_TOTAL,

        // 刀具测量参数个数
        METOOL_PARA0 = ToolNumDef.MAX_GEO_PARA + ToolNumDef.MAX_WEAR_PARA + ToolNumDef.MAX_TECH_PARA + 
            ToolNumDef.MAX_TOOL_EXPARA + ToolNumDef.MAX_TOOL_MONITOR,
        METOOL_PARA1,
        METOOL_PARA2,
        METOOL_PARA3,
        METOOL_PARA4,
        METOOL_PARA5,
        METOOL_PARA6,
        METOOL_PARA7,
        METOOL_PARA8,
        METOOL_PARA9,

        METOOL_TOTAL,

        // 	刀具一般信息
        INFTOOL_ID = ToolNumDef.MAX_GEO_PARA + ToolNumDef.MAX_WEAR_PARA + ToolNumDef.MAX_TECH_PARA +
            ToolNumDef.MAX_TOOL_EXPARA + ToolNumDef.MAX_TOOL_MONITOR + ToolNumDef.MAX_TOOL_BASE, // 刀具索引号
        INFTOOL_MAGZ,		// 	刀具所属刀库号
        INFTOOL_CH,			// 	刀具所属通道号
        INFTOOL_TYPE,		// 	刀具类型
        INFTOOL_STATE,		// 	刀具状态字

        INFTOOL_TOTAL,

        TOOL_PARA_TOTAL // < MAX_TOOL_PARA
    }

    // 刀库表表头数据索引
    public enum MagzHeadIndex
    {
        MAGZTAB_HEAD = 0,	// 刀库表起始偏移地址（刀具号段+刀位属性段）
        MAGZTAB_TOOL_NUM,	// 刀库表中刀具数
        MAGZTAB_CUR_TOOL,	// 当前刀具号
        MAGZTAB_CUR_POT,	// 当前刀位号
        MAGZTAB_REF_TOOL,	// 标刀号
        MAGZTAB_CHAN,		// 刀库所属通道号
        MAGZTAB_TYPE,		// 刀库类型
        SWAP_LEFT_TOOL,		// 机械手左刀位刀具号
        SWAP_RIGHT_TOOL,	// 机械手右刀位刀具号
        // 预留

        MAGZTAB_TOTAL
    }

    // 轴数据类型
    // Get(Bit32)表示Get数据时void *为Bit32 *
    public enum HncAxis
    {
        HNC_AXIS_NAME = 0,		// 轴名 {Get(Bit8[PARAM_STR_LEN])}
        HNC_AXIS_TYPE,			// 轴类型 {Get(Bit32)}
        HNC_AXIS_CHAN,			// 获取通道号 {Get(Bit32)}
        HNC_AXIS_CHAN_INDEX,	// 获取在通道中的轴号 {Get(Bit32)}
        HNC_AXIS_CHAN_SINDEX,	// 获取在通道中的主轴号 {Get(Bit32)}
        HNC_AXIS_LEAD,			// 获取引导轴 {Get(Bit32)}
        HNC_AXIS_ACT_POS,		// 机床实际位置 {Get(Bit32)}
        HNC_AXIS_ACT_POS2,		// 机床实际位置2 {Get(Bit32)}
        HNC_AXIS_CMD_POS,		// 机床指令位置 {Get(Bit32)}
        HNC_AXIS_ACT_POS_WCS,	// 工件实际位置 {Get(Bit32)}
        HNC_AXIS_CMD_POS_WCS,	// 工件指令位置 {Get(Bit32)}
        HNC_AXIS_ACT_POS_RCS,	// 相对实际位置 {Get(Bit32)}
        HNC_AXIS_CMD_POS_RCS,	// 相对指令位置 {Get(Bit32)}
        HNC_AXIS_ACT_PULSE,		// 实际脉冲位置 {Get(Bit32)}
        HNC_AXIS_CMD_PULSE,		// 指令脉冲位置 {Get(Bit32)}
        HNC_AXIS_PROG_POS,		// 编程位置 {Get(Bit32)}
        HNC_AXIS_ENC_CNTR,		// 电机位置 {Get(Bit32)}
        HNC_AXIS_CMD_VEL,		// 指令速度 {Get(Bit32)}
        HNC_AXIS_ACT_VEL,		// 实际速度 {Get(fBit64)}
        HNC_AXIS_LEFT_TOGO,		// 剩余进给 {Get(Bit32)}
        HNC_AXIS_WCS_ZERO,		// 工件零点 {Get(Bit32)}
        HNC_AXIS_WHEEl_OFF,		// 手轮中断偏移量 {Get(Bit32)}
        HNC_AXIS_FOLLOW_ERR,	// 跟踪误差 {Get(Bit32)}
        HNC_AXIS_SYN_ERR,		// 同步误差	{Get(Bit32)}
        HNC_AXIS_COMP,			// 轴补偿值 {Get(Bit32)}
        HNC_AXIS_ZSW_DIST,		// Z脉冲偏移 {Get(Bit32)}
        HNC_AXIS_REAL_ZERO,		// 相对零点 {Get(Bit32)}
        HNC_AXIS_MOTOR_REV,		// 电机转速 {Get(fBit64)}
        HNC_AXIS_DRIVE_CUR,		// 驱动单元电流 {Get(fBit64)}
        HNC_AXIS_LOAD_CUR,		// 负载电流 {Get(fBit64)}
        HNC_AXIS_RATED_CUR,		// 额定电流 {Get(fBit64)}
        HNC_AXIS_IS_HOMEF,		// 回零完成 {Get(Bit32)}
        HNC_AXIS_WAVE_FREQ,		// 波形频率 {Get(fBit64)}
        HNC_AXIS_DRIVE_VER,     // 伺服驱动版本 {Get(fBit64)}
        HNC_AXIS_MOTOR_TYPE,    // 伺服类型 {Get(Bit32)}
        HNC_AXIS_MOTOR_TYPE_FLAG,// 伺服类型出错标志 {Get(Bit32)}
        HNC_AXIS_TOTAL
    }

    // 采样类型
    public enum HncSampleType
    {
        SAMPL_AXIS_CMD = 1,	// 轴的指令位置
        SAMPL_AXIS_ACT,		// 轴的实际位置
        SAMPL_FOLLOW_ERR,	// 轴的跟随误差
        SAMPL_CMD_INC,		// 轴的指令速度
        SAMPL_ACT_VEL,		// 轴的实际速度
        SAMPL_ACT_TRQ,		// 轴的负载电流
        SAMPL_CMD_POS,		// 指令电机位置
        SAMPL_CMD_PULSE,	// 指令脉冲位置
        SAMPL_ACT_POS,		// 实际电机位置
        SAMPL_ACT_PULSE,	// 实际脉冲位置
        SAMPL_TOL_COMP,		// 补偿值
        SAMPL_SYS_VAL = 101,	// 系统变量
        SAMPL_CHAN_VAL,		// 通道变量
        SAMPL_AXIS_VAL,		// 轴变量
        SAMPL_X_REG,		// X寄存器
        SAMPL_Y_REG,		// Y寄存器
        SAMPL_F_AXIS_REG,	// 轴F寄存器
        SAMPL_G_AXIS_REG,	// 轴G寄存器
        SAMPL_F_CHAN_REG,	// 通道F寄存器
        SAMPL_G_CHAN_REG,	// 通道G寄存器
        SAMPL_F_SYS_REG,	// 系统F寄存器
        SAMPL_G_SYS_REG,	// 系统G寄存器
        SAMPL_R_REG,		// R寄存器
        SAMPL_B_REG,		// B寄存器
        SAMPL_TOTAL
    }
    //通道类型
    public enum HncChannel
    {
        HNC_CHAN_IS_EXIST = 0,		// 通道是否存在 {Get(Bit32)}
        HNC_CHAN_MAC_TYPE,			// 通道的机床类型 {Get(Bit32)}
        HNC_CHAN_AXES_MASK,			// 轴掩码 {Get(Bit32)}
        HNC_CHAN_AXES_MASK1,		// 轴掩码1 {Get(Bit32)}
        HNC_CHAN_NAME,				// 通道名 {Get(Bit8[PARAM_STR_LEN])}
        HNC_CHAN_CMD_TYPE,			// 读取当前G代码的标志 {Get(Bit32)}
        HNC_CHAN_CMD_FEEDRATE,		// 指令进给速度 {Get(fBit64)}
        HNC_CHAN_ACT_FEEDRATE,		// 实际进给速度 {Get(fBit64)}
        HNC_CHAN_PROG_FEEDRATE,		// 编程指令速度 {Get(fBit64)}
        HNC_CHAN_FEED_OVERRIDE,		// 进给修调 {Get(Bit32)}
        HNC_CHAN_RAPID_OVERRIDE,	// 快移修调 {Get(Bit32)}
        HNC_CHAN_MCODE,             // 通道的M指令 {Get(Bit32)}
        HNC_CHAN_TCODE,				// 通道的T指令 {Get(Bit32)}
        HNC_CHAN_TOFFS,				// 通道中的刀偏号 {Get(Bit32)}
        HNC_CHAN_TOOL_USE,			// 当前刀具 {Get(Bit32)}
        HNC_CHAN_TOOL_RDY,			// 准备好交换的刀具 {Get(Bit32)}
        HNC_CHAN_MODE,				// 模式(返回值数据定义见下面) {Get(Bit32)}
        HNC_CHAN_IS_MDI,			// MDI {Get(Bit32)}
        HNC_CHAN_CYCLE,				// 循环启动 {Get(Bit32), Set(NULL)}
        HNC_CHAN_HOLD,				// 进给保持 {Get(Bit32), Set(NULL)}
        HNC_CHAN_IS_PROGSEL,		// 已选程序 {Get(Bit32)}
        HNC_CHAN_IS_PROGEND,		// 程序运行完成 {Get(Bit32)}
        HNC_CHAN_IS_THREADING,		// 螺纹加工 {Get(Bit32)}
        HNC_CHAN_IS_RIGID,			// 刚性攻丝 {Get(Bit32)}
        HNC_CHAN_IS_REWINDED,		// 重运行复位状态 {Get(Bit32)}
        HNC_CHAN_IS_ESTOP,			// 急停 {Get(Bit32)}
        HNC_CHAN_IS_RESETTING,		// 复位 {Get(Bit32)}
        HNC_CHAN_IS_RUNNING,		// 运行中 {Get(Bit32)}
        HNC_CHAN_IS_HOMING,			// 回零中 {Get(Bit32)}
        HNC_CHAN_IS_MOVING,			// 轴移动中 {Get(Bit32)}
        HNC_CHAN_DIAMETER,			// 直半径编程 {Get(Bit32)}
        HNC_CHAN_VERIFY,			// 校验 {Get(Bit32), Set(Bit32)}
        HNC_CHAN_RUN_ROW,			// 运行行 {Get(Bit32)}
        HNC_CHAN_DCD_ROW,			// 译码行 {Get(Bit32)}
        HNC_CHAN_SEL_PROG,			// 选择程序的编号 {Get(Bit32)}
        HNC_CHAN_RUN_PROG,			// 运行程序的编号 {Get(Bit32)}
        HNC_CHAN_PART_CNTR,			// 加工计数 {Get(Bit32), Set(Bit32)}
        HNC_CHAN_PART_STATI,		// 工件总数 {Get(Bit32), Set(Bit32)}
        HNC_CHAN_HMI_RESET,			// HMI复位 {Set(NULL)}
        HNC_CHAN_CHG_PROG,			// 程序修改标志 {Set(NULL)} 39

        HNC_CHAN_PERIOD_TOTAL,		// 【周期数据结束】，以下数据不作为周期数据上传

        HNC_CHAN_LAX,				// 通道轴对应的逻辑轴号 {Get(Bit32)}
        HNC_CHAN_AXIS_NAME,			// 编程轴名 {Get(Bit8[PARAM_STR_LEN])}
        HNC_CHAN_SPDL_NAME,			// 编程主轴名 {Get(Bit8[PARAM_STR_LEN])}
        HNC_CHAN_MODAL,				// 通道模态 共80组 {Get(Bit32)}
        HNC_CHAN_SPDL_LAX,			// 通道主轴对应的逻辑轴号，动态 {Get(Bit32)}
        HNC_CHAN_SPDL_PARA_LAX,		// 通道主轴对应的逻辑轴号，静态 {Get(Bit32)}
        HNC_CHAN_CMD_SPDL_SPEED,	// 主轴指令速度 {Get(fBit64)}  47
        HNC_CHAN_ACT_SPDL_SPEED,	// 主轴实际速度 {Get(fBit64)}   48
        HNC_CHAN_SPDL_OVERRIDE,		// 主轴修调 {Get(Bit32)}
        HNC_CHAN_DO_HOLD,			// 设置进给保持 
        HNC_CHAN_BP_POS,			// 断点位置 {Get(Bit32)}
        HNC_CHAN_TOTAL
    }
    //改成了宏定义
    public enum HncChanMode
    {
        CHAN_MODE_RESET = 0,		//	复位
        CHAN_MODE_AUTO,				//	自动
        CHAN_MODE_JOG,				//	手动
        CHAN_MODE_STEP,				//	增量
        CHAN_MODE_MPG,				//	手摇
        CHAN_MODE_HOME,				//	回零
        CHAN_MODE_PMC,				//	PMC
        CHAN_MODE_SBL				//	手动
    }
    //变量类型
    public enum HncVarType
    {
        VAR_TYPE_AXIS = 0,	// 轴变量 {Get(Bit32), Set(Bit32)}
        VAR_TYPE_CHANNEL,	// 通道变量 {Get(Bit32), Set(Bit32)}
        VAR_TYPE_SYSTEM,	// 系统变量 {Get(Bit32), Set(Bit32)}
        VAR_TYPE_SYSTEM_F,	// 浮点类型的系统变量 {Get(fBit64), Set(fBit64)}
        VAR_TYPE_TOTAL
    }
    //	报警错误类型
    public enum AlarmType
    {
        ALARM_SY = 0,	//	系统报警（System）
        ALARM_CH,		//	通道报警（Channel）
        ALARM_AX,		//	轴报警（Axis）
        ALARM_SV,		//	伺服报警（Servo）
        ALARM_PC,		//	PLC报警（PLC）
        ALARM_DV,		//	设备报警（Dev）
        ALARM_PS,		//	语法报警（Program Syntax）
        ALARM_UP,		//	用户PLC报警（User PLC）
        ALARM_HM,		//	HMI报警（HMI）
        ALARM_TYPE_ALL
    }
    //	报警级别
    public enum AlarmLevel
    {
        ALARM_ERR = 0,	//	错误（Error）
        ALARM_MSG,		//	提示（Message）	
        ALARM_LEVEL_ALL
    }
    // 数控设备控制对象应用类型定义，系统根据其类型和接口参数确定控制对象在系统中
    // 的输入输出数据地址
    public enum DevNcobjType
    {
        DEV_NCOBJ_NULL_LOC = 1000,	// 本地设备--非网络设备
        DEV_NCOBJ_SPDL_LOC,
        DEV_NCOBJ_AXIS_LOC,
        DEV_NCOBJ_IN_LOC,
        DEV_NCOBJ_OUT_LOC,
        DEV_NCOBJ_AD_LOC,
        DEV_NCOBJ_DA_LOC,
        DEV_NCOBJ_IOMD_LOC,		// NCUC总线的IO集成模块
        DEV_NCOBJ_MCP_LOC,
        DEV_NCOBJ_MPG_LOC,
        DEV_NCOBJ_NCKB_LOC,
        DEV_NCOBJ_SENSOR_LOC,	// 传感器设备
        DEV_NCOBJ_SERIAL_LOC,	// 串口设备
        DEV_NCOBJ_GATHER_LOC,	// 温度采集卡

        DEV_NCOBJ_NULL_NET = 2000,	// 网络设备--ncuc\ethercat\syqnet...
        DEV_NCOBJ_SPDL_NET,
        DEV_NCOBJ_AXIS_NET,
        DEV_NCOBJ_IN_NET,
        DEV_NCOBJ_OUT_NET,
        DEV_NCOBJ_AD_NET,
        DEV_NCOBJ_DA_NET,
        DEV_NCOBJ_IOMD_NET,		// NCUC总线的IO集成模块
        DEV_NCOBJ_MCP_NET,
        DEV_NCOBJ_MPG_NET,
        DEV_NCOBJ_NCKB_NET,
        DEV_NCOBJ_SENSOR_NET,	// 传感器
        DEV_NCOBJ_PIDC_NET,		// 位控板

        // 此处扩展新的类型
        DEV_NCOBJ_ENCOD_NET		// 编码器
    }

    public enum AccessLevel
    {
        ACCESS_FREE = 0,
        ACCESS_USER,
        ACCESS_MAC,
        ACCESS_NC,
        ACCESS_RD,
        ACCESS_VENDER,
    }

    public enum GModalDef
    {
	    MODAL_G04_92 = 0,	
	    MODAL_G00_03,	// 1
	    MODAL_G17_19,	// 2
	    MODAL_G24_25,	// 3
	    MODAL_G50_51,	// 4
	    MODAL_G68_69,	// 5
	    MODAL_G71_82,	// 6
	    MODAL_G11_12,	// 7
	    MODAL_G20_22,	// 8
	    MODAL_G41_42,	// 9
	    MODAL_G43_44,	// 10
	    MODAL_G54_59,	// 11
	    MODAL_G61_64,	// 12
	    MODAL_G90_91,	// 13
	    MODAL_G94_95,	// 14
	    MODAL_G98_99,	// 15
	    MODAL_G15_16,	// 16 极坐标编程
	    MODAL_G36_37,	// 17 
	    MODAL_G12_13,	// 18 极坐标插补
	    MODAL_G96_97,	// 19
	    MODAL_G38_39,	// 20 同步标志开关
	    MODAL_G38_39_,	// 20 同步标志开关2
	    MODAL_G66_67,	// 22 模态调用 ?
	    MODAL_G125_126, // 优劣弧选择
	    MODAL_G135_136, // 24 倾斜轴虚轴坐标系编程
	    MODAL_G140_143,	// 25 定向插补类型
	
	    FLAG_G15_16 = 70,	// 极坐标编程标识字
	    FLAG_G71_82,		// 固定循环切换标识字
	    FLAG_H_IDX,			// 长度补偿寄存器编号
	    FLAG_D_IDX,			// 半径补偿寄存器编号
	    FLAG_T_OFF,			// 模态刀偏号
	    FLAG_INDEX_AX_MASK, //记录轴状态
	
	    MAX_GGRP_NUM = 80	// 模态指令组数，Simens是近60组
    }

    [StructLayout(LayoutKind.Explicit, Size = 8, CharSet = CharSet.Ansi, Pack = 4)]
    public struct SHncData
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.I4)]
        public Int32 i;
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.R8)]
        public Double f;
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 n;	// 变量偏移地址
    }
    // 系统用全局变量、表达式运算的联合数据类型
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct SDataUnion
    {
        public Byte type;
        public Byte g90;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 1)]
        public SHncData v;
    }

    // 参数值
    [StructLayout(LayoutKind.Explicit, Size = 8, CharSet = CharSet.Ansi, Pack = 4)]
    public struct SParamValue
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public SByte[] s;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct nctime_t
    {
        public Int32 second;	// seconds - [0,59]
        public Int32 minute;	// minutes - [0,59]
        public Int32 hour;	// hours   - [0,23]
        public Int32 hsecond; /* hundredths of seconds */
        public Int32 day;	// [1,31]
        public Int32 month;	// [0,11] (January = 0)
        public Int32 year;	// (current year minus 1900)
        public Int32 wday;	// Day of week, [0,6] (Sunday = 0)
    }
    // 报警历史记录数据
    [StructLayout(LayoutKind.Sequential, Size = 132, CharSet = CharSet.Ansi, Pack = 4)]
    public struct AlarmHisData
    {
        [MarshalAs(UnmanagedType.I4)]
        public Int32 alarmNo;
        [MarshalAs(UnmanagedType.Struct)]
        public nctime_t timeBegin;
        [MarshalAs(UnmanagedType.Struct)]
        public nctime_t timeEnd;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public String text;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public class SEventElement
    {
        [MarshalAs(UnmanagedType.I2)]
        public Int16 src;// 事件来源
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 code;// 事件代码
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public SByte[] buf;
    }
    // 文件结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct nc_finfo_t
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public SByte[] reserved;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 attrib;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 wr_time;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 wr_date;
        [MarshalAs(UnmanagedType.I4)]
        public Int32 size;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
        public String name;
    }
    // 文件查找结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct ncfind_t
    {
        [MarshalAs(UnmanagedType.Struct, SizeConst = 1)]
        public nc_finfo_t info;
        [MarshalAs(UnmanagedType.I4)]
        public Int32 handle;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public String time;
    }

    public class HncApi
    {
        private const Int32 byteSize = 1;
        private const Int32 shortSize = 2;
        private const Int32 intSize = 4;
        private const Int32 doubleSize = 8;
        private const Int32 ipSize = 100;

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetInit", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int16 HNC_NetInit(String ip, UInt16 port);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetExit", CallingConvention = CallingConvention.Cdecl)]
        public static extern void HNC_NetExit();

       // [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetThreadStartup", CallingConvention = CallingConvention.Cdecl)]
       // public static extern Int16 HNC_NetThreadStartup();

       // [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetThreadShutdown", CallingConvention = CallingConvention.Cdecl)]
       // public static extern Int16 HNC_NetThreadShutdown();

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetIsThreadStartup", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int16 HNC_NetIsThreadStartup();

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetSetIpaddr", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_NetSetIpaddr(String ip, UInt16 port, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetGetIpaddr", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_NetGetIpaddr(IntPtr ip, ref UInt16 port, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetAddIpaddr", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_NetAddIpaddr(String ip, UInt16 port, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetDelIpaddr", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_NetDelIpaddr(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetGetClientNo", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_NetGetClientNo(String ip, UInt16 port, ref Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetFileSend", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_NetFileSend(String localNamme, String dstName, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetFileGet", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_NetFileGet(String localNamme, String dstName, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetFileGetDirInfo", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_NetFileGetDirInfo(String dirname, IntPtr info, ref UInt16 num, Int16 clientNo);
        
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetConnect", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int16 HNC_NetConnect(String ip, UInt16 port);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_NetIsConnect", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 HNC_NetIsConnect(Int16 clientNo);

		[DllImport("HncNetDll.dll", EntryPoint = "HNC_NetFileCheck", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 HNC_NetFileCheck(String localNamme, String dstName, Int16 clientNo);

		[DllImport("HncNetDll.dll", EntryPoint = "HNC_NetFileRemove", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 HNC_NetFileRemove(String dstName, Int16 clientNo);

        //hncmsg api
        //寄存器
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_RegGetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_RegGetValue(Int32 type, Int32 index, IntPtr value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_RegSetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_RegSetValue(Int32 type, Int32 index, IntPtr value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_RegSetBit", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_RegSetBit(Int32 type, Int32 index, Int32 bit, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_RegClrBit", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_RegClrBit(Int32 type, Int32 index, Int32 bit, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_RegGetNum", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_RegGetNum(Int32 type, ref Int32 num, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_RegGetFGBase", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_RegGetFGBase(Int32 baseType, ref Int32 value, Int16 clientNo);

        //变量
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_VarGetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_VarGetValue(Int32 type, Int32 no, Int32 index, IntPtr value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_VarSetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_VarSetValue(Int32 type, Int32 no, Int32 index, IntPtr value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_VarSetBit", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_VarSetBit(Int32 type, Int32 no, Int32 index, Int32 bit, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_VarClrBit", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_VarClrBit(Int32 type, Int32 no, Int32 index, Int32 bit, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_MacroVarGetValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_MacroVarGetValue(Int32 no, ref SDataUnion var, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_MacroVarSetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_MacroVarSetValue(Int32 no, IntPtr index, Int16 clientNo);

        //参数
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanLoad", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanLoad(String lpFileName, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanSave", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanSave(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanSaveAs", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanSaveAs(String lpFileName, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanGetParaPropEx", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ParamanGetParaPropEx(Int32 parmId, Byte propType, IntPtr propValue, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanSetParaPropEx", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ParamanSetParaPropEx(Int32 parmId, Byte propType, IntPtr propValue, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanGetParaProp", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ParamanGetParaProp(Int32 filetype, Int32 subid, Int32 index, Byte prop_type, IntPtr prop_value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanSetParaProp", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ParamanSetParaProp(Int32 filetype, Int32 subid, Int32 index, Byte prop_type, IntPtr prop_value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanGetFileName", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ParamanGetFileName(Int32 fileNo, IntPtr buf, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanGetSubClassProp", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ParamanGetSubClassProp(Int32 fileNo, Byte propType, IntPtr propValue, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanGetTotalRowNum", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanGetTotalRowNum(ref Int32 rowNum, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanTransRow2Index", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanTransRow2Index(Int32 fileNo, Int32 subNo, Int32 rowNo, ref Int32 index, ref Int16 dupNum, ref Int16 dupNo, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanTransRowx2Row", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanTransRowx2Row(Int32 rowx, ref Int32 fileNo, ref Int32 subNo, ref Int32 row, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanTransId2Rowx", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanTransId2Rowx(Int32 parmId, ref Int32 rowx, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanRewriteSubClass", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanRewriteSubClass(Int32 fileNo, Int32 subNo, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanSaveStrFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanSaveStrFile(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanGetI32", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanGetI32(Int32 fileNo, Int32 subNo, Int32 index, ref Int32 value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanSetI32", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanSetI32(Int32 fileNo, Int32 subNo, Int32 index, Int32 value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanGetFloat", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanGetFloat(Int32 fileNo, Int32 subNo, Int32 index, ref Double value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanSetFloat", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanSetFloat(Int32 fileNo, Int32 subNo, Int32 index, Double value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanGetStr", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ParamanGetStr(Int32 fileNo, Int32 subNo, Int32 index, IntPtr value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanSetStr", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ParamanSetStr(Int32 fileNo, Int32 subNo, Int32 index, String value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanGetItem", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ParamanGetItem(Int32 fileNo, Int32 subNo, Int32 index, IntPtr value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ParamanSetItem", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ParamanSetItem(Int32 fileNo, Int32 subNo, Int32 index, IntPtr value, Int16 clientNo);

        //系统
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SystemGetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_SystemGetValue(Int32 type, IntPtr value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SystemSetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_SystemSetValue(Int32 type, IntPtr value, Int16 clientNo);

        //通道、轴
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ChannelGetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ChannelGetValue(Int32 type, Int32 ch, Int32 index, IntPtr value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ChannelSetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ChannelSetValue(Int32 type, Int32 ch, Int32 index, IntPtr value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_AxisGetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_AxisGetValue(Int32 type, Int32 ax, IntPtr value, Int16 clientNo);

        //坐标系
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_CrdsGetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_CrdsGetValue(Int32 type, Int32 ax, IntPtr value, Int32 ch, Int32 crds, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_CrdsSetValue", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_CrdsSetValue(Int32 type, Int32 ax, IntPtr value, Int32 ch, Int32 crds, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_CrdsGetMaxNum", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_CrdsGetMaxNum(Int32 type, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_CrdsLoad", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_CrdsLoad(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_CrdsSave", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_CrdsSave(Int16 clientNo);

        //刀具、刀库
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolMagSave", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolMagSave(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolGetMaxMagNum", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolGetMaxMagNum(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolGetMagHeadBase", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolGetMagHeadBase(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolGetPotDataBase", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolGetPotDataBase(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolGetMagBase", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolGetMagBase(Int32 magNo, Int32 index, ref Int32 value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolSetMagBase", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolSetMagBase(Int32 magNo, Int32 index, Int32 value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolMagGetToolNo", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolMagGetToolNo(Int32 magNo, Int32 potNo, ref Int32 toolNo, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolMagSetToolNo", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolMagSetToolNo(Int32 magNo, Int32 potNo, Int32 toolNo, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolGetPotAttri", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolGetPotAttri(Int32 magNo, Int32 potNo, ref Int32 potAttri, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolSetPotAttri", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolSetPotAttri(Int32 magNo, Int32 potNo, Int32 potAttri, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolLoad", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolLoad(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolSave", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolSave(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolGetMaxToolNum", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_ToolGetMaxToolNum(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolGetToolPara", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ToolGetToolPara(Int32 toolNo, Int32 index, IntPtr value, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_ToolSetToolPara", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_ToolSetToolPara(Int32 toolNo, Int32 index, IntPtr value, Int16 clientNo);

        //采样
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplGetPeriod", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplGetPeriod(Int32 ch, ref Int32 tick, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplSetPeriod", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplSetPeriod(Int32 ch, Int32 tick, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplGetLmt", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplGetLmt(Int32 ch, ref Int32 type, ref Int32 n, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplSetLmt", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplSetLmt(Int32 ch, Int32 type, Int32 n, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplGetChannel", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplGetChannel(ref Int32 chnNum, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplSetChannel", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplSetChannel(Int32 chnNum, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplGetPropertyType", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplGetPropertyType(Int32 ch, ref Int16 type, ref Int16 axisNo, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplSetPropertyType", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplSetPropertyType(Int32 ch, Int32 type, Int32 axisNo, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplGetRegType", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplGetRegType(Int32 ch, ref Int32 type, ref Int32 offset, ref Int32 dataLen, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplSetRegType", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplSetRegType(Int32 ch, Int32 type, Int32 offset, Int32 dataLen, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplReset", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplReset(Int32 ch, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplTriggerOn", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplTriggerOn(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplTriggerOff", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplTriggerOff(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplGetNum", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplGetNum(Int32 ch, ref Int32 num, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplSetNum", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplSetNum(Int32 ch, Int32 num, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplGetData", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_SamplGetData(Int32 ch, ref Int32 num, IntPtr data, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SamplGetStat", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SamplGetStat(Int16 clientNo);

        //告警
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_AlarmGetNum", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_AlarmGetNum(Int32 type, Int32 level, ref Int32 num, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_AlarmGetData", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_AlarmGetData(Int32 type, Int32 level, Int32 index, ref Int32 alarmNo, IntPtr alarmText, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_AlarmGetHistoryNum", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_AlarmGetHistoryNum(ref Int32 num, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_AlarmGetHistoryData", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_AlarmGetHistoryData(Int32 index, ref Int32 count, IntPtr data, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_AlarmRefresh", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_AlarmRefresh(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_AlarmSaveHistory", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_AlarmSaveHistory(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_AlarmClrHistory", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_AlarmClrHistory(Int16 clientNo);

        //G代码
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_FprogGetFullName", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_FprogGetFullName(Int32 ch, IntPtr progName, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_FprogRandomInit", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_FprogRandomInit(Int32 ch, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_FprogRandomLoad", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_FprogRandomLoad(Int32 line, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_FprogRandomWriteback", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_FprogRandomWriteback(Int32 line, Int16 clientNo);

		[DllImport("HncNetDll.dll", EntryPoint = "HNC_SysCtrlSkipToRow", CallingConvention = CallingConvention.Cdecl)]
    	public static extern Int32 HNC_SysCtrlSkipToRow(Int32 ch, Int32 row, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_FprogRandomExit", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_FprogRandomExit(Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SysCtrlSelectProg", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SysCtrlSelectProg(Int32 ch, String name, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SysCtrlLoadProg", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SysCtrlLoadProg(Int32 ch, String name, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SysCtrlResetProg", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SysCtrlResetProg(Int32 ch, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SysCtrlStopProg", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SysCtrlStopProg(Int32 ch, Int16 clientNo);

        //升级、备份
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SysBackup", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SysBackup(Int32 flag, String PathName, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_SysUpdate", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_SysUpdate(Int32 flag, String PathName, Int16 clientNo);

        //事件
        [DllImport("HncNetDll.dll", EntryPoint = "HNC_EventPut", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 HNC_EventPut(SEventElement ev, Int16 clientNo);

        [DllImport("HncNetDll.dll", EntryPoint = "HNC_EventGetSysEv", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 HNC_EventGetSysEv(IntPtr ev);

		[DllImport("HncNetDll.dll", EntryPoint = "HNC_NetDiskMount", CallingConvention = CallingConvention.Cdecl)]
		public static extern Int32 HNC_NetDiskMount(String ip, String progAddr, String name, String pass, Int16 clientNo);


        //重载函数
        public static Int32 HNC_RegGetValue(Int32 type, Int32 index, ref Byte value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(byteSize);
            ret = HNC_RegGetValue(type, index, ptr, clientNo);
            value = Marshal.ReadByte(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_RegGetValue(Int32 type, Int32 index, ref Int16 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(shortSize);
            ret = HNC_RegGetValue(type, index, ptr, clientNo);
            value = Marshal.ReadInt16(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_RegGetValue(Int32 type, Int32 index, ref Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            ret = HNC_RegGetValue(type, index, ptr, clientNo);
            value = Marshal.ReadInt32(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_RegSetValue(Int32 type, Int32 index, Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            Marshal.WriteInt32(ptr, value);
            ret = HNC_RegSetValue(type, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_RegSetValue(Int32 type, Int32 index, Int16 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(shortSize);
            Marshal.WriteInt16(ptr, value);
            ret = HNC_RegSetValue(type, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_RegSetValue(Int32 type, Int32 index, Byte value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(byteSize);
            Marshal.WriteByte(ptr, value);
            ret = HNC_RegSetValue(type, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ChannelGetValue(Int32 type, Int32 ch, Int32 index, ref String value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(doubleSize);
            ret = HNC_ChannelGetValue(type, ch, index, ptr, clientNo);
            value = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ChannelGetValue(Int32 type, Int32 ch, Int32 index, ref Double value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(doubleSize);
            ret = HNC_ChannelGetValue(type, ch, index, ptr, clientNo);
            value = (Double)Marshal.PtrToStructure(ptr, typeof(Double));
            Marshal.FreeHGlobal(ptr);
            return ret;
        }

        public static Int32 HNC_ChannelGetValue(Int32 type, Int32 ch, Int32 index, ref Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            ret = HNC_ChannelGetValue(type, ch, index, ptr, clientNo);
            value = Marshal.ReadInt32(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ChannelSetValue(Int32 type, Int32 ch, Int32 index, Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            Marshal.WriteInt32(ptr, value);
            ret = HNC_ChannelSetValue(type, ch, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_AxisGetValue(Int32 type, Int32 ax, ref String value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.AXIS_DATA_LEN);
            ret = HNC_AxisGetValue(type, ax, ptr, clientNo);
            value = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_AxisGetValue(Int32 type, Int32 ax, ref Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            ret = HNC_AxisGetValue(type, ax, ptr, clientNo);
            value = Marshal.ReadInt32(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_AxisGetValue(Int32 type, Int32 ax, ref Double value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(doubleSize);
            ret = HNC_AxisGetValue(type, ax, ptr, clientNo);
            value = (Double)Marshal.PtrToStructure(ptr, typeof(Double));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_CrdsGetValue(Int32 type, Int32 ax, ref Double value, Int32 ch, Int32 crds, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(doubleSize);
            ret = HNC_CrdsGetValue(type, ax, ptr, ch, crds, clientNo);
            value = (Double)Marshal.PtrToStructure(ptr, typeof(Double));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_CrdsGetValue(Int32 type, Int32 ax, ref Int32 value, Int32 ch, Int32 crds, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            ret = HNC_CrdsGetValue(type, ax, ptr, ch, crds, clientNo);
            value = Marshal.ReadInt32(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_CrdsSetValue(Int32 type, Int32 ax, Double value, Int32 ch, Int32 crds, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(doubleSize);
            Marshal.StructureToPtr(value, ptr, true);
            ret = HNC_CrdsSetValue(type, ax, ptr, ch, crds, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_CrdsSetValue(Int32 type, Int32 ax, Int32 value, Int32 ch, Int32 crds, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            Marshal.WriteInt32(ptr, value);
            ret = HNC_CrdsSetValue(type, ax, ptr, ch, crds, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ToolGetToolPara(Int32 toolNo, Int32 index, ref Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            ret = HNC_ToolGetToolPara(toolNo, index, ptr, clientNo);
            value = Marshal.ReadInt32(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ToolGetToolPara(Int32 toolNo, Int32 index, ref Double value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(doubleSize);
            ret = HNC_ToolGetToolPara(toolNo, index, ptr, clientNo);
            value = (Double)Marshal.PtrToStructure(ptr, typeof(Double));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ToolSetToolPara(Int32 toolNo, Int32 index, Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            Marshal.WriteInt32(ptr, value);
            ret = HNC_ToolSetToolPara(toolNo, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ToolSetToolPara(Int32 toolNo, Int32 index, Double value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(doubleSize);
            Marshal.StructureToPtr(value, ptr, true);
            ret = HNC_ToolSetToolPara(toolNo, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_VarGetValue(Int32 type, Int32 no, Int32 index, ref Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            ret = HNC_VarGetValue(type, no, index, ptr, clientNo);
            value = Marshal.ReadInt32(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_VarGetValue(Int32 type, Int32 no, Int32 index, ref Double value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(doubleSize);
            ret = HNC_VarGetValue(type, no, index, ptr, clientNo);
            value = (Double)Marshal.PtrToStructure(ptr, typeof(Double));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_VarSetValue(Int32 type, Int32 no, Int32 index, Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            Marshal.WriteInt32(ptr, value);
            ret = HNC_VarSetValue(type, no, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_VarSetValue(Int32 type, Int32 no, Int32 index, Double value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(doubleSize);
            Marshal.StructureToPtr(value, ptr, true);
            ret = HNC_VarSetValue(type, no, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_SystemGetValue(Int32 type, ref String value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.SYS_DATA_LEN);
            ret = HNC_SystemGetValue(type, ptr, clientNo);
            value = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_SystemGetValue(Int32 type, ref Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            ret = HNC_SystemGetValue(type, ptr, clientNo);
            value = Marshal.ReadInt32(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_SystemSetValue(Int32 type, String value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.SYS_DATA_LEN);
            ptr = Marshal.StringToCoTaskMemAnsi(value);
            ret = HNC_SystemSetValue(type, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_SystemSetValue(Int32 type, Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(intSize);
            Marshal.WriteInt32(ptr, value);
            ret = HNC_SystemSetValue(type, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetParaPropEx(Int32 parmId, Byte propType, ref Int32 propValue, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetParaPropEx(parmId, propType, ptr, clientNo);
            propValue = Marshal.ReadInt32((IntPtr)(ptr.ToInt32() + intSize));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetParaPropEx(Int32 parmId, Byte propType, ref Double propValue, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetParaPropEx(parmId, propType, ptr, clientNo);
            propValue = (Double)Marshal.PtrToStructure((IntPtr)(ptr.ToInt32() + intSize), typeof(Double));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetParaPropEx(Int32 parmId, Byte propType, SByte[] propValue, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetParaPropEx(parmId, propType, ptr, clientNo);
            Int32 length = propValue.Length < HncDataDef.SDATAPROPOTY_LEN ? propValue.Length : HncDataDef.SDATAPROPOTY_LEN;

            if (ret == 0)
            {
                for (Int32 i = 0; i < length; ++i)
                {
                    propValue[i] = (SByte)Marshal.ReadByte((IntPtr)(ptr.ToInt32() + intSize + i));
                }
            }
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetParaPropEx(Int32 parmId, Byte propType, ref String propValue, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetParaPropEx(parmId, propType, ptr, clientNo);
            propValue = Marshal.PtrToStringAnsi((IntPtr)(ptr.ToInt32() + intSize));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetParaPropEx(Int32 parmId, Byte propType, Int32 propValue, Int16 clientNo)
        {
            Int32 ret = -1;

            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            Marshal.WriteInt32(ptr, 1);
            Marshal.WriteInt32((IntPtr)(ptr.ToInt32() + intSize), propValue);
            ret = HNC_ParamanSetParaPropEx(parmId, propType, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetParaPropEx(Int32 parmId, Byte propType, Double propValue, Int16 clientNo)
        {
            Int32 ret = -1;

            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            Marshal.WriteInt32(ptr, 2);
            Marshal.StructureToPtr(propValue, (IntPtr)(ptr.ToInt32() + intSize), true);
            ret = HNC_ParamanSetParaPropEx(parmId, propType, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetParaPropEx(Int32 parmId, Byte propType, SByte[] propValue, Int16 clientNo)
        {
            Int32 ret = -1;

            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            Marshal.WriteInt32(ptr, 11);
            for (Int32 i = 0; i < propValue.Length; ++i)
            {
                Marshal.WriteByte((IntPtr)(ptr.ToInt32() + intSize + i), (Byte)propValue[i]);
            }
            
            ret = HNC_ParamanSetParaPropEx(parmId, propType, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetParaPropEx(Int32 parmId, Byte propType, String propValue, Int16 clientNo)
        {
            Int32 ret = -1;

            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            Marshal.WriteInt32(ptr, 5);

            Byte[] tempArray = Encoding.Default.GetBytes(propValue);
            Byte[] strArray = new Byte[tempArray.Length + 1];
            tempArray.CopyTo(strArray, 0);
            strArray[strArray.Length - 1] = 0;
            Marshal.Copy(strArray, 0, (IntPtr)(ptr.ToInt32() + intSize), strArray.Length);
            
            ret = HNC_ParamanSetParaPropEx(parmId, propType, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetParaProp(Int32 filetype, Int32 subid, Int32 index, Byte prop_type, ref Int32 prop_value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetParaProp(filetype, subid, index, prop_type, ptr, clientNo);
            prop_value = Marshal.ReadInt32((IntPtr)(ptr.ToInt32() + intSize));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetParaProp(Int32 filetype, Int32 subid, Int32 index, Byte prop_type, ref Double prop_value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetParaProp(filetype, subid, index, prop_type, ptr, clientNo);
            prop_value = (Double)Marshal.PtrToStructure((IntPtr)(ptr.ToInt32() + intSize), typeof(Double));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetParaProp(Int32 filetype, Int32 subid, Int32 index, Byte prop_type, SByte[] prop_value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetParaProp(filetype, subid, index, prop_type, ptr, clientNo);
            Int32 length = prop_value.Length < HncDataDef.SDATAPROPOTY_LEN ? prop_value.Length : HncDataDef.SDATAPROPOTY_LEN;

            if (ret == 0)
            {
                for (Int32 i = 0; i < length; ++i)
                {
                    prop_value[i] = (SByte)Marshal.ReadByte((IntPtr)(ptr.ToInt32() + intSize + i));
                }
            }
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetParaProp(Int32 filetype, Int32 subid, Int32 index, Byte prop_type, ref String prop_value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetParaProp(filetype, subid, index, prop_type, ptr, clientNo);
            prop_value = Marshal.PtrToStringAnsi((IntPtr)(ptr.ToInt32() + intSize));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetParaProp(Int32 filetype, Int32 subid, Int32 index, Byte prop_type, Int32 prop_value, Int16 clientNo)
        {
            Int32 ret = -1;

            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            Marshal.WriteInt32(ptr, 1);
            Marshal.WriteInt32((IntPtr)(ptr.ToInt32() + intSize), prop_value);
            ret = HNC_ParamanSetParaProp(filetype, subid, index, prop_type, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetParaProp(Int32 filetype, Int32 subid, Int32 index, Byte prop_type, Double prop_value, Int16 clientNo)
        {
            Int32 ret = -1;

            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            Marshal.WriteInt32(ptr, 2);
            Marshal.StructureToPtr(prop_value, (IntPtr)(ptr.ToInt32() + intSize), true);
            ret = HNC_ParamanSetParaProp(filetype, subid, index, prop_type, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetParaProp(Int32 filetype, Int32 subid, Int32 index, Byte prop_type, SByte[] prop_value, Int16 clientNo)
        {
            Int32 ret = -1;

            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            Marshal.WriteInt32(ptr, 11);
            for (Int32 i = 0; i < prop_value.Length; ++i)
            {
                Marshal.WriteByte((IntPtr)(ptr.ToInt32() + intSize + i), (Byte)prop_value[i]);
            }
            ret = HNC_ParamanSetParaProp(filetype, subid, index, prop_type, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetParaProp(Int32 filetype, Int32 subid, Int32 index, Byte prop_type, String prop_value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            Marshal.WriteInt32(ptr, 5);

            Byte[] tempArray = Encoding.Default.GetBytes(prop_value);
            Byte[] strArray = new Byte[tempArray.Length + 1];
            tempArray.CopyTo(strArray, 0);
            strArray[strArray.Length - 1] = 0;
            Marshal.Copy(strArray, 0, (IntPtr)(ptr.ToInt32() + intSize), strArray.Length);

            ret = HNC_ParamanSetParaProp(filetype, subid, index, prop_type, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetSubClassProp(Int32 fileNo, Byte propType, ref Int32 propValue, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetSubClassProp(fileNo, propType, ptr, clientNo);
            propValue = Marshal.ReadInt32((IntPtr)(ptr.ToInt32() + intSize));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetSubClassProp(Int32 fileNo, Byte propType, ref String propValue, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetSubClassProp(fileNo, propType, ptr, clientNo);
            propValue = Marshal.PtrToStringAnsi((IntPtr)(ptr.ToInt32() + intSize));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }
        
        public static Int32 HNC_MacroVarSetValue(Int32 no, SDataUnion var, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SDataUnion)));
            Marshal.StructureToPtr(var, ptr, true);
            ret = HNC_MacroVarSetValue(no, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetItem(Int32 fileNo, Int32 subNo, Int32 index, ref Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SParamValue)));
            ret = HNC_ParamanGetItem(fileNo, subNo, index, ptr, clientNo);
            value = Marshal.ReadInt32(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetItem(Int32 fileNo, Int32 subNo, Int32 index, ref Double value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SParamValue)));
            ret = HNC_ParamanGetItem(fileNo, subNo, index, ptr, clientNo);
            value = (Double)Marshal.PtrToStructure(ptr, typeof(Double));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetItem(Int32 fileNo, Int32 subNo, Int32 index, SByte[] value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SParamValue)));
            ret = HNC_ParamanGetItem(fileNo, subNo, index, ptr, clientNo);
            Int32 length = value.Length < HncDataDef.SPARAMVALUE_LEN ? value.Length : HncDataDef.SPARAMVALUE_LEN;

            if (ret == 0)
            {
                for (Int32 i = 0; i < length; ++i)
                {
                    value[i] = (SByte)Marshal.ReadByte((IntPtr)(ptr.ToInt32() + i));
                }
            }
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetItem(Int32 fileNo, Int32 subNo, Int32 index, ref String value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SParamValue)));
            ret = HNC_ParamanGetItem(fileNo, subNo, index, ptr, clientNo);
            value = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetItem(Int32 fileNo, Int32 subNo, Int32 index, Int32 value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SParamValue)));
            Marshal.WriteInt32(ptr, value);
            ret = HNC_ParamanSetItem(fileNo, subNo, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetItem(Int32 fileNo, Int32 subNo, Int32 index, Double value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SParamValue)));
            Marshal.StructureToPtr(value, ptr, true);
            ret = HNC_ParamanSetItem(fileNo, subNo, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetItem(Int32 fileNo, Int32 subNo, Int32 index, SByte[] value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SParamValue)));
            for (Int32 i = 0; i < value.Length; ++i)
            {
                Marshal.WriteByte((IntPtr)(ptr.ToInt32() + i), (Byte)value[i]);
            }
            ret = HNC_ParamanSetItem(fileNo, subNo, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanSetItem(Int32 fileNo, Int32 subNo, Int32 index, String value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SParamValue)));
            Byte[] tempArray = Encoding.Default.GetBytes(value);
            Byte[] strArray = new Byte[tempArray.Length + 1];
            tempArray.CopyTo(strArray, 0);
            strArray[strArray.Length - 1] = 0;

            Marshal.Copy(strArray, 0, ptr, strArray.Length);
            ret = HNC_ParamanSetItem(fileNo, subNo, index, ptr, clientNo);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_NetGetIpaddr(ref String ip, ref UInt16 port, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(ipSize);
            ret = HNC_NetGetIpaddr(ptr, ref port, clientNo);
            ip = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_SamplGetData(Int32 ch, ref Int32 num, Int32[] data, Int16 clientNo)
        {
            Int32 ret = -1;
            if (num <= 0)
            {
                return ret;
            }

            Int32 dataLength = num * intSize;
            IntPtr ptr = Marshal.AllocHGlobal(dataLength);
            ret = HNC_SamplGetData(ch, ref num, ptr, clientNo);

            if (ret == 0)
            {
                Marshal.Copy(ptr, data, 0, num);
            }
            
            Marshal.FreeHGlobal(ptr);
            return ret;
        }

        public static Int32 HNC_ParamanGetStr(Int32 fileNo, Int32 subNo, Int32 index, ref String value, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_PROP_DATA_LEN);
            ret = HNC_ParamanGetStr(fileNo, subNo, index, ptr, clientNo);
            value = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_FprogGetFullName(Int32 ch, ref String progName, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PROG_NAME_LEN);
            ret = HNC_FprogGetFullName(ch, ptr, clientNo);
            progName = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_ParamanGetFileName(Int32 fileNo, ref String buf, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.PAR_FILE_NAME_LEN);
            ret = HNC_ParamanGetFileName(fileNo, ptr, clientNo);
            buf = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_AlarmGetData(Int32 type, Int32 level, Int32 index, ref Int32 alarmNo, ref String alarmText, Int16 clientNo)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(HncDataDef.ALARM_DATA_LEN);
            ret = HNC_AlarmGetData(type, level, index, ref alarmNo, ptr, clientNo);
            alarmText = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_NetFileGetDirInfo(String dirname, ncfind_t[] info, ref UInt16 num, Int16 clientNo)
        {
            Int32 size = Marshal.SizeOf(typeof(ncfind_t));
            IntPtr ptr = Marshal.AllocHGlobal(size * info.Length);
            Int32 ret = -1;
            ret = HNC_NetFileGetDirInfo(dirname, ptr, ref num, clientNo);

            if (ret == 0)
            {
                for (Int32 i = 0; i < num; ++i)
                {
                    info[i] = (ncfind_t)Marshal.PtrToStructure((IntPtr)(ptr.ToInt32() + i * size), typeof(ncfind_t));
                }
            }

            Marshal.FreeHGlobal(ptr);
            return ret;
        }

        public static Int32 HNC_EventGetSysEv(ref SEventElement ev)
        {
            Int32 ret = -1;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SEventElement)));
            ret = HNC_EventGetSysEv(ptr);
            ev = (SEventElement)Marshal.PtrToStructure(ptr, typeof(SEventElement));
            Marshal.FreeHGlobal(ptr);

            return ret;
        }

        public static Int32 HNC_AlarmGetHistoryData(Int32 index, ref Int32 count, AlarmHisData[] data, Int16 clientNo)
        {
            Int32 MAX_ALARM_HISDATA_LEN = AlarmDef.ALARM_HISTORY_MAX_NUM * Marshal.SizeOf(typeof(AlarmHisData));
            IntPtr ptr = Marshal.AllocHGlobal(MAX_ALARM_HISDATA_LEN);
            Int32 ret = HNC_AlarmGetHistoryData(index, ref count, ptr, clientNo);
            if (ret == 0)
            {
                for (Int32 i = 0; i < count; i++)
                {
                    data[i] = (AlarmHisData)Marshal.PtrToStructure((IntPtr)(ptr.ToInt32() + i * Marshal.SizeOf(typeof(AlarmHisData))), typeof(AlarmHisData));
                }
            }

            Marshal.FreeHGlobal(ptr);

            return ret;
        }
    }
}
