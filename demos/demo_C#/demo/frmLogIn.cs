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
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
           InitializeComponent();
           pictureBox1.Image = demo.Properties.Resources.buaa;
        }

        private void bntOK_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                MessageBox.Show("用户名不能为空！", "提示");
                txtUser.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("密码不能为空！", "提示");
                txtPassword.Focus();
                return;
            }
            Userclass tbClass = new Userclass();
            tbClass.strUserEng = txtUser.Text;
            tbClass.strPasword = txtPassword.Text;
            if (tbClass.tbUserLogIn(tbClass) == 1)
            {
                FormMain frman = new FormMain();
                frman.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("登录失败！", "提示");
                txtPassword.Text = "";
                txtUser.Text = "";
            }
        }

        private void bntEsce_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
