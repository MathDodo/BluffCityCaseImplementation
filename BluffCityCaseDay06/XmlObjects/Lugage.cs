using System.Xml.Serialization;

namespace BluffCityCaseDay06
{
    [XmlRoot(ElementName = "Luggage")]
    public class Luggage
    {
        [XmlElement(ElementName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "Identification")]
        public int Identification { get; set; }

        [XmlElement(ElementName = "Category")]
        public string Category { get; set; }

        [XmlElement(ElementName = "Weight")]
        public float Weight { get; set; }
    }
}