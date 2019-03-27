using Setup.Data;

namespace BluffCityCaseDay08
{
    public struct TimeDate
    {
        public int DateNumber { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public Time Time { get; set; }

        public TimeDate(int dateNumber, string month, int year, Time time)
        {
            DateNumber = dateNumber;
            Month = month;
            Year = year;
            Time = time;
        }

        public override string ToString()
        {
            return Time.ToString() + " " + DateNumber.ToString() + " " + Month.ToString() + " " + Year.ToString();
        }
    }
}