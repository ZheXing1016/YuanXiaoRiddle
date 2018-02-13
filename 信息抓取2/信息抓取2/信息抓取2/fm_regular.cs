using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 信息抓取2
{
    public partial class fm_regular : Form
    {
        fm_main fm;
        //新增应该如何处理？

        public fm_regular(fm_main fm)
        {           
            InitializeComponent();
            this.fm = fm;
        }
        static string fullpath = "";
        private void RecursionTreeControl(XmlNode xmlNode, TreeNodeCollection nodes) 
        {           
            foreach (XmlNode node in xmlNode.ChildNodes)//循环遍历当前元素的子元素集合     
            {
                if (node.Name != "#text" && node.Name != "#comment")
                {
                    TreeNode new_child = new TreeNode();//定义一个TreeNode节点对象     
                    new_child.Name = node.InnerText;
                    new_child.Text = node.Name;
                    nodes.Add(new_child);//向当前TreeNodeCollection集合中添加当前节点  
                    RecursionTreeControl(node, new_child.Nodes);//调用本方法进行递归    
                }
            }     
        }
        private void fm_regular_Load(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(XmlController._StoreXmlFileName);
            RecursionTreeControl(xml, treeView1.Nodes);
            treeView1.ExpandAll();
        }

        private void fm_regular_FormClosed(object sender, FormClosedEventArgs e)
        {
            fm.Enabled = true;
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!treeView1.SelectedNode.IsExpanded)
            {
                tb_val.Text = treeView1.SelectedNode.Name;
                fullpath = treeView1.SelectedNode.FullPath;
            }          
         
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            XmlController xmlc = new XmlController();
            string[] path = fullpath.Split('\\');
            xmlc.ModifyXmlInnertextByNodeName(path, tb_val.Text,XmlController._StoreXmlFileName);
            treeView1.SelectedNode.Name = tb_val.Text;
            MessageBox.Show("保存成功");
        }
    }
}
