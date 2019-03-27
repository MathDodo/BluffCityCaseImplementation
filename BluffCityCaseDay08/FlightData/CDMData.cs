using System;

namespace BluffCityCaseDay08
{
    [System.Serializable]
    public struct CDMData
    {
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string Destintation { get; set; }
        public string Origin { get; set; }
        public char ArrivalDeparture { get; set; }
        public DateTime Date { get; set; }

        public CDMData(string airline, string flightNumber, string destintation, string origin, char arrivalDeparture, DateTime date)
        {
            Airline = airline;
            FlightNumber = flightNumber;
            Destintation = destintation;
            Origin = origin;
            ArrivalDeparture = arrivalDeparture;
            Date = date;
        }

        public override string ToString()
        {
            return "Airline: " + Airline + " Flightno: " + FlightNumber + " Destination: " + Destintation + " Origin: " + Origin + " Arrived or departed: " + ArrivalDeparture + " Date: " + Date.ToString();
        }

        public static implicit operator SASData(CDMData data)
        {
            return new SASData(data.Airline, data.FlightNumber, data.Destintation, data.Origin, data.ArrivalDeparture,
                new Date(data.Date.Day, data.Date.Month.ToString(), data.Date.Year), new Setup.Data.Time(data.Date.Hour, data.Date.Minute));
        }

        public static implicit operator EmiratesData(CDMData data)
        {
            return new EmiratesData(data.Airline, data.FlightNumber, data.Destintation, new Date(data.Date.Day, data.Date.Month.ToString(), data.Date.Year),
                new Setup.Data.Time(data.Date.Hour, data.Date.Minute));
        }

        public static implicit operator WowData(CDMData data)
        {
            return new WowData(data.Airline, data.FlightNumber, data.Destintation, data.Origin, new TimeDate(data.Date.Day, data.Date.Month.ToString(), data.Date.Year,
                new Setup.Data.Time(data.Date.Hour, data.Date.Minute)));
        }
    }
}