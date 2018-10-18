using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using NetServer.ProgressShow;
using HncNetDll;

namespace NetServer.Paraman
{
    public partial class UserControlParaman : UserControl
    {
        const string STR_NO_CONNECTION = "未连接";
        string[] STR_PARM = { "名称", "参数值", "生效方式", "默认值", "最小值", "最大值" };
        int[] columnHeadWidth = { 200, 100, 80, 100, 100, 100 };
        string[] STR_ACT = { "保存生效", "立即生效", "复位生效", "重启生效" };

        private bool isParmanRead = false;

        public UserControlParaman()
        {
            InitializeComponent();

            for (int i = 0; i < STR_PARM.Length; i++ )
            {
                listViewParm.Columns.Add(STR_PARM[i], columnHeadWidth[i]);
            } 
        }

        public void InitParamanTreeView()
        {
            if (isParmanRead)
            {
                return;
            }

            treeViewParaman.Nodes.Clear();

            short clientNo = CNetData.GetActiveClientNo();
            if (NetDll.HncNetIsConnect(clientNo) == 0)
            {
                treeViewParaman.Nodes.Add(new TreeNode(STR_NO_CONNECTION));
            }
            else
            {
                int ret = AddParamanItemFrmNet(clientNo);
                if (ret < 0)
                {
                    MessageBox.Show("网络读取参数结构错误！");
                }
            }
        }

        private int AddParamanItemFrmNet(short clientNo)
        {
            string tip = "正在读取参数结构";
            CProgressShow progress = new CProgressShow();
            progress.SetProgressTip(tip);
            progress.Show();
            progress.SetProgress(0);

            int fileNum = NetDll.HncParmanGetFileNum(clientNo);
            string fileName = "";
            int ret = 0;
            for (int i = 0; i < fileNum; i++)
            {
                ret = NetDll.HncParmanGetLibTitle(i, ref fileName, clientNo);
                if (ret < 0)
                {
                    break;
                }

                TreeNode rootNode = new TreeNode(fileName);
                treeViewParaman.Nodes.Add(rootNode);

                int recNum = NetDll.HncParmanGetRecNum(i, clientNo);
                if (recNum < 0)
                {
                    ret = recNum;
                    break;
                }
                else if (recNum > 1)
                {
                    string recName = "";
                    ret = NetDll.HncParmanGetRecTitle(i, ref recName, clientNo);
                    if (ret < 0)
                    {
                        break;
                    }

                    for (int j = 0; j < recNum; j++ )
                    {
                        rootNode.Nodes.Add(new TreeNode(recName + j.ToString()));
                    }
                }

                double hundred = 100;
                int progVal = Convert.ToInt32(Convert.ToDouble(i+1) / Convert.ToDouble(fileNum) * hundred);
                progress.SetProgress(progVal);
            }

            progress.ProgWndClose();
            isParmanRead = true;

            return ret;
        }

        private void treeViewParaman_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                return;
            }

            listViewParm.Items.Clear();

            int fileNo = 0;
            int recNo = 0;

            if (e.Node.Parent == null)
            {
                fileNo = e.Node.Index;
                recNo = 0;
            }
            else
            {
                fileNo = e.Node.Parent.Index;
                recNo = e.Node.Index;
            }

