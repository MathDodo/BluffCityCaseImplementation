using Data;
using System;
using System.Messaging;
using System.Timers;

namespace BluffCityCaseDay10A
{
    public class GateListener : MessageQueueAccessor, IReceiver<BaggageTransaction>
    {
        private string _id;
        private string _name;
        private int _gateNumber;

        public string Name => _name;

        public GateListener(string name)
        {
            _id = Guid.NewGuid().ToString();
            _name = name;
            _gateNumber = 3;
        }

        public void Setup()
        {
            MessageQueuesManager.Instance.SendMessage(_gateNumber, _id, DynamicRouter.Instance[typeof(int)], this[typeof(BaggageTransaction)]);

            Timer timer = new Timer(RandomManager.Instance._Randy.Next(5000, 100000));
            timer.AutoReset = true;
            timer.Elapsed += OnTimed;
            timer.Enabled = true;
        }

        private void OnTimed(object sender, ElapsedEventArgs e)
        {
            _gateNumber = RandomManager.Instance._Randy.Next(1, 44);
            MessageQueuesManager.Instance.SendMessage(_gateNumber, _id, DynamicRouter.Instance[typeof(int)]);
        }

        public void OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            Console.WriteLine("Received stuff for: " + _id + " at gatenumber: " + _gateNumber);

            mq.BeginReceive();
        }
    }
}