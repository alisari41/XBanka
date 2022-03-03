using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    [Serializable]
    public class SerializableLogEvent
    {//Loglıcamız datanın  kendisi
        private LoggingEvent _loggingEvent;

        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }

        //Loglama datasının içerisine ne koymak istyorsak onları koyuyoruz
        //Buraya User name de koyabiliriz
        public object Message => _loggingEvent.MessageObject;//Loglama datası için oluşturduğumuz mesaj
    }
}
