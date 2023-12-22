namespace TestConsoleApp
{
    public static class DateHelper
    {
        public static bool IsFederalHoliday(this DateTime date)
        {
            // to ease typing
            var nthWeekDay = (int)Math.Ceiling(date.Day / 7.0d);
            var dayName = date.DayOfWeek;
            var isThursday = dayName == DayOfWeek.Thursday;
            var isFriday = dayName == DayOfWeek.Friday;
            var isMonday = dayName == DayOfWeek.Monday;
            var isWeekend = dayName is DayOfWeek.Saturday or DayOfWeek.Sunday;

            // New Years Day (Jan 1, or preceding Friday/following Monday if weekend)
            if ((date is { Month: 12, Day: 31 } && isFriday) ||
                (date is { Month: 1, Day: 1 } && !isWeekend) ||
                (date is { Month: 1, Day: 2 } && isMonday))
            {
                return true;
            }

            // Memorial Day (Last Monday in May)
            if (date.Month == 5 && isMonday && date.AddDays(7).Month == 6)
            {
                return true;
            }

            // Independence Day (July 4, or preceding Friday/following Monday if weekend)
            if ((date is { Month: 7, Day: 3 } && isFriday) ||
                (date is { Month: 7, Day: 4 } && !isWeekend) ||
                (date is { Month: 7, Day: 5 } && isMonday))
            {
                return true;
            }

            // Labor Day (1st Monday in September)
            if (date.Month == 9 && isMonday && nthWeekDay == 1)
            {
                return true;
            }

            // Thanksgiving Day (4th Thursday in November)
            if (date.Month == 11 && isThursday && nthWeekDay == 4)
            {
                return true;
            }

            // Christmas Day (December 25, or preceding Friday/following Monday if weekend))
            if ((date is { Month: 12, Day: 24 } && isFriday) ||
                (date is { Month: 12, Day: 25 } && !isWeekend) ||
                (date is { Month: 12, Day: 26 } && isMonday))
            {
                return true;
            }

            return false;
        }

        public static DateTime FirstDay(int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 1, 1);
        }

        public static DateTime LastDay(int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 12, 31);
        }
    }
}
