using Data;
using System;
using System.Collections.Generic;
using System.Messaging;

namespace BluffCityCaseDay08
{
    public class CDMTranslator : SingletonBase<CDMTranslator>, IReceiver<SASData>, IReceiver<WowData>, IReceiver<EmiratesData>
    {
        private List<CDMData> _cdmData;
        private Dictionary<Type, MessageQueue> _queues;

        public string Name => "Translator";

        public MessageQueue this[Type targetType]
        {
            get
            {
                return _queues[targetType];
            }
        }

        public CDMData this[int index]
        {
            get
            {
                return _cdmData[index];
            }
        }

        public int Count { get { return _cdmData.Count; } }

        private CDMTranslator()
        {
            _cdmData = new List<CDMData>();
            _queues = new Dictionary<Type, MessageQueue>();
        }

        public void ReceivingQueue(Type queueReceiveType, MessageQueue messageQueue)
        {
            _queues.Add(queueReceiveType, messageQueue);
        }

        void IReceiver<SASData>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            SASData data = (SASData)m.Body;

            if (DateTime.TryParse(data.Time.ToString() + " " + data.Date.ToString(), out DateTime date))
            {
                CDMData cDMData = new CDMData(data.Airline, data.FlightNumber, data.Destintation, data.Origin, data.ArrivalDeparture, date);

                m.ResponseQueue.Formatter = JsonFormatter.Instance;

                if (m.Label == "Emirates")
                {
                    m.ResponseQueue.Send((EmiratesData)cDMData);
                }

                _cdmData.Add(cDMData);
            }
            else
            {
                Console.WriteLine("Error");
            }

            mq.BeginReceive();
        }

        void IReceiver<WowData>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            WowData data = (WowData)m.Body;

            if (DateTime.TryParse(data.TimeDate.ToString(), out DateTime date))
            {
                CDMData cDMData = new CDMData(data.Airline, data.FlightNumber, data.Destintation, data.Origin, date > DateTime.Now ? 'A' : 'D', date);
                _cdmData.Add(cDMData);
            }
            else
            {
                Console.WriteLine("Error");
            }

            mq.BeginReceive();
        }

        void IReceiver<EmiratesData>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            EmiratesData data = (EmiratesData)m.Body;

            if (DateTime.TryParse(data.TimeDeparture.ToString() + " " + data.Date.ToString(), out DateTime date))
            {
                CDMData cDMData = new CDMData(data.Airline, data.FlightNumber, data.Destintation, "Here", date > DateTime.Now ? 'A' : 'D', date);
                _cdmData.Add(cDMData);
            }
            else
            {
                Console.WriteLine("Error");
            }

            mq.BeginReceive();
        }
    }
}