            short clientNo = CNetData.GetActiveClientNo();
            int ret = AddParItemFrmNet(fileNo, recNo, clientNo);
            if (ret < 0)
            {
                MessageBox.Show("网络读取参数出错！");
            }
        }

        private int AddParItemFrmNet(int fileNo, int recNo, short clientNo)
        {
            int parNum = NetDll.HncParmanGetParNum(fileNo, clientNo);
            if (parNum < 0)
            {
                return -1;
            }
            int ret = NetDll.HncParmanRewirteLib(fileNo, recNo, clientNo);
            if (ret < 0)
            {
                return -1;
            }

            string tip = "正在读取参数";
            CProgressShow progress = new CProgressShow();
            progress.SetProgressTip(tip);
            progress.Show();
            progress.SetProgress(0);

            string[] strPar = new string[6];
            for (int i = 0; i < parNum; i++)
            {
                int index = NetDll.HncParmanGetIndexFrmRow(fileNo, i, clientNo);
                if (index < 0)
                {
                    ret = -1;
                    break;
                }

                int parNo = NetDll.HncParmanGetNoFrmIndex(fileNo, index, clientNo);
                if (parNo < 0)
                {
                    ret = -1;
                    break;
                }

                //获取生效方式
                sbyte actType = NetDll.HncParmanGetItemActType(fileNo, parNo, clientNo);
                if (actType < 0)
                {
                    ret = -1;
                    break;
                }

                if (actType == (sbyte)CDataDef.PAR_ACT_TYPE.PARA_ACT_HIDE)
                {
                    continue;
                }
                switch (actType)
                {
                    case (sbyte)CDataDef.PAR_ACT_TYPE.PARA_ACT_SAVE:
                        strPar[2] = STR_ACT[0];
                        break;
                    case (sbyte)CDataDef.PAR_ACT_TYPE.PARA_ACT_NOW:
                        strPar[2] = STR_ACT[1];
                        break;
                    case (sbyte)CDataDef.PAR_ACT_TYPE.PARA_ACT_RST:
                        strPar[2] = STR_ACT[2];
                        break;
                    case (sbyte)CDataDef.PAR_ACT_TYPE.PARA_ACT_PWR:
                        strPar[2] = STR_ACT[3];
                        break;
                }

                //获取参数名称
                ret = NetDll.HncParmanGetItemName(fileNo, parNo, ref strPar[0], clientNo);
                if (ret < 0)
                {
                    break;
                }

                //获取参数储存类型
                sbyte storeType = NetDll.HncParmanGetItemStoreType(fileNo, parNo, clientNo);
                if (storeType < 0)
                {
                    ret = -1;
                    break;
                }

                //获取参数值、默认值、最小值和最大值
                int iVal = 0;
                double dVal = 0;
                const int DFT = 1;
                const int MIN = 2;
                const int MAX = 3;
                switch (storeType)
                {
                    case (sbyte)CDataDef.PAR_STORE_TYPE.TYPE_BOOL:
                    case (sbyte)CDataDef.PAR_STORE_TYPE.TYPE_UINT1:
                    case (sbyte)CDataDef.PAR_STORE_TYPE.TYPE_INT1:
                    case (sbyte)CDataDef.PAR_STORE_TYPE.TYPE_UINT2:
                    case (sbyte)CDataDef.PAR_STORE_TYPE.TYPE_INT2:
                    case (sbyte)CDataDef.PAR_STORE_TYPE.TYPE_UINT4:
                    case (sbyte)CDataDef.PAR_STORE_TYPE.TYPE_INT4:
                        ret = NetDll.HncParGetIntVal(fileNo, recNo, index, ref iVal, clientNo);
                        if (ret < 0)
                        {
                            break;
                        }
                        strPar[1] = iVal.ToString();                       
                        ret = NetDll.HncParmanGetItemIntVal(DFT, fileNo, parNo, ref iVal, clientNo);
                        if (ret < 0)
                        {
                            break;
                        }
                        strPar[3] = iVal.ToString();
                        ret = NetDll.HncParmanGetItemIntVal(MIN, fileNo, parNo, ref iVal, clientNo);
                        if (ret < 0)
                        {
                            break;
                        }
                        strPar[4] = iVal.ToString();
                        ret = NetDll.HncParmanGetItemIntVal(MAX, fileNo, parNo, ref iVal, clientNo);
                        if (ret < 0)
                        {
                            break;
                        }
                        strPar[5] = iVal.ToString();  
                        break;
                    case (sbyte)CDataDef.PAR_STORE_TYPE.TYPE_REAL:
                        ret = NetDll.HncParGetDoubVal(fileNo, recNo, index, ref dVal, clientNo);
                        if (ret < 0)
                        {
                            break;
                        }
                        strPar[1] = dVal.ToString("F6");
                        ret = NetDll.HncParmanGetItemDoubVal(DFT, fileNo, parNo, ref dVal, clientNo);
                        if (ret < 0)
                        {
                            break;
                        }
                        strPar[3] = dVal.ToString("F6");
                        ret = NetDll.HncParmanGetItemDoubVal(MIN, fileNo, parNo, ref dVal, clientNo);
                        if (ret < 0)
                        {
                            break;
                        }
                        strPar[4] = dVal.ToString("F6");
                        ret = NetDll.HncParmanGetItemDoubVal(MAX, fileNo, parNo, ref dVal, clientNo);
                        if (ret < 0)
                        {
                            break;
                        }
                        strPar[5] = dVal.ToString("F6");
                        break;
                    case (sbyte)CDataDef.PAR_STORE_TYPE.TYPE_STRING:
                        ret = NetDll.HncParGetStrVal(fileNo, recNo, index, ref strPar[1], clientNo);
                        if (ret < 0)
                        {
                            break;
                        }
                        strPar[3] = "N/A";
                        strPar[4] = "N/A";
                        strPar[5] = "N/A";
                        break;                       
                    case (sbyte)CDataDef.PAR_STORE_TYPE.TYPE_HEX4:
                        ret = NetDll.HncParGetIntVal(fileNo, recNo, index, ref iVal, clientNo);
                        strPar[1] = iVal.ToString("X4");
                        strPar[3] = "N/A";
                        strPar[4] = "N/A";
                        strPar[5] = "N/A";
                        break;
                    //case TYPE_BYTE:
                    //    str.Format("0");
                    //    break;
                    default:
                        strPar[1] = "0";
                        break;
                }
                if (ret < 0)
                {
                    break;
                }
                listViewParm.Items.Add(new ListViewItem(strPar));

                double hundred = 100;
                int progVal = Convert.ToInt32(Convert.ToDouble(i+1) / Convert.ToDouble(parNum) * hundred);
                progress.SetProgress(progVal);
            }
            ret = 0;

            progress.ProgWndClose();

            return ret;
        }
    }
}
