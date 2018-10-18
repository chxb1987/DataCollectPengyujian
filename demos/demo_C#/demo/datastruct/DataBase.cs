
#define MARCO_Win7  //用于win7笔记本调试开关
#undef MARCO_Win7   //使用XP工控机
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;


namespace demo
{
    public class DataBase
    {
        #region

        public  MySqlConnection My_Conn;
        //public  string M_str_sqlcon = "Server=.;Database=DNCDATA_TEST;Trusted_Connection=SSPI";
#if (MARCO_Win7)
        public string M_str_sqlcon = "Data Source=127.0.0.1;Database=db_data;User ID=root;Password=";
#else
        public string M_str_sqlcon = "Data Source=127.0.0.1;Database=db_data;User ID=root;Password=00000000";
#endif
        #endregion

        /*建立数据库连接*/
        public MySqlConnection getcon()
        {
            My_Conn = new MySqlConnection(M_str_sqlcon);
            My_Conn.Open();
            return My_Conn;
        }
        /*测试数据库是否连接正常*/
        public void con_open()
        {
            getcon();
        }
        /*关闭数据库连接*/
        public void con_close()
        {
            if (My_Conn.State == ConnectionState.Open)
            {
                My_Conn.Close();
                My_Conn.Dispose();
            }
        }

        public MySqlDataReader Select( string selectSql)
        {
            //getcon();

            My_Conn = new MySqlConnection(M_str_sqlcon);
            My_Conn.Open();
            MySqlCommand cmd = new MySqlCommand(selectSql, My_Conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //reader.Close();
                //sqlcon.Close();
                return reader;
            }
            return null;
        }

        public void Insert(MySqlConnection conn, string insertSql)
        {
            MySqlCommand cmd = new MySqlCommand(insertSql, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void Update(MySqlConnection conn, string updateSql)
        {

            MySqlCommand cmd = new MySqlCommand(updateSql, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void Delete(MySqlConnection conn, string deleteSql)
        {
            MySqlCommand cmd = new MySqlCommand(deleteSql, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        /*创建 DataSet对象*/
        public DataSet getDataSet(string SQLstr, string tableName)
        {
            getcon();
            MySqlDataAdapter SQLda = new MySqlDataAdapter(SQLstr, My_Conn);
            DataSet My_DataSet = new DataSet();
            SQLda.Fill(My_DataSet, tableName);       //通过方法Fill()将数据表信息添加到 DataSet对象中
            con_close();
            return My_DataSet;
        }


        /// <summary>
        /// 模型对象组装类
        /// </summary>
        public class Fabricate
        {
            /// <summary>
            /// 判断某列是否存在并且有无数据
            /// </summary>
            /// <param name="table"></param>
            /// <param name="reader"></param>
            /// <param name="columnName"></param>
            /// <returns></returns>
            public static bool ReaderExists(System.Collections.Hashtable table, MySqlDataReader reader, string columnName)
            {
                if (table.Contains(columnName.ToLower()) && !Convert.IsDBNull(reader[columnName]))
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 组装一个模型对象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="reader"></param>
            /// <param name="table"></param>
            /// <returns></returns>
            public static T Fill<T>(MySqlDataReader reader, System.Collections.Hashtable table)
            {
                T t = System.Activator.CreateInstance<T>();

                if (table == null || table.Count == 0)
                {
                    table = FillTable(reader);
                }

                System.Reflection.PropertyInfo[] propertys = typeof(T).GetProperties();

                foreach (System.Reflection.PropertyInfo item in propertys)
                {
                    if (ReaderExists(table, reader, item.Name))
                    {
                        try
                        {
                            item.SetValue(t, Convert.ChangeType(reader[item.Name], item.PropertyType), null);
                        }
                        catch
                        {
                            item.SetValue(t, Enum.Parse(item.PropertyType, Convert.ToString(reader[item.Name])), null);
                        }
                    }
                }

                return t;
            }
            /// <summary>
            /// 组装一个模型对象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="reader"></param>
            /// <returns></returns>
            public static T Fill<T>(MySqlDataReader reader)
            {
                if (reader != null && !reader.IsClosed && reader.HasRows && reader.Read())
                {
                    return Fill<T>(reader, null);
                }
                else
                {
                    return default(T);//System.Activator.CreateInstance<T>();
                }

            }
            /// <summary>
            /// 获取模型对象集合
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="reader"></param>
            /// <returns></returns>
            public static List<T> FillList<T>(MySqlDataReader reader)
            {
                List<T> list = new List<T>();
                if (reader != null && !reader.IsClosed && reader.HasRows)
                {
                    System.Collections.Hashtable table = FillTable(reader);
                    while (reader.Read())
                    {
                        list.Add(Fill<T>(reader, table));
                    }
                    reader.Close();
                }

                return list;
            }
            /// <summary>
            /// 获取reader中列名集合
            /// </summary>
            /// <param name="reader"></param>
            /// <returns></returns>
            public static System.Collections.Hashtable FillTable(MySqlDataReader reader)
            {
                System.Collections.Hashtable table = new System.Collections.Hashtable();

                table = new System.Collections.Hashtable();
                int count = reader.FieldCount;
                for (int i = 0; i < count; i++)
                {
                    table.Add(reader.GetName(i).ToLower(), null);
                }

                return table;
            }
          
            
        }

          


        
    }
}

