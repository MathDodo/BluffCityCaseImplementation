using Data;
using Setup;
using Setup.Data;
using System;
using System.Messaging;

namespace BluffCityCaseDay08
{
    public class WowAir : AirlineCompany
    {
        public string Name => CompanyName;

        public WowAir() : base("Wow Air")
        {
        }
    }
}