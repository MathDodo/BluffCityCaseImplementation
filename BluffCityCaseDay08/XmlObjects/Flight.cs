using System;
using System.Xml.Serialization;

namespace BluffCityCaseDay08
{
    [XmlRoot(ElementName = "Flight")]
    public class Flight
    {
        [XmlElement(ElementName = "Origin")]
        public string Origin { get; set; }

        [XmlElement(ElementName = "Destination")]
        public string Destination { get; set; }

        [XmlAttribute(AttributeName = "number")]
        public string Number { get; set; }

        [XmlAttribute(AttributeName = "Flightdate")]
        public DateTime Flightdate { get; set; }
    }
}