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
    public partial class IpEnter : Form
    {
        static public string ip = " ";
        static public ushort port = 21;
        public IpEnter()
        {
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_ipaddress.Text.Trim()) || string.IsNullOrWhiteSpace(textBox_port.Text.Trim()))
            {
                MessageBox.Show("请输入IP地址和端口号！");
                return;
            }
            ip = textBox_ipaddress.Text;
            port = Convert.ToUInt16(textBox_port.Text);
            DialogResult = DialogResult.OK;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBox_ipaddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ip = textBox_ipaddress.Text;
                DialogResult = DialogResult.OK;
            }
        }

    }
}
