using Data;
using Setup;
using Setup.Data;
using System;
using System.Messaging;

namespace BluffCityCaseDay04
{
    public class SAS : AirlineCompany, IReceiver<Tuple<string, Time>>, IRequester
    {
        public string Name => CompanyName;

        public SAS() : base("SAS")
        {
        }

        void IReceiver<Tuple<string, Time>>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            var body = (Tuple<string, Time>)m.Body;

            Console.WriteLine(_flightsInformation.Find(f => f.FlightNumber == body.Item1).ToString() + " is expected to arrive at: " + body.Item2.ToString());

            mq.BeginReceive();
        }

        public void Request(MessageQueue queueToSendRequest)
        {
            MessageQueuesManager.Instance.SendMessage(_flightsInformation[RandomManager.Instance._Randy.Next(0, _flightsInformation.Count)].FlightNumber,
                "Expected", queueToSendRequest, this[typeof(Tuple<string, Time>)]);
        }
    }
}