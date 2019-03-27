using Data;
using System.Messaging;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BluffCityCaseDay10A
{
    public class DynamicRouter : MessageQueueAccessor, IReceiver<int>
    {
        private List<Rule> _rules;
        private MessageQueue _invalidQueue;

        public string Name => "DynamicRouter";

        private static DynamicRouter _instance;

        public static DynamicRouter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DynamicRouter();
                }

                return _instance;
            }
        }

        private DynamicRouter()
        {
            _rules = new List<Rule>();
            _invalidQueue = MessageQueuesManager.Instance.CreateQueue("Invalid");
            _invalidQueue.Purge();
        }

        public void OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            var rule = (int)Convert.ToInt64(m.Body);

            Rule existing;

            if ((existing = _rules.Find(r => r.ID == m.Label)) != null)
            {
                Console.WriteLine("Updated rule for: " + m.Label + " from old gatenumber: " + existing.GateNumber + " to new gatenumber: " + rule);
                existing.GateNumber = rule;
            }
            else
            {
                _rules.Add(new Rule(m.Label, rule, m.ResponseQueue));
                Console.WriteLine("Created rule for: " + m.Label + " the gatenumber is: " + rule);
            }

            mq.BeginReceive();
        }

        public void Send(BaggageTransaction baggage, int targetGate)
        {
            var recipients = _rules.Where(r => r.GateNumber == targetGate).ToArray();

            for (int i = 0; i < recipients.Length; i++)
            {
                MessageQueuesManager.Instance.SendMessage(baggage, recipients[i].TargetQueue);
            }

            if (recipients.Length == 0)
            {
                MessageQueuesManager.Instance.SendMessage(baggage, _invalidQueue);
            }
        }
    }
}