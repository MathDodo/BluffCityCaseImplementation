using System.Xml.Serialization;

namespace BluffCityCaseDay10B
{
    [XmlRoot(ElementName = "Flight")]
    public struct Flight
    {
        [XmlElement(ElementName = "Origin")]
        public string Origin { get; set; }

        [XmlElement(ElementName = "Destination")]
        public string Destination { get; set; }

        [XmlAttribute(AttributeName = "number")]
        public string Number { get; set; }

        [XmlAttribute(AttributeName = "Flightdate")]
        public string Flightdate { get; set; }
    }
}