using System.Xml.Serialization;
using System.Collections.Generic;

namespace BluffCityCaseDay06
{
    [XmlRoot(ElementName = "FlightDetailsInfoResponse")]
    public class FlightDetailsInfoResponse
    {
        [XmlElement(ElementName = "Flight")]
        public Flight Flight { get; set; }

        [XmlElement(ElementName = "Passenger")]
        public Passenger Passenger { get; set; }

        [XmlElement(ElementName = "Luggage")]
        public List<Luggage> Luggage { get; set; }

        public override string ToString()
        {
            return "Flightnumber: " + Flight.Number + " Passenger name: " + Passenger.FirstName + " " + Passenger.LastName + " Lugage amount: " + Luggage.Count;
        }
    }
}