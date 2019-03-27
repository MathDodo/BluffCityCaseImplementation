using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BluffCityCaseDay08
{
    public class AirportCargoLoader : SingletonBase<AirportCargoLoader>, IReceiver<AirportCargoData>
    {
        private Dictionary<Type, MessageQueue> _queues;

        public string Name => "CargoLoader";

        public MessageQueue this[Type targetType]
        {
            get
            {
                return _queues[targetType];
            }
        }

        private AirportCargoLoader()
        {
            _queues = new Dictionary<Type, MessageQueue>();
        }

        public void OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            mq.BeginReceive();
        }

        public void ReceivingQueue(Type queueReceiveType, MessageQueue messageQueue)
        {
            _queues.Add(queueReceiveType, messageQueue);
        }
    }
}