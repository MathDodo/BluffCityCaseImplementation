using Data;
using System.Messaging;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BluffCityCaseDay10B
{
    public class RecipientsList : MessageQueueAccessor, IReceiver<string>
    {
        private List<Rule> _rules;
        private MessageQueue _invalidQueue;

        public string Name => "DynamicRouter";

        private static RecipientsList _instance;

        public static RecipientsList Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecipientsList();
                }

                return _instance;
            }
        }

        private RecipientsList()
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

            var rule = (string)m.Body;

            Rule existing;

            if ((existing = _rules.Find(r => r.ID == m.Label)) != null)
            {
                existing.RuleVal = rule;
            }
            else
            {
                _rules.Add(new Rule(m.Label, rule, m.ResponseQueue));
                Console.WriteLine("Created rule for: " + m.Label + " the country identifier is: " + rule);
            }

            mq.BeginReceive();
        }

        public void Send(CBPArrivalInfo info)
        {
            List<Rule> recipients = new List<Rule>();

            for (int i = 0; i < info.Passport.Count; i++)
            {
                recipients.AddRange(_rules.Where(r => r.RuleVal == info.Passport[i].Nationality));
            }

            for (int i = 0; i < recipients.Count; i++)
            {
                var sendInfo = new Tuple<Passenger, Passport>(info.Passenger, info.Passport.Find(p => p.Nationality == recipients[i].RuleVal));

                MessageQueuesManager.Instance.SendMessage(sendInfo, recipients[i].TargetQueue);
            }

            if (recipients.Count == 0)
            {
                MessageQueuesManager.Instance.SendMessage(info, _invalidQueue);
            }
        }
    }
}