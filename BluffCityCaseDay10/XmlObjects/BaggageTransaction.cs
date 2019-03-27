using System.Xml.Serialization;

namespace BluffCityCaseDay10A
{
    [XmlRoot(ElementName = "BaggageTransaction"), System.Serializable]
    public struct BaggageTransaction
    {
        [XmlElement(ElementName = "TransactionId")]
        public int TransactionId { get; set; }

        [XmlElement(ElementName = "FlightNumber")]
        public string FlightNumber { get; set; }

        [XmlElement(ElementName = "AirlineCompany")]
        public string AirlineCompany { get; set; }

        [XmlElement(ElementName = "GateNumber")]
        public int GateNumber { get; set; }

        [XmlElement(ElementName = "Weight")]
        public float Weight { get; set; }

        [XmlElement(ElementName = "Class")]
        public string Class { get; set; }

        [XmlElement(ElementName = "Priority")]
        public string Priority { get; set; }

        [XmlElement(ElementName = "Destination")]
        public string Destination { get; set; }

        public BaggageTransaction(int transactionId, string flightNumber, string airlineCompany, int gateNumber, float weight, string @class, string priority, string destination)
        {
            TransactionId = transactionId;
            FlightNumber = flightNumber;
            AirlineCompany = airlineCompany;
            GateNumber = gateNumber;
            Weight = weight;
            Class = @class;
            Priority = priority;
            Destination = destination;
        }
    }
}