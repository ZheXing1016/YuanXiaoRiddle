using System;
using log4net.Repository;
using log4net.Config;
using log4net;
using System.IO;


class logging
{
    static ILog log;
    public logging()
    {
        
    }


    public static void Infolog(string LogStr)
    {
        ILoggerRepository LogRepository = LogManager.CreateRepository("NETCoreRepository");
        XmlConfigurator.Configure(LogRepository, new FileInfo("log4net.config"));
        log = LogManager.GetLogger(LogRepository.Name, "NETCorelog4net");
        log.Info(LogStr);
    }

    public static void InfoFormatlog(string LogStr)
    {
        ILoggerRepository LogRepository = LogManager.CreateRepository("NETCoreRepository");
        XmlConfigurator.Configure(LogRepository, new FileInfo("log4net.config"));
        log = LogManager.GetLogger(LogRepository.Name, "NETCorelog4net");
        log.InfoFormat(LogStr);
    }

    public static void Errorlog(string LogStr)
    {
        ILoggerRepository LogRepository = LogManager.CreateRepository("NETCoreRepository");
        XmlConfigurator.Configure(LogRepository, new FileInfo("log4net.config"));
        log = LogManager.GetLogger(LogRepository.Name, "NETCorelog4net");
        log.Error(LogStr);
    }

    public static void ErrorFormatlog(string LogStr)
    {
        ILoggerRepository LogRepository = LogManager.CreateRepository("NETCoreRepository");
        XmlConfigurator.Configure(LogRepository, new FileInfo("log4net.config"));
        log = LogManager.GetLogger(LogRepository.Name, "NETCorelog4net");
        log.ErrorFormat(LogStr);
    }
}

class Logging
{
    public static logging Log;
}

