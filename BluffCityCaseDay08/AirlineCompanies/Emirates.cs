using Data;
using Setup;
using System;
using System.Messaging;

namespace BluffCityCaseDay08
{
    public class Emirates : AirlineCompany, IReceiver<EmiratesData>
    {
        public string Name => CompanyName;

        public Emirates() : base("Emirates")
        {
        }

        public void OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            EmiratesData data = (EmiratesData)m.Body;

            Console.WriteLine("Has data");

            mq.BeginReceive();
        }
    }
}