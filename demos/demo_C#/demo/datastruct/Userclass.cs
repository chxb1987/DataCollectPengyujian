using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace demo
{
    class Userclass
    {
        private string UserEng;      //登录名称
        public string strUserEng
        {
            get { return UserEng; }
            set { UserEng = value; }
        }
        private string Pasword;     //密码
        public string strPasword
        {
            get { return Pasword; }
            set { Pasword = value; }
        }

        public int tbUserLogIn(Userclass Customer)
        {
            DataBase tbuser = new DataBase();
            int intFalg = 0;
            try
            {
                //MySqlConnection sqlcon = addnc.getcon();
                string select = "select * from tbUser where username='" + Customer.strUserEng + "' and password='" + Customer.strPasword + "'";
                MySqlDataReader reader = null;
                tbuser.getcon();
                MySqlCommand cmd = new MySqlCommand(select, tbuser.My_Conn);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    intFalg = 1;
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
    }
}
