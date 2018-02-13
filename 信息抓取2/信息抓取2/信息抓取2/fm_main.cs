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
    public partial class fm_main : Form
    {
        public static string xmlSvaePath;//XML文件存储文件夹
        public fm_main()
        {
            //12
            InitializeComponent();
        }
        string[] Holdpath = { "root", "HoldList" };
        string[] dopath = { "root", "DoList" };
       
        private void reflashlb()
        {
            string customerstr = xmlSvaePath + "customer.xml";//用户自定义存储XML
            XmlController xmlc = new XmlController();
            string HoldList = xmlc.GetXmlInnertextByNodeName(customerstr, Holdpath);
            string DoList = xmlc.GetXmlInnertextByNodeName(customerstr, dopath);
            if (HoldList != string.Empty)
            {
                string[] tmp = HoldList.Split('~');
                for (int i = 0; i < tmp.Length; i++)
                {
                    lb_hold.Items.Add(tmp[i]);
                }
            }
            if (DoList != string.Empty)
            {
                string[] tmp = DoList.Split('~');
                for (int i = 0; i < tmp.Length; i++)
                {
                    lb_do.Items.Add(tmp[i]);
                }
            }
            DataTable dt = new DataTable();
            //如果出现新增，customer中没有，如何解决？
            dt.Rows.Add();
            dt.Columns.Add();
            //for(int i =0;i<lb_do.Items.Count)
        }

        private void work(string Webname,string savepath)
        {
            XmlController xmlc = new XmlController();
            string makepetten = xmlc.GetXmlInnertextByNodeName(XmlController._StoreXmlFileName, "", Webname, "生成方式");
            XmlDocument xml=new XmlDocument();
            DateTime enddt = Convert.ToDateTime("1987-12-22");    
            if(!ck_DataForm.Checked)
            {
                enddt = dt_EndData.Value;
            }//用于区分单日期模式还是多日期模式，单日期模式要求enddate小于1990-01-01
            comComent cc=new comComent();
            xml = cc.makeXmlOutPutModel(Webname, dt_StartData.Value, enddt);
            switch (makepetten)
            {
                case"0":
                    break;
                case"1":
                    xml.Save(savepath);
                    break;
                default:
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            xmlSvaePath = Application.StartupPath + @"\Xmls\";
            XmlController._StoreXmlFileName = xmlSvaePath + "StoreXml.xml";//存储规则XML
            reflashlb();
          
        }

        private void ck_DataForm_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_DataForm.Checked)
            {
                dt_EndData.Enabled = false;                
            }
            else
            {
                dt_EndData.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fm_regular fm = new fm_regular(this);
            fm.Show();
            this.Enabled = false;
        }

        private void fm_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //结束的时候，把选中的规则存入到customer.xml中
            string HoldList = "";            
            for(int i=0;i<lb_hold.Items.Count;i++)
            {
                HoldList += lb_hold.Items[i].ToString() + "~";
            }
            if (HoldList != string.Empty)
            {
                HoldList = HoldList.Substring(0, HoldList.Length - 1);
            }

            string DoList = "";
            for (int i = 0; i < lb_do.Items.Count; i++)
            {
                DoList += lb_do.Items[i].ToString() + "~";
            }
            if (DoList != string.Empty)
            {
                DoList = DoList.Substring(0, DoList.Length - 1);
            }
           
            string savepath=xmlSvaePath+"customer.xml";
            XmlController xmlc = new XmlController();
            xmlc.ModifyXmlInnertextByNodeName(Holdpath, HoldList, savepath);
            xmlc.ModifyXmlInnertextByNodeName(dopath, DoList, savepath);
        }

        private void bt_go_Click(object sender, EventArgs e)
        {
            if (lb_hold.SelectedIndex > -1)
            {
                lb_do.Items.Add(lb_hold.SelectedItem);
                lb_hold.Items.Remove(lb_hold.SelectedItem);
            }
        }

        private void bt_goall_Click(object sender, EventArgs e)
        {
            int maxcount = lb_hold.Items.Count;
            for(int i=0;i<maxcount;i++)
            {
                lb_do.Items.Add(lb_hold.Items[0]);
                lb_hold.Items.RemoveAt(0);
            }
        }

        private void bt_back_Click(object sender, EventArgs e)
        {
            if (lb_do.SelectedIndex > -1)
            {
                lb_hold.Items.Add(lb_do.SelectedItem);
                lb_do.Items.Remove(lb_do.SelectedItem);
            }
        }

        private void bt_backall_Click(object sender, EventArgs e)
        {
            int maxcount = lb_do.Items.Count;
            for (int i = 0; i < maxcount; i++)
            {
                lb_hold.Items.Add(lb_do.Items[0]);
                lb_do.Items.RemoveAt(0);
            }
        }

        private void bt_do_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < lb_do.Items.Count; i++)
                {
                   
                   
                     string savepath = fbd.SelectedPath + @"\" + lb_do.Items[i].ToString() + ".xml";
                        
                     work(lb_do.Items[i].ToString(), savepath);                          
                       
                 }
             }
            MessageBox.Show("工作完毕！");
           
           
        }
    }
}
