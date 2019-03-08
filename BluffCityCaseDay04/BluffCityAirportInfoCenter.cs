using Data;
using System;
using Setup.Data;
using System.Messaging;
using BluffCitySetupLib;

namespace BluffCityCaseDay04
{
    public class BluffCityAirportInfoCenter : AirportInformationCenter, ITransmitter<Tuple<string, Time>>, IReceiver<string>
    {
        public string _TargetCompany = string.Empty;

        public string Name => "Infocenter";

        void IReceiver<string>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            if (m.Label == "Expected")
            {
                string val = (string)m.Body;
                m.ResponseQueue.Formatter = JsonFormatter.Instance;
                SendMessage(m.ResponseQueue, new Tuple<string, Time>(val, _centerData.Find(d => d.FlightNumber == val).Expected));
            }

            mq.BeginReceive();
        }

        public void SendMessage(MessageQueue messageQueue, Tuple<string, Time> messageBody)
        {
            MessageQueuesManager.Instance.SendMessage(messageBody, _TargetCompany, messageQueue);
        }
    }
}