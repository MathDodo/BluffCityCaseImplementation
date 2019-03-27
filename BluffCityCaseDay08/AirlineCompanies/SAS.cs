using Data;
using Setup;
using Setup.Data;
using System;
using System.Messaging;

namespace BluffCityCaseDay08
{
    public class SAS : AirlineCompany
    {
        public string Name => CompanyName;

        public SAS() : base("SAS")
        {
        }
    }
}