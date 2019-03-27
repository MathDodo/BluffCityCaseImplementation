using Data;
using System;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace BluffCityCaseDay08
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var time = DateTime.Parse("3/06/2017", new CultureInfo("en-US"));
            Console.WriteLine(time.ToString("d/MM/yyyy"));

            XmlSerializer serializer = new XmlSerializer(typeof(FlightDetailsInfoResponse));
            FlightDetailsInfoResponse response = (FlightDetailsInfoResponse)serializer.Deserialize(new XmlTextReader("Response.xml"));

            MessageQueuesManager.Instance.CreateQueue<SASData>(CDMTranslator.Instance, "TranslationOfSas");
            MessageQueuesManager.Instance.CreateQueue<WowData>(CDMTranslator.Instance, "TranslationOfWow");
            MessageQueuesManager.Instance.CreateQueue<EmiratesData>(CDMTranslator.Instance, "TranslationOfEmirates");
            MessageQueuesManager.Instance.CreateQueue(AirportCargoLoader.Instance, "CargoInfo");

            var emirates = new Emirates();

            MessageQueuesManager.Instance.CreateQueue(emirates, "EmirateData");

            MessageQueuesManager.Instance.SendMessage(new SASData("Sas", "SK 239", "JFK", "CPH", 'D', new Date(6, "marts", 2017), new Setup.Data.Time(16, 45)), "Emirates",
                CDMTranslator.Instance[typeof(SASData)], emirates[typeof(EmiratesData)]);
            MessageQueuesManager.Instance.SendMessage(new WowData("Wow", "154", "Sandiego", "Schipol", new TimeDate(6, "March", 2017, new Setup.Data.Time(16, 45))), CDMTranslator.Instance[typeof(WowData)]);
            MessageQueuesManager.Instance.SendMessage(new EmiratesData("Emirates", "EM056", "New York", new Date(6, "3", 2017), new Setup.Data.Time(9, 45)), CDMTranslator.Instance[typeof(EmiratesData)]);

            MessageQueuesManager.Instance.SendMessage(new AirportCargoData(response.Flight.Number, response.Passenger.ReservationNumber, response.Luggage.fore))

            Console.ReadKey();

            for (int i = 0; i < CDMTranslator.Instance.Count; i++)
            {
                Console.WriteLine(CDMTranslator.Instance[i].ToString());
            }

            for (int i = 0; i < CDMTranslator.Instance.Count; i++)
            {
                SASData data = CDMTranslator.Instance[i];
            }

            Console.ReadKey();
        }
    }
}