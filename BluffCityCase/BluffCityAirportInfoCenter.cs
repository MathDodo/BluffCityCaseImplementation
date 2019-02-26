using Data;
using System;
using System.Messaging;
using BluffCitySetupLib;

namespace BluffCityCaseDay02
{
    public class BluffCityAirportInfoCenter : AirportInformationCenter, ITransmitter<Tuple<string, int>>
    {
        public string _TargetCompany = string.Empty;

        public void SendMessage(MessageQueue messageQueue, Tuple<string, int> messageBody)
        {
            messageBody = new Tuple<string, int>(messageBody.Item1, _centerData.Find(d => d.FlightNumber == messageBody.Item1).Gate);

            MessageQueuesManager.Instance.SendMessage(messageBody, _TargetCompany, messageQueue);
        }
    }
}