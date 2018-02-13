using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace 信息抓取2
{
    /// <summary>
    /// 该类就用于HTML代码抓取工作
    /// </summary>
    class GetHtmlStr
    {
        /// <summary>获取非指定网站目标链接网页代码
        /// 获取目标链接网页代码
        /// </summary>
        /// <param name="weblink">网页链接</param>
        /// <returns>整个网页Html代码</returns>
        public static string GetHtmlString(string weblink)
        {
            WebClient webC = new WebClient();
            webC.Credentials = CredentialCache.DefaultCredentials;
            Byte[] pagedata = webC.DownloadData(weblink);
            string pagehtml = Encoding.Default.GetString(pagedata);
            if (pagehtml.IndexOf("charset=utf-8") > 0)
                pagehtml = Encoding.UTF8.GetString(pagedata);
            else
                pagehtml = Encoding.Default.GetString(pagedata);
            return pagehtml;
        }
    }
}
