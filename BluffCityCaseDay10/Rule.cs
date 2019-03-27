using Data;
using System.Messaging;

namespace BluffCityCaseDay10A
{
    public class Rule
    {
        public string ID { get; set; }
        public int GateNumber { get; set; }
        public MessageQueue TargetQueue { get; set; }

        public Rule(string iD, int gateNumber, MessageQueue targetQueue)
        {
            ID = iD;
            GateNumber = gateNumber;
            TargetQueue = targetQueue;
            targetQueue.Formatter = JsonFormatter.Instance;
        }
    }
}