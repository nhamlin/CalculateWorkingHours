namespace TestConsoleApp
{
    internal class DaysOff
    {
        private static readonly List<DateTime> _ftoDaysOff = new List<DateTime>()
        {
            new DateTime(2023, 7, 3),
            new DateTime(2023, 8, 1),
            new DateTime(2023, 8, 2),
            new DateTime(2023, 9, 22),
            new DateTime(2023, 9, 29),
            new DateTime(2023, 11, 24)
        };

        public static List<DateTime> GetDaysOff(int? year = null)
        {
            var daysOff = new List<DateTime>();
            var allDays = GetDaysBetween(DateHelper.FirstDay(year), DateHelper.LastDay(year)).ToList();
            var weekends = allDays.Where(d => d.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday);
            daysOff.AddRange(weekends);
            var holidays = GetHolidays(allDays);
            daysOff.AddRange(holidays);
            daysOff.AddRange(_ftoDaysOff);
            return daysOff.OrderBy(x => x.Date).ToList();
        }

        public static IEnumerable<DateTime> GetDaysBetween(DateTime start, DateTime end)
        {
            for (var i = start; i < end; i = i.AddDays(1))
            {
                yield return i;
            }
        }

        private static IEnumerable<DateTime> GetHolidays(IEnumerable<DateTime> daysList)
        {
            return daysList.Where(day => day.IsFederalHoliday());
        }
    }
}
