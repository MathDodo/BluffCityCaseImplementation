using System.Xml.Serialization;

namespace BluffCityCaseDay10B
{
    [XmlRoot(ElementName = "Passenger")]
    public struct Passenger
    {
        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "LastName")]
        public string LastName { get; set; }

        [XmlElement(ElementName = "DayOfBirth")]
        public string DayOfBirth { get; set; }

        [XmlElement(ElementName = "Height")]
        public string Height { get; set; }

        [XmlElement(ElementName = "Sex")]
        public string Sex { get; set; }
    }
}