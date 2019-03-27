using Setup.Data;

namespace BluffCityCaseDay08
{
    [System.Serializable]
    public struct SASData
    {
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string Destintation { get; set; }
        public string Origin { get; set; }
        public char ArrivalDeparture { get; set; }
        public Date Date { get; set; }
        public Time Time { get; set; }

        public SASData(string airline, string flightnumber, string destination, string origin, char arrivalDeparture, Date date, Time time)
        {
            Airline = airline;
            FlightNumber = flightnumber;
            Destintation = destination;
            Origin = origin;
            ArrivalDeparture = arrivalDeparture;
            Date = date;
            Time = time;
        }
    }
}