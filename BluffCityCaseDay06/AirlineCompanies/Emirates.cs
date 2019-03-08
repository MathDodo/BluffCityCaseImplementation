using Data;
using Setup;
using System;
using Setup.Data;
using System.Messaging;
using System.Collections.Generic;
using System.Linq;

namespace BluffCityCaseDay06
{
    public class Emirates : AirlineCompany, IRequester, IReceiver<Tuple<int, int, Flight, Passenger>>, IReceiver<Tuple<int, int, string, Flight, Luggage>>
    {
        public List<Data> _MyData;
        public bool _DoneReceiving;
        public string Name => CompanyName;

        public Emirates() : base("Emirates")
        {
            _MyData = new List<Data>();
        }

        void IReceiver<Tuple<int, int, Flight, Passenger>>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            if (m.Label == CompanyName)
            {
                var value = (Tuple<int, int, Flight, Passenger>)m.Body;
                Data da = null;

                if (!_MyData.Any(d => d.Passenger.ReservationNumber == value.Item4.ReservationNumber))
                {
                    _MyData.Add(new Data(value.Item3, value.Item4, new List<Luggage>()));
                }
                else if ((da = _MyData.Find(d => d.Passenger.ReservationNumber == value.Item4.ReservationNumber)).Passenger.FirstName == null)
                {
                    da.Flight = value.Item3;
                    da.Passenger = value.Item4;
                }

                if (da != null && da.Luggage.Count + 1 == value.Item1)
                {
                    _DoneReceiving = true;
                }
            }

            mq.BeginReceive();
        }

        void IReceiver<Tuple<int, int, string, Flight, Luggage>>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            if (m.Label == CompanyName)
            {
                var value = (Tuple<int, int, string, Flight, Luggage>)m.Body;
                Data da = null;

                if (!_MyData.Any(d => d.Passenger.ReservationNumber == value.Item3))
                {
                    _MyData.Add(new Data(value.Item4, new Passenger() { ReservationNumber = value.Item3 }, new List<Luggage>() { value.Item5 }));
                }
                else if ((da = _MyData.Find(d => d.Passenger.ReservationNumber == value.Item3)) != null)
                {
                    da.Luggage.Add(value.Item5);

                    da.Luggage.Sort((x, y) => x.Identification.CompareTo(y.Identification));
                }

                if (da != null && da.Luggage.Count + 1 == value.Item1)
                {
                    _DoneReceiving = true;
                }
            }

            mq.BeginReceive();
        }

        public void Request(MessageQueue queueToSendRequest)
        {
            MessageQueuesManager.Instance.SendMessage(_flightsInformation[RandomManager.Instance._Randy.Next(0, _flightsInformation.Count)].FlightNumber,
                "Expected", queueToSendRequest, this[typeof(Tuple<string, Time>)]);
        }
    }
}