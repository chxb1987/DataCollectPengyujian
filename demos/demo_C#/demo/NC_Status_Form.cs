using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace demo
{
    
    public partial class NC_Status_Form : Form
    {
        public NC_Status_Form()
        {
            InitializeComponent();
            tim_DetailStatus.Enabled = true;
        }

        private   short clientnum = 0;
        public   short intclientnum
        {
            set { clientnum = value; }
            get { return clientnum; }
        }

        private void NC_Status_Update()
        {
            double value64 = 0;
            int Move_unit = 0;
            int Turn_unit = 0;
            int ret32 = 0;
            int value = 0;
            UInt16 ret = HncApi.HNC_NetIsConnect(CGlbFunc.clientNo[clientnum]);
            if (ret == 0)
            {

                //ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_TYPE, Constants.AXIS_LOGIC_NUM_X, ref axisType, clientnum);
                //switch (axisType)
                //{
                //    case 1: // 直线轴
                HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_MOVE_UNIT, ref Move_unit, CGlbFunc.clientNo[clientnum]);
                    //    break;
                    //default: // 旋转轴
                HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_TURN_UNIT, ref Turn_unit, CGlbFunc.clientNo[clientnum]);
                //        break;
                //}
                ///*    电机转速     */
                //ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_MOTOR_REV, Constants.AXIS_LOGIC_NUM_X, ref value64, clientnum);
                //if (ret32 == 0)
                //{
                //    Rev_X.Text = value64.ToString();
                //}
                //ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_MOTOR_REV, Constants.AXIS_LOGIC_NUM_Y, ref value64, clientnum);
                //if (ret32 == 0)
                //{
                //    Rev_Y.Text = value64.ToString();
                //}
                //ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_MOTOR_REV, Constants.AXIS_LOGIC_NUM_Z, ref value64, clientnum);
                //if (ret32 == 0)
                //{
                //    Rev_Z.Text = value64.ToString();
                //}
                        /*    电机跟踪误差     */
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_FOLLOW_ERR, Constants.AXIS_LOGIC_NUM_X, ref value, CGlbFunc.clientNo[clientnum]);
                        if (ret32 == 0)
                        {
                            value = value / Move_unit;
                            Rev_X_err.Text = value.ToString("0.0000");
                        }
                        ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_FOLLOW_ERR, Constants.AXIS_LOGIC_NUM_Y, ref value, CGlbFunc.clientNo[clientnum]);
                        if (ret32 == 0)
                        {
                            value = value / Move_unit;
                            Rev_Y_err.Text = value.ToString("0.0000");
                        }
                        ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_FOLLOW_ERR, Constants.AXIS_LOGIC_NUM_Z, ref value, CGlbFunc.clientNo[clientnum]);
                        if (ret32 == 0)
                        {
                            value = value / Move_unit;
                            Rev_Z_err.Text = value.ToString("0.0000");
                        }
                //ret2 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_POS_RCS, int.Parse(axisnum.Text), ref value, clientnum);
                //if (ret2 == 0)
                //{
                //    pos = (double)value / unit;
                //    Axis_position.Text += ("cmd_pos_rcs：" + pos.ToString() + System.Environment.NewLine);
                //}
                //ret2 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_POS_WCS, int.Parse(axisnum.Text), ref value, clientnum);
                //if (ret2 == 0)
                //{
                //    pos = (double)value / unit;
                //    Axis_position.Text += ("cmd_pos_wcs：" + pos.ToString() + System.Environment.NewLine);
                //}
                //ret2 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_POS, int.Parse(axisnum.Text), ref value, clientnum);
                //if (ret2 == 0)
                //{
                //    pos = (double)value / unit;
                //    Axis_type.Text = axisType.ToString();
                //    Axis_position.Text += ("pos：" + pos.ToString() + System.Environment.NewLine);
                //}
                /*    ACT_POS相对实际位置     */
                        ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_ACT_POS_RCS, Constants.AXIS_LOGIC_NUM_X, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    value = value / Move_unit;
                    Pos_Act_X.Text = value.ToString();
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_ACT_POS_RCS, Constants.AXIS_LOGIC_NUM_Y, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    value = value / Move_unit;
                    Pos_Act_Y.Text = value.ToString();
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_ACT_POS_RCS, Constants.AXIS_LOGIC_NUM_Z, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    value = value / Move_unit;
                    Pos_Act_Z.Text = value.ToString();
                }
                /*    CMD_POS相对指令位置     */
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_POS_RCS, Constants.AXIS_LOGIC_NUM_X, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    value = value / Move_unit;
                    Pos_Com_X.Text = value.ToString();
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_POS_RCS, Constants.AXIS_LOGIC_NUM_Y, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    value = value / Move_unit;
                    Pos_Com_Y.Text = value.ToString();
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_POS_RCS, Constants.AXIS_LOGIC_NUM_Z, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    value = value / Move_unit;
                    Pos_Com_Z.Text = value.ToString();
                }
                /*    Cur_Load负载电流     */
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, Constants.AXIS_LOGIC_NUM_X, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    Cur_Load_X.Text = value64.ToString();
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, Constants.AXIS_LOGIC_NUM_Y, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    Cur_Load_Y.Text = value64.ToString();
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, Constants.AXIS_LOGIC_NUM_Z, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    Cur_Load_Z.Text = value64.ToString();
                }
                /*    Cur_Rated额定负载电流     */
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_RATED_CUR, Constants.AXIS_LOGIC_NUM_X, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    Cur_Rated_X.Text = value64.ToString();
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_RATED_CUR, Constants.AXIS_LOGIC_NUM_Y, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    Cur_Rated_Y.Text = value64.ToString();
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_RATED_CUR, Constants.AXIS_LOGIC_NUM_Z, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    Cur_Rated_Z.Text = value64.ToString();
                }
                /*    Speed_Act实际进给速度     */
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_ACT_VEL, Constants.AXIS_LOGIC_NUM_X, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Move_unit * 1000 * 60;
                    Speed_Act_X.Text = value64.ToString("0.00");
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_ACT_VEL, Constants.AXIS_LOGIC_NUM_Y, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Move_unit * 1000 * 60;
                    Speed_Act_Y.Text = value64.ToString("0.00");
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_ACT_VEL, Constants.AXIS_LOGIC_NUM_Z, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Move_unit * 1000 * 60;  
                    Speed_Act_Z.Text = value64.ToString("0.00");
                }
                /*    Speed_Com指令进给速度     */
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_VEL, Constants.AXIS_LOGIC_NUM_X, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                   // value = value / Move_unit * 1000 * 60;
                    Speed_Com_X.Text = value.ToString("0.00");
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_VEL, Constants.AXIS_LOGIC_NUM_Y, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value = value / Move_unit * 1000 * 60;
                    Speed_Com_Y.Text = value.ToString("0.00");
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_CMD_VEL, Constants.AXIS_LOGIC_NUM_Z, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value = value / Move_unit * 1000 * 60;
                    Speed_Com_Z.Text = value.ToString("0.00");
                }
                else
                {
                    MessageBox.Show("IP" + CGlbFunc.ip_info[0] + "读取失败");
                }
                /*    主轴指令进给速度     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_CMD_FEEDRATE, 0, 0, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Turn_unit * 1000 * 60;
                    Chan_Cmd_Feedrate.Text = value64.ToString("0.00");
                }
                /*    主轴实际进给速度     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_ACT_FEEDRATE, 0, 0, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                   // value64 = value64 / Turn_unit * 1000 * 60;
                    Chan_Act_Feedrate.Text = value64.ToString("0.00");
                }
                /*    主轴指令速度     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_CMD_SPDL_SPEED, 0, 0, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Turn_unit * 1000 * 60;
                    Chan_Cmd_Speed.Text = value64.ToString("0.00");
                }
                /*    主轴实际速度     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_ACT_SPDL_SPEED, 0, 0, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Turn_unit * 1000 * 60;
                    Chan_Act_Speed.Text = value64.ToString("0.00");
                }
               
                /*增加主轴负载电流及三种倍率信息*/
                /*    主轴负载电流     */
                //ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_RATED_CUR, Constants.AXIS_LOGIC_NUM_X, ref value64, clientnum);
                Double cur1 = 0;
                //值
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_RATED_CUR, 5, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Turn_unit * 1000 * 60;
                    cur1 = value64;
                    Current_Spindle_Rated.Text = value64.ToString("0.000");
                }
                ret32 = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_LOAD_CUR, 5, ref value64, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Turn_unit * 1000 * 60;
                    value64 = value64 / 100 * cur1;
                    Current_Spindle_Load.Text = value64.ToString("0.000");
                }
                /*    进给修调     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_FEED_OVERRIDE, 0, 0, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Turn_unit * 1000 * 60;
                    Feed_override.Text = (value/100.0).ToString("0.00"+"%");
                }
                /*    快移修调     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_RAPID_OVERRIDE, 0, 0, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Turn_unit * 1000 * 60;
                    Rapid_override.Text = (value / 100.0).ToString("0.00" + "%");
                }
                /*    主轴转速修调     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_SPDL_OVERRIDE, 0, 0, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    //value64 = value64 / Turn_unit * 1000 * 60;
                    Spdl_override.Text = (value / 100.0).ToString("0.00" + "%");
                }


                /*    主轴逻辑轴号     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_SPDL_LAX, 0, 0, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret == 0)
                {
                    Chan_Logic_Num.Text = value.ToString();
                }
                /*    运行程序编号     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_RUN_PROG, 0, 0, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    Chan_Run_Prog.Text = value.ToString();
                }
                /*    刀具编号     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_TOOL_USE, 0, 0, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    Cur_Tool.Text = value.ToString();
                }
                /*    运行程序行     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_RUN_ROW, 0, 0, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32== 0)
                {
                    Chan_Run_Row.Text = value.ToString();
                }
                /*    编码程序行     */
                ret32 = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_DCD_ROW, 0, 0, ref value, CGlbFunc.clientNo[clientnum]);
                if (ret32 == 0)
                {
                    Chan_Dcd_Row.Text = value.ToString();
                }

                NC_num.Text = clientnum.ToString();
                Chan_num.Text = "0";    
                //else
                //{
                //    MessageBox.Show("IP" + CGlbFunc.ip_info[0] + "读取失败");
                //}

            }
            //else
            //{
            //    MessageBox.Show("IP" + CGlbFunc.ip_info[0] + "通信失败");
            //}
        }

        private void tim_DetailStatus_Tick(object sender, EventArgs e)
        {
            NC_Status_Update();
        }

        private void NC_Status_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            tim_DetailStatus.Enabled = false;
        }

       

 

       

        //private void AxisNumKeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar != '\b')//这是允许输入退格键
        //    {
        //        if ((e.KeyChar < '0') || (e.KeyChar > '3'))//这是允许输入0-9数字
        //        {
        //            e.Handled = true;
        //        }
        //    }

        //}

       


       
    }
}
