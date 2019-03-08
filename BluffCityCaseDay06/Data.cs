using System.Collections.Generic;

namespace BluffCityCaseDay06
{
    public class Data
    {
        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
        public List<Luggage> Luggage { get; set; }

        public Data(Flight flight, Passenger passenger, List<Luggage> luggage)
        {
            Flight = flight;
            Passenger = passenger;
            Luggage = luggage;
        }

        public static implicit operator FlightDetailsInfoResponse(Data data)
        {
            return new FlightDetailsInfoResponse() { Flight = data.Flight, Passenger = data.Passenger, Luggage = data.Luggage };
        }
    }
}