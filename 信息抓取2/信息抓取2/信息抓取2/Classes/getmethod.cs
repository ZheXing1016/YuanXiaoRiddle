using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace 信息抓取2
{
    class getmethod
    {
        /// <summary>
        /// 通过固定格式字符串传入，对Html字符串进行提取
        /// </summary>
        /// <param name="Ostr">需要提取内容的字符串</param>
        /// <param name="regular">固定格式字符串，/re后面跟的是正则表达式，/xp后面跟的是XPath，/rp后面跟替换字符，用|分割，各规则中间用~分割。例如/re</param>
        /// <returns>根据规则提取的字符串</returns>
        public static string GetMothod(string Ostr, string regular)
        {
            try
            {
                string val = Ostr;//每次进行提取后都重新赋值给予val
                if (val != string.Empty)
                {
                    string[] regulartmp = regular.Split('~');
                    foreach (string re in regulartmp)
                    {
                        if (re.Length >= 4)
                        {
                            string typesin = re.Substring(0, 3);//开头3个字符为类型标记
                            string context = re.Substring(3, re.Length - 3); //后面所有位数为具体规则内容
                            switch (typesin)
                            {
                                case "/re"://正则表达式的使用
                                    Regex getresulte = new Regex(context);
                                    //MatchCollection ma = getresulte.Matches(val);
                                    Match resulte = getresulte.Match(val);
                                    val = resulte.ToString();
                                    break;
                                case "/xp"://Xpath的使用
                                    HtmlDocument hd = new HtmlDocument();
                                    hd.LoadHtml(val);
                                    val = hd.DocumentNode.SelectSingleNode(context).InnerHtml;
                                    break;
                                case "/rp"://replace的使用
                                    string[] replacestr = context.Split('|');
                                    val = val.Replace(replacestr[0], replacestr[1]);
                                    break;
                                case "/rr"://正则表达式去除的使用
                                    val = Regex.Replace(val, context, string.Empty, RegexOptions.IgnoreCase);
                                    break;
                                case "/st"://直接获取文字信息str
                                    val = context;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                }
                return val;
            }
            catch
            {
                return "";
            }
        }
    }
}
