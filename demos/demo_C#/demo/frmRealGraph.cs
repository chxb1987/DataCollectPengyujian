using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace demo
{
    public partial class frmRealGraph : Form
    {
        public frmRealGraph()
        {
            InitializeComponent();
        }
        Random ran = new Random();
        PointPairList list1 = new PointPairList();
        PointPairList list2 = new PointPairList();
        PointPairList list3 = new PointPairList();
        LineItem myCurve1, myCurve2, myCurve3;
        /// <summary>
        /// 根据下拉框选择的值，在相应的zedgraph中绘制图像
        /// </summary>
        /// <param name="num">下拉框</param>
        /// <param name="param">选中的参数</param>
        public void showGraph(int num,string param)
        {                      
            switch(num)
            {
                case 1:
                    time_showGraph.Enabled = true;
                    setingCurve(zedGraphControl1, param,num);
                    break;
                case 2:                    
                    time_showGraph2.Enabled = true;
                    setingCurve(zedGraphControl2, param,num);
                    break;
                case 3:                   
                    time_showGraph3.Enabled = true;
                    setingCurve(zedGraphControl3, param,num);
                    break;
                default:
                    break;
            }
           
        }
        Double cur_x=0;
        Double cur_y=0;
        Double cur_z=0;
        Double cur_main = 0;
        int ret = -1;
        short  clientno = -1;

        public  void getclientnum(int num)//num 表示华中的第几台设备
        {
           UInt16 ret = HncApi.HNC_NetIsConnect(CGlbFunc.clientNo[num+1]);
            if(ret==0)
            {
                clientno = CGlbFunc.clientNo[num+1];
            }
        }

        private void initList(PointPairList list,string param,ZedGraphControl zed)
        {
            zed.GraphPane.CurveList.Clear();
            zed.GraphPane.GraphObjList.Clear();
            switch (param)
            {
                case "主轴负载":
                    zed.GraphPane.Title.Text = "主轴负载电流";
                    zed.GraphPane.XAxis.Title.Text = "时间";
                    zed.GraphPane.YAxis.Title.Text = "主轴电流";
                    zed.GraphPane.XAxis.Type = ZedGraph.AxisType.DateAsOrdinal;
                    break;
                case "X轴负载":
                    zed.GraphPane.Title.Text = "X轴负载电流";
                    zed.GraphPane.XAxis.Title.Text = "时间";
                    zed.GraphPane.YAxis.Title.Text = "X轴电流";
                    zed.GraphPane.XAxis.Type = ZedGraph.AxisType.DateAsOrdinal;
                    break;
                case "Y轴负载":
                    zed.GraphPane.Title.Text = "Y轴负载电流";
                    zed.GraphPane.XAxis.Title.Text = "时间";
                    zed.GraphPane.YAxis.Title.Text = "Y轴电流";
                    zed.GraphPane.XAxis.Type = ZedGraph.AxisType.DateAsOrdinal;
                    break;
                case "Z轴负载":
                    zed.GraphPane.Title.Text = "Z轴负载电流";
                    zed.GraphPane.XAxis.Title.Text = "时间";
                    zed.GraphPane.YAxis.Title.Text = "Z轴电流";
                    zed.GraphPane.XAxis.Type = ZedGraph.AxisType.DateAsOrdinal;
                   
                    break;
                case "供电电流":
                    zed.GraphPane.Title.Text = "供电电流图";
                    zed.GraphPane.XAxis.Title.Text = "时间";
                    zed.GraphPane.YAxis.Title.Text = "供电电流";
                    zed.GraphPane.XAxis.Type = ZedGraph.AxisType.DateAsOrdinal;
                    //double y1= 0.0;

                    break;
                case "供电电压":
                    zed.GraphPane.Title.Text = "供电电压图";
                    zed.GraphPane.XAxis.Title.Text = "时间";
                    zed.GraphPane.YAxis.Title.Text = "供电电压";
                    break;
                default:
                    break;
            }
            //初始化坐标点为0
            for (int i = 0; i <= 100; i++)
            {
                double x = (double)new XDate(DateTime.Now.AddSeconds(-(100 - i)));
                double y = 0;
                list.Add(x, y);
            }
            
        }
        double cur1 = 0,cur=0,curate=0;

        private void add2list(PointPairList list,string param,ZedGraphControl zed)
        {
            double x = (double)new XDate(DateTime.Now); ;
            double y = 0.0;
            zed.GraphPane.XAxis.Scale.MaxAuto = true;
            //如果要在折线图内显示指定数量的点，只需要在添加坐标之前把第一个坐标点去掉：
            if (list.Count >= 100)
            {
                list.RemoveAt(0);
            }
            switch (param)
            {
                case "主轴负载":
                    //值
                    ret = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_RATED_CUR, 5, ref cur, clientno);
                    if (ret == 0)
                    {
                   
                        cur1 = cur;
                        //Current_Spindle_Rated.Text = value64.ToString("0.000");
                    }
                    ret = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, 5, ref curate, clientno);
                    if (ret == 0)
                    {
                        //value64 = value64 / Turn_unit * 1000 * 60;
                        y = curate / 100 * cur1;
                        //Current_Spindle_Load.Text = value64.ToString("0.000");
                    }
                   
                    list.Add(x, y);
                    break;
                case "X轴负载":                                       
                    ret = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, Constants.AXIS_LOGIC_NUM_X, ref cur_x, clientno);
                    if (ret == 0)
                    {
                        y = cur_x;   //负载电流单位为A,机床空负载时有静态误差,会出现负电流
                    }
                    list.Add(x, y);
                    break;
                case "Y轴负载":                     
                    ret = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, Constants.AXIS_LOGIC_NUM_Y, ref cur_y, clientno);
                    if (ret == 0)
                    {
                        y = cur_y;   //负载电流单位为A,机床空负载时有静态误差,会出现负电流
                    }
                    list.Add(x, y);
                    break;
                case "Z轴负载":                    
                    ret = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, Constants.AXIS_LOGIC_NUM_Z, ref cur_z, clientno);
                    if (ret == 0)
                    {
                        y = cur_z;   //负载电流单位为A,机床空负载时有静态误差,会出现负电流
                    }
                    list.Add(x, y);
                    break;
                case "供电电流":
                    
                    //double y1= 0.0;

                    break;
                case "供电电压":
                    
                    break;
                default:
                    break;
            }
           
        }

        private void setingCurve(ZedGraphControl zed,string param,int num)
        {           
            switch(num)
            {
                case 1:
                    initList(list1, param, zed);                    
                    myCurve1 = zed.GraphPane.AddCurve(param, list1, Color.WhiteSmoke, SymbolType.None);
                    break;
                case 2:
                    initList(list2, param, zed);                    
                    myCurve2 = zed.GraphPane.AddCurve(param, list2, Color.WhiteSmoke, SymbolType.None);
                    break;
                case 3:
                    initList(list3, param, zed);                    
                    myCurve3 = zed.GraphPane.AddCurve(param, list3, Color.WhiteSmoke, SymbolType.None);
                    break;
                default:
                    break;
            }
            zed.GraphPane.Chart.Fill = new Fill(System.Drawing.Color.Black);
            zed.AxisChange();
            zed.Refresh();
        }
        string[] name = new string[3];
        private void cbx_data1_SelectedIndexChanged(object sender, EventArgs e)
        {
            time_showGraph.Enabled = false;
            list1.Clear();
            ComboBox cb = sender as ComboBox;
            name[0] = cb.SelectedItem.ToString();
            //time_showGraph.Enabled = true;
            showGraph(1,name[0]);
        }

        private void cbx_data2_SelectedIndexChanged(object sender, EventArgs e)
        {
            time_showGraph2.Enabled = false;
            list2.Clear();
            ComboBox cb = sender as ComboBox;
            name[1] = cb.SelectedItem.ToString();
            showGraph(2, name[1]);
        }

        private void cmb_data3_SelectedIndexChanged(object sender, EventArgs e)
        {
            time_showGraph3.Enabled = false;

            list3.Clear();
            ComboBox cb = sender as ComboBox;
            name[2] = cb.SelectedItem.ToString();
            showGraph(3, name[2]);
        }


        private void time_showGraph_Tick(object sender, EventArgs e)
        {                       
            add2list(list1, name[0], zedGraphControl1);
            
            this.zedGraphControl1.AxisChange();
            this.zedGraphControl1.Refresh();
        }
        private void time_showGraph2_Tick(object sender, EventArgs e)
        {
            //
            add2list(list2, name[1], zedGraphControl2);
            this.zedGraphControl2.AxisChange();
            this.zedGraphControl2.Refresh();
        }

        private void time_showGraph3_Tick(object sender, EventArgs e)
        {
            add2list(list3, name[2], zedGraphControl3);
            this.zedGraphControl3.AxisChange();
            this.zedGraphControl3.Refresh();
        }

        private void frmRealGraph_FormClosing(object sender, FormClosingEventArgs e)
        {
            time_showGraph.Enabled = false;
            time_showGraph2.Enabled = false;
            time_showGraph3.Enabled = false;
        }
    }
}
