using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace demo
{
    class WebSreverce_PostJson
    {
        public static string urladd_local = "http://192.168.0.113:8080/pushData/";  //使用局域网带宽
        public static string urladd_inn = "http://192.168.188.166:7000/DataInputServiceTest/pushData";  //使用局域网带宽
        public static string urladd_inn_format = "http://192.168.188.166:7000/DataInputServiceTest/format";  //使用局域网带宽
        public static string urladd_outn = "http://111.198.20.237:7070/DataInputServiceTest/pushData";
        public class Data_post
        {
            private Byte datatype;   // 数据类型  1表示注册信息数据，2表示报警信息数据，3表示运行信息数据，4表示登录/登出信息数据
            public Byte bdatatype
            {
                get { return datatype;  }
                set { datatype = value; }
            }
            private string JsonData;  // JsonData 
            public string strJsonData
            {
                get { return JsonData;  }
                set { JsonData = value; }
            }   
        }

        public static String ConvertToJson(Object m)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss.ffff");

            string jsonData = JsonConvert.SerializeObject(m, Newtonsoft.Json.Formatting.None, timeConverter);
            return jsonData;
        }


        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }

        public static String Post_Jsonstr(string Url, String Paras1)
        {
            //string strURL = Conf.ServiceURL + "/" + methodName;
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Timeout =500;
            request.ReadWriteTimeout = 500;
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            //将字符串参数进行Url编码
            string paraUrlCoded1 =System.Web.HttpUtility.UrlEncode(Paras1);
            //string paraUrlCoded1 = Paras1;
            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes("data=" + paraUrlCoded1);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流  
            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
                return "break";
            }
            //将请求参数写入流
            if (writer != null)
            {
                writer.Write(payload, 0, payload.Length);
                writer.Close();//关闭请求流
            }
            String strValue = "";//strValue为http响应所返回的字符流
            HttpWebResponse response;

            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
               // return "break";  //这一句在使用format检查接口的时候，注释掉
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;

            }
            if (response != null)
            {
                Stream s = response.GetResponseStream();
                try
                {
                    StreamReader sr = new StreamReader(s, Encoding.UTF8);
                    strValue = sr.ReadToEnd();
                    //XmlTextReader Reader = new XmlTextReader(s);
                    //Reader.MoveToContent();
                    //strValue = Reader.ReadInnerXml();//取出Content中的数据
                    //Reader.Close();
                    //s.Close();
                }
                catch (Exception ex)
                {
                    //Console.Write(ex.Message);

                }
            }
            //服务器端返回的是一个XML格式的字符串，XML的Content才是我们所需要的Json数据

            if (request != null)
            {
                request.Abort();
            }
            if (response != null)
            {
                response.Close();
            }
           

            return strValue;//调用的webservice的返回值
        }

    }
}
