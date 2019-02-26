using Data;
using Setup;
using Setup.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluffCityCaseDay04
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<int> gates = new List<int>();
            List<Time> expected = new List<Time>();
            List<Time> actualTime = new List<Time>();
            List<Time> checkinTimes = new List<Time>();
            List<AirlineCompany> airlines = new List<AirlineCompany>();
            List<string> destinations = new List<string>();
            List<string> flightNumbers = new List<string>();
            List<IRequester> requesters = new List<IRequester>();

            destinations.Add("Copenhagen");
            destinations.Add("London");
            destinations.Add("Vancouver");
            destinations.Add("Boston");
            destinations.Add("Berlin");
            destinations.Add("Paris");

            var center = new BluffCityAirportInfoCenter();
            var sas = new SAS();
            var wow = new WowAir();
            var emirates = new Emirates();
            var path = "Day04";

            airlines.Add(sas);
            airlines.Add(wow);
            airlines.Add(emirates);

            for (int i = 0; i < airlines.Count; i++)
            {
                var airline = airlines[i].CompanyName.Substring(0, 2);

                if (airlines[i] is IRequester)
                {
                    requesters.Add((IRequester)airlines[i]);
                }

                for (int t = 0; t < 30; t++)
                {
                    flightNumbers.Add(airline + RandomManager.Instance._Randy.Next(1000, 9999).ToString());
                    gates.Add(RandomManager.Instance._Randy.Next(1, 48));
                    expected.Add(new Time(RandomManager.Instance._Randy.Next(1, 24), RandomManager.Instance._Randy.Next(1, 60)));
                    actualTime.Add(new Time(RandomManager.Instance._Randy.Next(1, 24), RandomManager.Instance._Randy.Next(1, 60)));
                    checkinTimes.Add(new Time(RandomManager.Instance._Randy.Next(1, 24), RandomManager.Instance._Randy.Next(1, 60)));
                }
            }

            for (int i = 0; i < airlines.Count; i++)
            {
                var airline = airlines[i];

                for (int t = 0; t < 30; t++)
                {
                    var time = actualTime[RandomManager.Instance._Randy.Next(0, actualTime.Count)];
                    var destination = destinations[RandomManager.Instance._Randy.Next(0, destinations.Count)];

                    airline.AddFlight(new AirlineCompanyData(destination, checkinTimes[RandomManager.Instance._Randy.Next(0, checkinTimes.Count)],
                    time, airline.CompanyName, flightNumbers[i * 30 + t], CheckInStatus.Open));

                    center.AddCenterData(new AirportInformationCenterData(gates[RandomManager.Instance._Randy.Next(0, gates.Count)], time, destination,
                        expected[RandomManager.Instance._Randy.Next(0, expected.Count)], airline.CompanyName, FlightStatus.Boarding, flightNumbers[i * 30 + t]));
                }
            }

            MessageQueuesManager.Instance.CreateQueue(sas, path);
            MessageQueuesManager.Instance.CreateQueue(wow, path);
            MessageQueuesManager.Instance.CreateQueue(emirates, path);
            MessageQueuesManager.Instance.CreateQueue(center, path);

            for (int i = 0; i < requesters.Count; i++)
            {
                requesters[i].Request(center[typeof(string)]);
            }

            Console.ReadKey();
        }
    }
}