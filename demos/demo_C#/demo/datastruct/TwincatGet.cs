using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TwinCAT.Ads;

namespace demo.datastruct
{
   public class TwincatGet
    {
        /*twincat ads 读取数据过程*/
        //定义结构体类型
        public struct ADS_struct
        {
            public double tmp;  //温度
            public double humi;  //湿度
            public double u;  //1#电压
            public double v;   //2#电压
            public double w;   //3#电压
            public bool smokeflag;//烟雾报警

        }
        //实例化结构体
        public ADS_struct structtest = new ADS_struct();

        //定义句柄变量
        private int hvar = new int();
        //通讯数据定义
        public  static TcAdsClient tcclient;//定义通讯协议
        public static void TwinCAT_ADS_LOAD()
        {
            //通讯协议
            tcclient = new TcAdsClient();
            tcclient.Connect(801);

        }
        private double Calc_temper(double adsdata)
        {
            return (50 * adsdata / 10);

        }
        private double Calc_humid(double adsdata)
        {
            return (100 / 10 * adsdata);

        }
        private double Calc_voge(double adsdata)
        {
            return (380 / 8 * adsdata);

        }
        public bool smokeflag;
       public bool GetSmokeFlag()
       {
           try
           {
               int handle_g_SetState = tcclient.CreateVariableHandle("MAIN.smokeFlag");
               AdsStream dataStream = new AdsStream(1);
               BinaryReader binRead = new BinaryReader(dataStream);
               tcclient.Read(handle_g_SetState, dataStream);
               dataStream.Position = 0;
               smokeflag = binRead.ReadBoolean();
           }
           catch (Exception)
           {
               MessageBox.Show("ADS read value  smokeFlag error");
              
           } 
           return smokeflag;
       }

        public void ADS_dataget(string sid, int num, Real_status real_data, Data_Env_info env_info)
        {
            //if (num == 4)
            //{
                try
                {
                    hvar = tcclient.CreateVariableHandle("MAIN.adsstru");
                }
                catch (Exception err)
                {
                    MessageBox.Show("ADS get hvar error");

                }
                AdsStream datastream = new AdsStream(40);// ads 字节流 5*8+1个bool
                BinaryReader binread = new BinaryReader(datastream);
                datastream.Position = 0;

                try
                {
                    tcclient.Read(hvar, datastream);
                    structtest.tmp = binread.ReadDouble();  //获取的5个信号量，由于电压传感器还未接入
                    structtest.humi = binread.ReadDouble();
                    structtest.u = binread.ReadDouble();
                    structtest.v = binread.ReadDouble();
                    structtest.w = binread.ReadDouble();
                    //structtest.smokeflag = binread.ReadBoolean();
                    real_data.douTemperature = Calc_temper(structtest.tmp);
                    real_data.douHumidity = Calc_humid(structtest.humi);
                    real_data.douVoltage_1ch = Calc_voge(structtest.u);
                    real_data.douVoltage_2ch = Calc_voge(structtest.v);
                    real_data.douVoltage_3ch = Calc_voge(structtest.w);
                    env_info.hmi = (float)real_data.douHumidity;
                    env_info.tep = (float)real_data.douTemperature;
                    env_info.u = (float)real_data.douVoltage_1ch;
                    env_info.v = (float)real_data.douVoltage_2ch;
                    env_info.w = (float)real_data.douVoltage_3ch;
                    env_info.id = sid;
                    env_info.time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff");//获得系统时间，当作时间戳
                    if (num == 4)
                    {
                        FormMain.queue_env.Enqueue(env_info);
                    }
                    //Console.WriteLine(env_info.hmi+"  "+env_info.tep+"   "+env_info.u);

                }

                catch (Exception err)
                {
                    MessageBox.Show("ADS read value error");
                }
                try
                {
                    tcclient.DeleteVariableHandle(hvar);
                }
                catch (Exception err)
                {
                    MessageBox.Show("ADS delect hvar error");
                }


              
            //}
        }
    }
}
