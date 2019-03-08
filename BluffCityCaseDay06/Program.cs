using Data;
using System;
using System.Messaging;
using System.Xml;
using System.Xml.Serialization;

namespace BluffCityCaseDay06
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FlightDetailsInfoResponse));
            FlightDetailsInfoResponse response = (FlightDetailsInfoResponse)serializer.Deserialize(new XmlTextReader("Response.xml"));

            var emirates = new Emirates();

            MessageQueuesManager.Instance.CreateQueue<Tuple<int, int, Flight, Passenger>>(emirates, "Passenger");
            MessageQueuesManager.Instance.CreateQueue<Tuple<int, int, string, Flight, Luggage>>(emirates, "Luggage");
            MessageQueuesManager.Instance.CreateQueue<Tuple<int, int, Flight, Passenger>>(Resequencer.Instance, "Passenger");
            MessageQueuesManager.Instance.CreateQueue<Tuple<int, int, string, Flight, Luggage>>(Resequencer.Instance, "Luggage");

            Resequencer.Instance.AddAirline(emirates);

            var splitter = new Splitter();
            splitter.SplitMsg(response);

            while (!emirates._DoneReceiving) { }

            for (int i = 0; i < emirates._MyData.Count; i++)
            {
                FlightDetailsInfoResponse r = emirates._MyData[i];
                Console.WriteLine(r.ToString());

                for (int t = 0; t < emirates._MyData[i].Luggage.Count; t++)
                {
                    Console.WriteLine("Lugage id: " + emirates._MyData[i].Luggage[t].Identification);
                }
            }

            var queue = MessageQueuesManager.Instance.CreateQueue("TimeToBeReceived");

            var msg = new Message("Hello world", JsonFormatter.Instance);
            msg.TimeToBeReceived = new TimeSpan(0, 0, 20);
            queue.Send(msg);

            Console.ReadKey();
        }
    }
}