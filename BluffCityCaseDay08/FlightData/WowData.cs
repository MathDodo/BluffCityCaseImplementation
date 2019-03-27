namespace BluffCityCaseDay08
{
    [System.Serializable]
    public struct WowData
    {
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string Destintation { get; set; }
        public string Origin { get; set; }
        public TimeDate TimeDate { get; set; }

        public WowData(string airline, string flightNumber, string destintation, string origin, TimeDate timeDate)
        {
            Airline = airline;
            FlightNumber = flightNumber;
            Destintation = destintation;
            Origin = origin;
            TimeDate = timeDate;
        }
    }
}