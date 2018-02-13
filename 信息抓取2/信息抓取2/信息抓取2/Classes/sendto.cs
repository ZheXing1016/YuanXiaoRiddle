using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Net;

namespace 信息抓取2
{
    enum XmlNodeNames
    {
        title,
        text,
        source,
        datetime,
        column,
        columnid,

    };
    class sendto
    {
        private XmlDocument _ModuleXmlFile;
        public sendto(XmlDocument ModuleXmlFile)
        {
            _ModuleXmlFile = ModuleXmlFile;
        }
         /// <summary>
         /// 返回用于推送的XML字符串
         /// </summary>
         /// <param name="title">文章标题</param>
         /// <param name="context">正文</param>
         /// <param name="fromname">来源</param>
         /// <param name="pagedate">发布日期</param>
         /// <param name="Ptoname">放置栏目的名称</param>
         /// <param name="Ptonum">放置栏目的代码</param>
         /// <returns>用于推送的字符串</returns>
        public string xmlstr( string title, string context, string fromname, string pagedate,string Ptoname,string Ptonum)
        {


            XmlElement root1 = _ModuleXmlFile.DocumentElement;   
            XmlNode root=root1.SelectSingleNode("article");
            //XmlNode tmpnode = root.SelectSingleNode("title");
            //tmpnode.InnerText = title;
            //tmpnode = root.SelectSingleNode("text");
            //tmpnode.InnerText = context;
            //tmpnode = root.SelectSingleNode("source");
            //tmpnode.InnerText = fromname;
            //tmpnode = root.SelectSingleNode("datetime");
            //tmpnode.InnerText = pagedate;
            //tmpnode = root.SelectSingleNode("column");
            //tmpnode.InnerText = Ptoname;
            //tmpnode = root.SelectSingleNode("columnid");
            //tmpnode.InnerText = Ptonum;
            XmlNode tmpnode = root.SelectSingleNode("标题");
            tmpnode.InnerText = title;
            tmpnode = root.SelectSingleNode("正文");
            tmpnode.InnerText = context;
            tmpnode = root.SelectSingleNode("文号");
            tmpnode.InnerText = fromname;
            tmpnode = root.SelectSingleNode("创建日期");
            tmpnode.InnerText = pagedate;
            tmpnode = root.SelectSingleNode("column");
            tmpnode.InnerText = Ptoname;
            tmpnode = root.SelectSingleNode("columnid");
            tmpnode.InnerText = Ptonum;
            return _ModuleXmlFile.InnerXml;
        }

        private string RequestData(string POSTURL, string PostData)
        {
            //发送请求的数据
            WebRequest myHttpWebRequest = WebRequest.Create(POSTURL);
            myHttpWebRequest.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(PostData);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = byte1.Length;
            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //发送成功后接收返回的XML信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }
        public string SendToSystem(string xmlstr)
        {
            string username = "dqxxzq";
            string pwd = "8071340@xxzx";
            string posturl = "http://jcms.deqing.gov.cn/jcms/interface/receive.jsp?userid=" + username + "&password=" + pwd + "&charset=UTF-8 ";
            return RequestData(posturl, xmlstr);
        }
      

       

    }
}
