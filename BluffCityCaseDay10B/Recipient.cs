using Data;
using System;
using System.Messaging;

namespace BluffCityCaseDay10B
{
    public class Recipient : MessageQueueAccessor, IReceiver<Tuple<Passenger, Passport>>
    {
        private string _id;
        private string _name;

        public string Name => _name;

        public Recipient(string name)
        {
            _id = Guid.NewGuid().ToString();
            _name = name;
        }

        public void Setup()
        {
            MessageQueuesManager.Instance.SendMessage(_name, _id, RecipientsList.Instance[typeof(string)], this[typeof(Tuple<Passenger, Passport>)]);
        }

        public void OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            var info = (Tuple<Passenger, Passport>)m.Body;

            Console.WriteLine("Process in: " + _name + " system: " + _id + " has started for passportnumber: " + info.Item2.PassNo);

            mq.BeginReceive();
        }
    }
}