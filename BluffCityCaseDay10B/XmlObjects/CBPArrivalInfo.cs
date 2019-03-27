using System.Xml.Serialization;
using System.Collections.Generic;

namespace BluffCityCaseDay10B
{
    [XmlRoot(ElementName = "CBPArrivalInfo")]
    public class CBPArrivalInfo
    {
        [XmlElement(ElementName = "Flight")]
        public Flight Flight { get; set; }

        [XmlElement(ElementName = "Passenger")]
        public Passenger Passenger { get; set; }

        [XmlElement(ElementName = "Passport")]
        public List<Passport> Passport { get; set; }
    }
}