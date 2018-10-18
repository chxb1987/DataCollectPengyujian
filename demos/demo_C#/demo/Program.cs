using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text;

namespace demo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            string constr = "DSN=mysql_0518;" + "UID=BOOKSQL;" + "PWD=BOOKSQLPW;";
            OdbcConnection conn = new OdbcConnection(constr);
            conn.Open();
            //string insert = "insert into tennis.teams values(59, 59, 'lala')";
            //string select = "select * from tennis.players";
            //string update = "update test.test set name='whwang' where id = 11";
            string delete = "delete from tennis.teams where TEAMNO = 59";
            DB db = new DB();
            //db.Insert(conn, insert);
            //db.Select(conn, select);
            //db.Update(conn, update);
            db.Delete(conn, delete);
            conn.Close();
            Console.Read();
           */
            /*  测试Queue队列  20161220
            CSeqQueue<int> queue = new CSeqQueue<int>(5);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            Console.WriteLine(queue);
            */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new frmLogIn());
            ///Application.Run(new frmRealGraph());
        }
    }
}
