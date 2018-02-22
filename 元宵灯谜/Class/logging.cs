using System;
using log4net.Repository;
using log4net.Config;
using log4net;
using System.IO;

[assembly:log4net.Config.XmlConfigurator(Watch = true)]
class Logging
{
    ILog log;
    public Logging()
    {       
       // log=LogManager.GetLogger(typeof())
    }


    public void Infolog(Type logtype , string LogStr)
    {
        log = LogManager.GetLogger(logtype);
        log.Info(LogStr);
    }

    public void InfoFormatlog(Type logtype, string LogStr)
    {
        log = LogManager.GetLogger(logtype);
        log.InfoFormat(LogStr);
    }

    public void Errorlog(Type logtype, string LogStr)
    {
        log = LogManager.GetLogger(logtype);
        log.Error(LogStr);
    }

    public void ErrorFormatlog(Type logtype, string LogStr)
    {
        log = LogManager.GetLogger(logtype);
        log.ErrorFormat(LogStr);
    }
}
