using System.Xml.Serialization;

namespace BluffCityCaseDay08
{
    [XmlRoot(ElementName = "Passenger")]
    public class Passenger
    {
        [XmlElement(ElementName = "ReservationNumber")]
        public string ReservationNumber { get; set; }

        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "LastName")]
        public string LastName { get; set; }
    }
}