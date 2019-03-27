using Data;
using System.Messaging;

namespace BluffCityCaseDay10B
{
    public class Rule
    {
        public string ID { get; set; }
        public string RuleVal { get; set; }
        public MessageQueue TargetQueue { get; set; }

        public Rule(string iD, string rule, MessageQueue targetQueue)
        {
            ID = iD;
            RuleVal = rule;
            TargetQueue = targetQueue;
            targetQueue.Formatter = JsonFormatter.Instance;
        }
    }
}