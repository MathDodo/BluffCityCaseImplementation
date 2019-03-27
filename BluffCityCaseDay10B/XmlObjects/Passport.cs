using System.Xml.Serialization;

namespace BluffCityCaseDay10B
{
    [XmlRoot(ElementName = "Passport")]
    public struct Passport
    {
        [XmlElement(ElementName = "PassNo")]
        public int PassNo { get; set; }

        [XmlElement(ElementName = "Nationality")]
        public string Nationality { get; set; }
    }
}