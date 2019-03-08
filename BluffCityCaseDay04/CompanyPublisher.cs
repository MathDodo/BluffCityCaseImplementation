using Data;
using Setup.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BluffCityCaseDay04
{
    public class CompanyPublisher : SingletonBase<CompanyPublisher>, IReceiver<Tuple<string, Time>>
    {
        private Dictionary<Type, MessageQueue> _messageQueues;

        public string Name => "Company publisher";

        public MessageQueue this[Type targetType]
        {
            get
            {
                return _messageQueues[targetType];
            }
        }

        private CompanyPublisher()
        {
            _messageQueues = new Dictionary<Type, MessageQueue>();
        }

        void IReceiver<Tuple<string, Time>>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
        }

        public void ReceivingQueue(Type queueReceiveType, MessageQueue messageQueue)
        {
            _messageQueues.Add(queueReceiveType, messageQueue);
        }
    }
}