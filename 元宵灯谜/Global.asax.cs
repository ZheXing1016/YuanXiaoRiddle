using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using log4net.Repository;
using log4net.Config;
using log4net;
using System.IO;

namespace 元宵灯谜
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //系统启动时候，自动配置logconfig
            XmlConfigurator.Configure();
            yuanxiao.Initilazition.Init();
            //ILoggerRepository LogRepository = LogManager.CreateRepository("YuanxiaoRepository");
            //XmlConfigurator.Configure(LogRepository, new FileInfo("log4net.config"));
            //Logging.log = LogManager.GetLogger(LogRepository.Name, "NETCorelog4net");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}