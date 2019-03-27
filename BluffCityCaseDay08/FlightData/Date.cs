using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluffCityCaseDay08
{
    [System.Serializable]
    public struct Date
    {
        public int DateNumber { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }

        public Date(int dateNumber, string month, int year)
        {
            DateNumber = dateNumber;
            Month = month;
            Year = year;
        }

        public Date(DateTime date, bool danish)
        {
            if (danish)
            {
                CultureInfo dk = new CultureInfo("da-DK");

                DateNumber = date.Day;
                Month = date.ToString("d/MMMM/yyyy", dk).Split('-')[1];
                Year = date.Year;
            }
            else
            {
                CultureInfo eng = new CultureInfo("en-GB");

                DateNumber = date.Day;
                Month = date.ToString("d/MMMM/yyyy", eng).Split('/')[1];
                Year = date.Year;
            }
        }

        public override string ToString()
        {
            return DateNumber.ToString() + " " + Month + " " + Year.ToString();
        }
    }
}