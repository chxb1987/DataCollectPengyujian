using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TwinCAT.Ads;
using System.IO;

namespace demo
{
    public partial class TwinCAT_ADS : Form
    {
        public TwinCAT_ADS()
        {
            InitializeComponent();
            

        }

        //定义所需变量

        private float writereal = 0;
        private float readreal = 0;

        private double writelreal = 0;
        private double readlreal = 0;

        private double temperature = 0;
        private double humididty = 0;

        //定义结构体类型
        public struct adsstruct
        {
            public double s1;
            public double s2;
            public double s3;
            public double s4;

        }
        //实例化结构体
        private adsstruct structtest = new adsstruct();

        //定义句柄变量
        private int hvar = new int();
        //通讯数据定义
        private TcAdsClient tcclient;//定义通讯协议



        private void TwinCAT_ADS_LOAD()
        {
            //通讯协议
            tcclient = new TcAdsClient();
            tcclient.Connect(801);
        }
        //readlreal
        private void button_Click(object sender, EventArgs e)
        {
            TwinCAT_ADS_LOAD();
            try
            {
                hvar = tcclient.CreateVariableHandle("MAIN.adsstru.KL3068_Value_Ch1");
            }
            catch (Exception err)
            {
                MessageBox.Show("get hvar error \n" + err.ToString());
            }
            AdsStream datastream = new AdsStream(8);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;

            try
            {
                tcclient.Read(hvar, datastream);
                readlreal = binread.ReadDouble();
                textBox1.Text = readlreal.ToString("0.00");
                textBox6.Text = Calc_temper(readlreal).ToString("0.00") + "度";
            }

            catch (Exception err)
            {
                MessageBox.Show("read value error");
            }
            try
            {
                tcclient.DeleteVariableHandle(hvar);
            }
            catch (Exception err)
            {
                MessageBox.Show("read delect hvar error");
            }
        }

        //readlreal
        private void button2_Click(object sender, EventArgs e)
        {
          
            try
            {
                hvar = tcclient.CreateVariableHandle("MAIN.adsstru.KL3068_Value_Ch2");
            }
            catch (Exception err)
            {
                MessageBox.Show("get hvar error \n" + err.ToString());
            }
            AdsStream datastream = new AdsStream(8);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;

            try
            {
                tcclient.Read(hvar, datastream);
                readlreal = binread.ReadDouble();
                textBox2.Text = readlreal.ToString("0.00");
                textBox7.Text = Calc_humid(readlreal).ToString("0.00") + "%";
            }

            catch (Exception err)
            {
                MessageBox.Show("read value error");
            }
            try
            {
                tcclient.DeleteVariableHandle(hvar);
            }
            catch (Exception err)
            {
                MessageBox.Show("read delect hvar error");
            }
        }

        //readlreal
        private void button3_Click(object sender, EventArgs e)
        {
           
            try
            {
                hvar = tcclient.CreateVariableHandle("MAIN.adsstru.KL3068_Value_Ch3");
            }
            catch (Exception err)
            {
                MessageBox.Show("get hvar error \n" + err.ToString());
            }
            AdsStream datastream = new AdsStream(8);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;

            try
            {
                tcclient.Read(hvar, datastream);
                readlreal = binread.ReadDouble();
                textBox3.Text = readlreal.ToString("0.00");
                textBox8.Text = Calc_temper(readlreal).ToString("0.00") + "度";
            }

            catch (Exception err)
            {
                MessageBox.Show("read value error");
            }
            try
            {
                tcclient.DeleteVariableHandle(hvar);
            }
            catch (Exception err)
            {
                MessageBox.Show("read delect hvar error");
            }
        }

        //readlreal
        private void button4_Click(object sender, EventArgs e)
        {
         
            try
            {
                hvar = tcclient.CreateVariableHandle("MAIN.adsstru.KL3068_Value_Ch4");
            }
            catch (Exception err)
            {
                MessageBox.Show("get hvar error \n" + err.ToString());
            }
            AdsStream datastream = new AdsStream(8);
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;

            try
            {
                tcclient.Read(hvar, datastream);
                readlreal = binread.ReadDouble();
                textBox4.Text = readlreal.ToString("0.00");
                textBox9.Text = Calc_humid(readlreal).ToString("0.00") + "%";
            }

            catch (Exception err)
            {
                MessageBox.Show("read value error");
            }
            try
            {
                tcclient.DeleteVariableHandle(hvar);
            }
            catch (Exception err)
            {
                MessageBox.Show("read delect hvar error");
            }
        }

        double Calc_temper(double adsdata)
        {
            return (50 * adsdata / 10);

        }
        double Calc_humid(double adsdata)
        {
            return (100 / 10 * adsdata);

        }
        private void button5_Click(object sender, EventArgs e)
        {
            timer_ADS.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer_ADS.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox5.Text = string.Empty;
        }

        private void timer_ADS_Tick(object sender, EventArgs e)
        {
            try
            {
                hvar = tcclient.CreateVariableHandle("MAIN.adsstru");
            }
            catch (Exception err)
            {
                MessageBox.Show("get hvar error");
            }
            AdsStream datastream = new AdsStream(32);//4*8=32
            BinaryReader binread = new BinaryReader(datastream);
            datastream.Position = 0;

            try
            {
                tcclient.Read(hvar, datastream);


                structtest.s1 = binread.ReadDouble();
                structtest.s2 = binread.ReadDouble();
                structtest.s3 = binread.ReadDouble();
                structtest.s4 = binread.ReadDouble();



                textBox5.Text += structtest.s1.ToString("    0.00") + structtest.s2.ToString(" 0.00") + structtest.s3.ToString("    0.00")+structtest.s4.ToString(" 0.00")+System.Environment.NewLine;
                
            }

            catch (Exception err)
            {
                MessageBox.Show("read value error");
            }
            try
            {
                tcclient.DeleteVariableHandle(hvar);
            }
            catch (Exception err)
            {
                MessageBox.Show("read delect hvar error");
            }


        }
    }
}
