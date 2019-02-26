using Data;
using Setup;
using System;
using Setup.Data;
using System.Collections.Generic;

namespace BluffCityCaseDay02
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
            var path = "Day02";

            airlines.Add(sas);
            airlines.Add(wow);
            airlines.Add(emirates);

            for (int i = 0; i < airlines.Count; i++)
            {
                var airline = airlines[i].CompanyName.Substring(0, 2);

                for (int t = 0; t < 30; t++)
                {
                    flightNumbers.Add(airline + Router.Instance._Randy.Next(1000, 9999).ToString());
                    gates.Add(Router.Instance._Randy.Next(1, 48));
                    expected.Add(new Time(Router.Instance._Randy.Next(1, 24), Router.Instance._Randy.Next(1, 60)));
                    actualTime.Add(new Time(Router.Instance._Randy.Next(1, 24), Router.Instance._Randy.Next(1, 60)));
                    checkinTimes.Add(new Time(Router.Instance._Randy.Next(1, 24), Router.Instance._Randy.Next(1, 60)));
                }
            }

            for (int i = 0; i < airlines.Count; i++)
            {
                var airline = airlines[i];

                for (int t = 0; t < 30; t++)
                {
                    var time = actualTime[Router.Instance._Randy.Next(0, actualTime.Count)];
                    var destination = destinations[Router.Instance._Randy.Next(0, destinations.Count)];

                    airline.AddFlight(new AirlineCompanyData(destination, checkinTimes[Router.Instance._Randy.Next(0, checkinTimes.Count)],
                    time, airline.CompanyName, flightNumbers[i * 30 + t], CheckInStatus.Open));

                    center.AddCenterData(new AirportInformationCenterData(gates[Router.Instance._Randy.Next(0, gates.Count)], time, destination,
                        expected[Router.Instance._Randy.Next(0, expected.Count)], airline.CompanyName, FlightStatus.Boarding, flightNumbers[i * 30 + t]));
                }
            }

            MessageQueuesManager.Instance.CreateQueue(sas, path);
            MessageQueuesManager.Instance.CreateQueue(wow, path);
            MessageQueuesManager.Instance.CreateQueue(emirates, path);
            MessageQueuesManager.Instance.CreateQueue(Router.Instance, path);

            Router.Instance.AddAirline(sas);
            Router.Instance.AddAirline(wow);
            Router.Instance.AddAirline(emirates);

            for (int i = 0; i < airlines.Count; i++)
            {
                center._TargetCompany = airlines[i].CompanyName;

                for (int t = 0; t < airlines[i].Count; t++)
                {
                    center.SendMessage(Router.Instance[typeof(Tuple<string, int>)], new Tuple<string, int>(airlines[i][t].FlightNumber, 0));
                }
            }

            Console.ReadKey();
        }
    }
}