using System;

namespace BluffCityCaseDay08
{
    [Serializable]
    public struct AirportCargoData
    {
        public string FlighNumber { get; set; }
        public string ReservationNumber { get; set; }
        public float TotalLuggageWeight { get; set; }

        public AirportCargoData(string flighNumber, string reservationNumber, float totalLuggageWeight)
        {
            FlighNumber = flighNumber;
            ReservationNumber = reservationNumber;
            TotalLuggageWeight = totalLuggageWeight;
        }
    }
}