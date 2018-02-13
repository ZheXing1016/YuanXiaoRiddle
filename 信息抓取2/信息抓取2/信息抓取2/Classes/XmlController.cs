using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 信息抓取2
{
    class XmlController
    {
        /// <summary>
        /// 保存策略的XML地址，用于修改、读取等操作。
        /// </summary>
        public static string _StoreXmlFileName = "";
        /// <summary>
        /// 根据节点名返回改节点的InnerText
        /// </summary>
        /// <param name="loadname">载入的文件名</param>
        /// <param name="Nodename">除根节点外的各级节点，按次序传入（根节点需传入，但会被排除，可以随意传入）</param>
        /// <returns>对应节点的Innertext</returns>
        public string GetXmlInnertextByNodeName(string loadname,params string[] Nodename)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(loadname);
            XmlNode tmp = xml.DocumentElement;           
            for (int i = 1; i < Nodename.Length; i++)
            {
                tmp = tmp.SelectSingleNode(Nodename[i]);
            }

            return tmp.InnerText;
        }

        /// <summary>
        /// 根据节点名返回改节点的InnerText
        /// </summary>
        /// <param name="xmlD">载入的xml文档</param>
        /// <param name="Nodename">除根节点外的各级节点，按次序传入（根节点需传入，但会被排除，可以随意传入）</param>
        /// <returns>对应节点的Innertext</returns>
        public string GetXmlInnertextByNodeName(XmlDocument xmlD, params string[] Nodename)
        {
            XmlDocument xml = xmlD;            
            XmlNode tmp = xml.DocumentElement;
            for (int i = 1; i < Nodename.Length; i++)
            {
                tmp = tmp.SelectSingleNode(Nodename[i]);
            }

            return tmp.InnerText;
        }
        /// <summary>
        /// 根据节点名返回改节点的InnerXml
        /// </summary>
        /// <param name="Nodename">除根节点外的各级节点，按次序传入（根节点需传入）</param>
        /// <returns>对应节点的innerXml</returns>
        public string GetXmlInnerxmlByNodeName(params string[] Nodename)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(_StoreXmlFileName);
            XmlNode root = xml.DocumentElement;
            XmlNode tmp = root;
            for (int i = 1; i < Nodename.Length; i++)
            {
                tmp = tmp.SelectSingleNode(Nodename[i]);
            }

            return tmp.InnerText;
        }
        /// <summary>
        /// 根据对应路径修改对应节点的内容
        /// </summary>
        /// <param name="NodeName">路径</param>
        /// <param name="ModifyVal">需要修改的值</param>
        /// <param name="savepath">打开并修改后文件保存地址</param>
        public void ModifyXmlInnertextByNodeName(string[] NodeName,string ModifyVal,string savepath)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(savepath);
            XmlNode root = xml.DocumentElement;
            XmlNode tmp = root;
            for (int i = 1; i < NodeName.Length; i++)
            {
                tmp = tmp.SelectSingleNode(NodeName[i]);
            }
            tmp.InnerText = ModifyVal;
            xml.Save(savepath);

        }

        private static XmlNode root = null;// 配合xmlmake使用，因为模板节点只有一个，不能循环中使用
        private static XmlNode modelnodename = null;// 配合xmlmake使用，因为模板节点只有一个，不能循环中使用   
        /// <summary>
        /// 根据模板生成XML文件
        /// </summary>
        /// <param name="ModleXmlPath">模板文件路径</param>
        /// <param name="ModelNodeName">模板节点名称</param>
        /// <param name="Factor">插入节点内容，格式为：节点名称~节点内容</param>
        /// <returns></returns>
        public XmlDocument xmlmake(string ModelXmlPath, string ModelNodeName, params string[] Factor)
        {           
                XmlDocument xml = new XmlDocument();
                xml.Load(ModelXmlPath);
                root = xml.DocumentElement;
                modelnodename = root.SelectSingleNode(ModelNodeName);
                XmlNode newnode = modelnodename.CloneNode(true);//赋值节点
                foreach (string foctortmp in Factor)
                {
                    string[] tmp = foctortmp.Split('~');
                    newnode.SelectSingleNode(tmp[0]).InnerText = tmp[1];
                }
                root.AppendChild(newnode);
            
                
                return xml;

        }

        /// <summary>
        /// 根据模板生成XML文件
        /// </summary>
        /// <param name="xml">已导入模板文件的XmlDocument</param>
        /// <param name="ModelNodeName">模板节点名称</param>
        /// <param name="Factor">插入节点内容，格式为：节点名称~节点内容</param>
        /// <returns></returns>
        public XmlDocument xmlmake(XmlDocument xml, string ModelNodeName, params string[] Factor)
        {          
            root = xml.DocumentElement;
            modelnodename = root.SelectSingleNode(ModelNodeName);
            XmlNode newnode = modelnodename.CloneNode(true);//赋值节点
            
            foreach (string foctortmp in Factor)
            {
                string[] tmp = foctortmp.Split('~');
                XmlCDataSection datanode = xml.CreateCDataSection(tmp[1]);
                XmlNode oldnode = newnode.SelectSingleNode(tmp[0]);
                oldnode.AppendChild(datanode);
                
            }
            root.AppendChild(newnode);

            return xml;

        }
        /// <summary>
        /// 配合xmlmake使用，因为模板节点只有一个，不能循环中使用
        /// </summary>
        public void deletemodelnodename()
        {
            root.RemoveChild(modelnodename);
        }
    }
}
