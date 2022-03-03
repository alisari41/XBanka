using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using log4net;
using log4net.Repository;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class LoggerServiceBase
    {//Log işlemini yapacak sınıf
        private ILog _log;
        public LoggerServiceBase(string name)//Name = Veri tabanı mı yoksa file logger gibi mi onu belirmek için
        {
            XmlDocument xmlDocument = new XmlDocument(); //config dosyası okunması
            xmlDocument.Load(File.OpenRead("log4net.config"));

            ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);

            _log = LogManager.GetLogger(loggerRepository.Name,name);//gönderilen congid adına göre al

        }
        //istediğim zaman Info logu yada Fatal logu almayabilirim
        public bool IsInfoEnabled => _log.IsInfoEnabled;
        public bool IsDebugEnabled => _log.IsDebugEnabled;
        public bool IsWarnEnabled => _log.IsWarnEnabled;//Uyarı
        public bool IsFatalEnabled => _log.IsFatalEnabled;
        public bool IsErrorEnabled => _log.IsErrorEnabled;

        public void Info(object logMessage)
        {
            if (IsInfoEnabled)
            {
                _log.Info(logMessage);
            }
        }
        public void Debug(object logMessage)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(logMessage);
            }
        }
        public void Warn(object logMessage)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(logMessage);
            }
        }
        public void Fatal(object logMessage)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(logMessage);
            }
        }
        public void Error(object logMessage)
        {
            if (IsErrorEnabled)
            {
                _log.Error(logMessage);
            }
        }
    }
}
