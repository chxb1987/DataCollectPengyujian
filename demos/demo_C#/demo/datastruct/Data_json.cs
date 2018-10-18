using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo
{
    public class Data_json
    {
        private Byte datatype;   // 数据类型  1表示注册信息数据，2表示报警信息数据，3表示运行信息数据，4表示登录/登出信息数据
        public Byte did
        {
            get { return datatype; }
            set { datatype = value; }
        }
        private string JsonData;  // JsonData 
        public string dt
        {
            get { return JsonData; }
            set { JsonData = value; }
        }   
    

   }


}
