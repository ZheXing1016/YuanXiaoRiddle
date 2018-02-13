using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 信息抓取2
{
    /// <summary>
    /// 整合性操作，将所有组件整合成一个流程操作。
    /// </summary>
    class comComent
    {
        XmlDocument Storexml;
        public comComent()
        {
            Storexml = new XmlDocument();
            Storexml.Load(XmlController._StoreXmlFileName);
        }
        /// <summary>
        /// 生成XML导入模式的生成XML工作
        /// </summary>
        /// <param name="Webname">对应网站（StoreXml.xml所保存的第二级节点名称）</param>
        /// <param name="startdate">开始（对应）日期</param>
        /// <param name="enddate">结束日期（若结束日期小于1990-01-01，则表示为单日期模式）</param>
        /// <returns> XmlDocument</returns>
        public  XmlDocument makeXmlOutPutModel(string Webname,DateTime startdate,DateTime enddate)
        {
            DateTime enddateLowest=Convert.ToDateTime("1990-01-01");//enddate低于该日期的判定为单日期模式           
            XmlController xmlc=new XmlController();
            string Modelname=xmlc.GetXmlInnertextByNodeName(Storexml, "", Webname, "模板文件名");
            XmlDocument OutPutXmlFile=new XmlDocument();
            OutPutXmlFile.Load(fm_main.xmlSvaePath + Modelname + ".xml");

            string Url = xmlc.GetXmlInnertextByNodeName(Storexml, "", Webname, "域名");//生成的标题链接为相对地址，前面需要加上域名形成绝对地址
            string Urls = xmlc.GetXmlInnertextByNodeName(Storexml, "", Webname, "列表信息页", "链接");
            string Xpath = xmlc.GetXmlInnertextByNodeName(Storexml, "", Webname, "列表信息页", "规则");           
            string Stag = xmlc.GetXmlInnertextByNodeName(Storexml, "", Webname, "列表信息页", "对应获取内容标签");
            string datereg = xmlc.GetXmlInnertextByNodeName(Storexml, "", Webname, "列表信息页", "日期正则格式");

            string regularstr = xmlc.GetXmlInnertextByNodeName(Storexml, "", Webname, "文件内容页", "规则列");
            string regularstrBackup = xmlc.GetXmlInnertextByNodeName(Storexml, "", Webname, "文件内容页", "规则备用列");
            string ModelXmlPath = xmlc.GetXmlInnertextByNodeName(Storexml, "", Webname, "文件内容页", "生成模板名");
            string ModelNodeName = xmlc.GetXmlInnertextByNodeName(Storexml, "", Webname, "文件内容页", "模板节点名");
            string OutPutFliePath = fm_main.xmlSvaePath + ModelXmlPath + ".xml";
            string[] weblinks = Urls.Split('~');
           
            foreach (string weblink in weblinks)
            { //1.根据设定网站的循环开始
                //2.根据页面取链接开始
                string htmlstr = GetHtmlStr.GetHtmlString(weblink);
                string titlesstr = "";   
                if(enddate<enddateLowest)
                {
                    titlesstr = GetTileInfo.LinkArray(htmlstr, Xpath, Stag, datereg, startdate);
                }
                else
                {
                    titlesstr = GetTileInfo.LinkArray(htmlstr, Xpath, Stag, datereg, startdate,enddate);
                }
                //2.取链接结束

                if (titlesstr != string.Empty)
                {
               
                  string[] titlelinks = titlesstr.Split('~');//都是相对地址
              
                   foreach(string titlelink in titlelinks)
                   {  //3.根据标题链接取对应元素循环开始
                       htmlstr = GetHtmlStr.GetHtmlString(Url+titlelink);
                       string[] regulars = regularstr.Split('`');//规则列各个规则的分隔符，最大外圈
                       string[] regularBackups = regularstrBackup.Split('`');
                       string[] Foctors = new string[regulars.Length];//用于存放组合好的，对应用于生成XMLstr的数组
                       for (int i = 0; i < regulars.Length;i++ )
                       { //4.根据获取的规则组织生成循环开始

                           string[] regularinner = regulars[i].Split(':');//第二圈，用于分割字段名和规则内容
                           string getstr = getmethod.GetMothod(htmlstr, regularinner[1]);
                           if(getstr==string.Empty)
                           {
                               regularinner = regularBackups[i].Split(':');
                               getstr = getmethod.GetMothod(htmlstr, regularinner[1]);
                           }
                           Foctors[i] = regularinner[0] + "~" +  getstr ;//生成数组根据Xmlcontroller的xmlmake规则，对应规则+“~”+对应内容
                       }//4.根据获取的规则组织生成循环结束
                       OutPutXmlFile = xmlc.xmlmake(OutPutXmlFile, ModelNodeName, Foctors);
                    }
                 

                 } //3.根据标题链接取对应元素循环结束

               
               
               
            }//1.根据设定网站的循环结束
            xmlc.deletemodelnodename();
            return OutPutXmlFile;
           
        }
    }
}
