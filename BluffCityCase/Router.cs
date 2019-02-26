using Data;
using Setup;
using System;
using System.Messaging;
using System.Collections.Generic;

namespace BluffCityCaseDay02
{
    public sealed class Router : SingletonBase<Router>, IReceiver<Tuple<string, int>>
    {
        private List<AirlineCompany> _companies;
        private Dictionary<Type, MessageQueue> _queues;

        public string Name => "Router";

        public MessageQueue this[Type targetType]
        {
            get
            {
                return _queues[targetType];
            }
        }

        private Router()
        {
            _companies = new List<AirlineCompany>();
            _queues = new Dictionary<Type, MessageQueue>();
        }

        void IReceiver<Tuple<string, int>>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            var queue = _companies.Find(c => c.CompanyName == m.Label)[typeof(Tuple<string, int>)];

            MessageQueuesManager.Instance.SendMessage(m.Body, queue);

            mq.BeginReceive();
        }

        public void ReceivingQueue(Type queueReceiveType, MessageQueue messageQueue)
        {
            _queues.Add(queueReceiveType, messageQueue);
        }

        public void AddAirline(AirlineCompany company)
        {
            _companies.Add(company);
        }
    }
}