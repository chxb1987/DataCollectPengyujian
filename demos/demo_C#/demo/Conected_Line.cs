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
    public partial class Conected_Line : Form
    {
        public Conected_Line() 
        {
            InitializeComponent();
            
            int i = 0;
            for (i = 0; i < CGlbFunc.SumOfClient; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "NC" + (i+1);
                lvi.SubItems.Add(CGlbFunc.ip_info[i]);
                lvi.SubItems.Add(CGlbFunc.clientNo[i].ToString());
                listView2.Items.Add(lvi);
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   
    }
}
