using Data;
using System;

namespace BluffCityCaseDay06
{
    public class Splitter
    {
        public void SplitMsg(FlightDetailsInfoResponse response)
        {
            for (int i = response.Luggage.Count; i-- > 0;)
            {
                MessageQueuesManager.Instance.SendMessage(new Tuple<int, int, string, Flight, Luggage>(response.Luggage.Count + 1, i + 1, response.Passenger.ReservationNumber, response.Flight,
                    response.Luggage[i]), "Emirates", Resequencer.Instance[typeof(Tuple<int, int, string, Flight, Luggage>)]);
            }

            MessageQueuesManager.Instance.SendMessage(new Tuple<int, int, Flight, Passenger>(response.Luggage.Count + 1, 0, response.Flight, response.Passenger), "Emirates",
            Resequencer.Instance[typeof(Tuple<int, int, Flight, Passenger>)]);
        }
    }
}