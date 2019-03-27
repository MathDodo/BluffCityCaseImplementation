using Setup.Data;

namespace BluffCityCaseDay08
{
    [System.Serializable]
    public struct EmiratesData
    {
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string Destintation { get; set; }
        public Date Date { get; set; }
        public Time TimeDeparture { get; set; }

        public EmiratesData(string airline, string flightNumber, string destintation, Date date, Time timeDeparture)
        {
            Airline = airline;
            FlightNumber = flightNumber;
            Destintation = destintation;
            Date = date;
            TimeDeparture = timeDeparture;
        }
    }
}