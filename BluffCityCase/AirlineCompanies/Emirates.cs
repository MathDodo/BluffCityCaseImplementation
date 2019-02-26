using Data;
using Setup;
using System;
using System.Messaging;

namespace BluffCityCaseDay02
{
    public class Emirates : AirlineCompany, IReceiver<Tuple<string, int>>
    {
        public string Name => CompanyName;

        public Emirates() : base("Emirates")
        {
        }

        void IReceiver<Tuple<string, int>>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            var body = (Tuple<string, int>)m.Body;

            Console.WriteLine(_flightsInformation.Find(f => f.FlightNumber == body.Item1).ToString() + " will be at gate: " + body.Item2);

            mq.BeginReceive();
        }
    }
}