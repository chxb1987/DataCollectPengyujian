using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace demo
{
    public class Constants
    {
        public const int gskMaxAlarmNum = 20;

        public const int AXIS_LOGIC_NUM_X = 0;
        public const int AXIS_LOGIC_NUM_Y = 1;
        public const int AXIS_LOGIC_NUM_Z = 2;

        public const int LOGINFLAG = 1;
        public const int LOGOFFLAG = 0;

        public const int TB_ROWNUM = 0;
        public const int TB_STATE = 1;
        public const int TB_ID = 2;
        public const int TB_IP = 3;
        public const int TB_PORT = 4;
        public const int TB_FCTR = 5;
        public const int TB_CLTNO = 6;
        public const int TB_DT = 13;


        public const int IDENTITY = 1;
        public const int ERRORINFO = 2;
        public const int RUNSTATE = 3;
        public const int LOGIN = 4;
        public const int ENVINFO = 5;

        public const int ERRORFIND = 0;
        public const int ERRORCLEAR = 1;
    }
    class CGlbFunc
    {
       
        public static Int16[] clientNo = {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};
        public static string[] Power_time_today = new string[20]; //作为系统本次开机时间
        public static string[] Error_time_last = new string[20];//系统最近一次报警的时间，大概可计算出MTBF
        public static string[] ip_info = new string[20];
        public static string[] id_info = new string[20];
        public static ushort[] port_info = new ushort[20];
        public static Int16 SumOfClient = 0;
        public static IntPtr[] gsktock=new IntPtr[20];
        public static Int32[] LoginFlag = new Int32[20];

        public static Int16 WebLinkFlag = 0;
        public static Int16 DbLinkFlag = 0;

        public static byte[] StructureToByte<T>(T structure)
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[size];
            IntPtr bufferIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structure, bufferIntPtr, true);
                Marshal.Copy(bufferIntPtr, buffer, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(bufferIntPtr);
            }
            return buffer;
        }

        public static T ByteToStructure<T>(byte[] dataBuffer)
        {
            object structure = null;
            int size = Marshal.SizeOf(typeof(T));

            Debug.Assert(size == dataBuffer.Length);

            IntPtr allocIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(dataBuffer, 0, allocIntPtr, size);
                structure = Marshal.PtrToStructure(allocIntPtr, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(allocIntPtr);
            }
            return (T)structure;
        }
    }
}
