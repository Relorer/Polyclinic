using System;
using System.Globalization;

namespace POLYCLINIC.BLL.Infrastructure
{
    public class Week
    {    
        private readonly CultureInfo ru = CultureInfo.GetCultureInfo("ru-RU");
        private DateTime monday;
        private DateTime sunday;

        public DateTime Monday
        {
            get => monday;
            set
            {
                if (value.DayOfWeek != DayOfWeek.Monday)
                {
                    throw new Exception(value + " is not Monday");
                }
                monday = value;
                sunday = value.AddDays(6);
            }
        }

        public DateTime Sunday
        {
            get => sunday;
            set
            {
                if (value.DayOfWeek != DayOfWeek.Sunday)
                {
                    throw new Exception(value + " is not Sunday");
                }
                sunday = value;
                monday = value.AddDays(-6);
            }
        }

        public Week(DateTime day)
        {
            Monday = day.StartOfWeek(DayOfWeek.Monday);
        }

        public override string ToString()
        {
            return $"{Monday.Day} - {Sunday.Day} {ru.DateTimeFormat.MonthGenitiveNames[Sunday.Month - 1]}";
        }

        public DateTime GetDay(DayOfWeek day)
        {
            return Monday.AddDays((7 + (day - DayOfWeek.Monday)) % 7);
        }

        public string ToString(DayOfWeek day)
        {
            DateTime date = GetDay(day);
            return $"{date:ddd d}\n{ru.DateTimeFormat.MonthGenitiveNames[date.Month - 1]}";
        }

        public bool Exist(DateTime date)
        {
            return date >= monday && date <= sunday;
        }
    }

}
