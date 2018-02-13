using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using HtmlAgilityPack;


namespace 信息抓取2
{
    /// <summary>
    /// 用于获取内容列表信息
    /// </summary>
    class GetTileInfo
    {

        /// <summary>获取对应时间段列表信息
        /// 获取对应时间段列表信息beta
        /// </summary>
        /// <param name="URL">对应列表地址</param>
        /// <param name="Xpath">对应的Xpath地址</param>
        ///  <param name="STag">地址对应的标签（一般都为a）</param>
        /// <param name="DataRegex">日期的正则表达式</param>
        /// <param name="StartData">开始日期</param>
        /// <param name="EndData">结束日期</param>
        /// <returns>对应的链接列表报文，以“~”分割</returns>
        public static string LinkArray(string htmlstr, string Xpath, string STag, string DataRegex, DateTime StartData, DateTime EndData)//链接列表获取，重载1，主要区别时间段和单个时间
        {
            string[] outstag = STag.Split('~');

            Regex re = new Regex(DataRegex);
            HtmlDocument htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(htmlstr);
            string val = "";
            HtmlNodeCollection h = htmldoc.DocumentNode.SelectNodes(Xpath);
            if (h != null)
            {
                foreach (HtmlNode div in h)
                {
                    //val += div.InnerHtml + "~~~~~~~~~~~~~~~~";
                    Match m = re.Match(div.InnerHtml);
                    if (m.Value != string.Empty && Convert.ToDateTime(m.Value) >= StartData && Convert.ToDateTime(m.Value) <= EndData)
                    {
                        HtmlDocument tmp = new HtmlDocument();
                        tmp.LoadHtml(div.InnerHtml);

                        val += tmp.DocumentNode.SelectSingleNode("//a").InnerText + ":" + tmp.DocumentNode.SelectSingleNode("//a").Attributes["href"].Value + "~";
                    }
                }
            }



            if (val != string.Empty)
                return val.Substring(0, val.Length - 1);
            else
                return "";



        }

        /// <summary>获取对应时间列表信息
        /// 获取对应时间列表信息
        /// </summary>
        /// <param name="URL">对应列表地址</param>
        /// <param name="Xpath">对应的Xpath地址</param>
        ///  <param name="STag">地址对应的标签（一般都为a）</param>
        /// <param name="DataRegex">日期的正则表达式</param>
        /// <param name="Data">对应符合日期</param>
        /// <returns>对应的链接列表报文，以“~”分割</returns>
        public static string LinkArray(string htmlstr, string Xpath, string STag, string DataRegex, DateTime Date)//链接列表获取，重载1，主要区别时间段和单个时间
        {
            string[] outstag = STag.Split('~');

            Regex re = new Regex(DataRegex);
            HtmlDocument htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(htmlstr);
            string val = "";
            HtmlNodeCollection h = htmldoc.DocumentNode.SelectNodes(Xpath);
            if (h != null)
            {
                foreach (HtmlNode div in h)
                {
                    //val += div.InnerHtml + "~~~~~~~~~~~~~~~~";
                    Match m = re.Match(div.InnerHtml);
                    if (m.Value != string.Empty && Convert.ToDateTime(m.Value) == Date)
                    {
                        HtmlDocument tmp = new HtmlDocument();
                        tmp.LoadHtml(div.InnerHtml);

                        val += tmp.DocumentNode.SelectSingleNode("//a").InnerText + ":" + tmp.DocumentNode.SelectSingleNode("//a").Attributes["href"].Value + "~";
                    }
                }
            }



            if (val != string.Empty)
                return val.Substring(0, val.Length - 1);
            else
                return "";

        }
    }
}
