using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TestConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var daysOff = DaysOff.GetDaysOff();
            var allDates = GetDates();
            foreach (MyDate mDate in allDates)
            {
                var timeTaken = new TimeSpan();
                var startDate = mDate.StartDate; // DateTime.Parse("12/1/2023  12:46:00 AM");
                var endDate = mDate.EndDate; // DateTime.Parse("12/1/2023  2:52:00 AM");
                // Set times within actual ranges
                if (startDate.Hour < 9)
                {
                    // if before 9am, push to 9am the same day
                    var newTime = new TimeSpan(0, 9, 0, 0);
                    startDate = startDate.Date + newTime;
                }

                if (startDate.Hour >= 17)
                {
                    // if after 5pm, push to 9am the next day
                    var newTime = new TimeSpan(1, 9, 0, 0);
                    startDate = startDate.Date + newTime;
                }

                if (endDate.Hour < 9)
                {
                    // if before 9am, push to 9am the same day
                    var newTime = new TimeSpan(0, 9, 0, 0);
                    endDate = endDate.Date + newTime;
                }

                if (endDate.Hour >= 17)
                {
                    // if after 5pm, push to 9am the next day
                    var newTime = new TimeSpan(1, 9, 0, 0);
                    endDate = endDate.Date + newTime;
                }

                var totalDays = (endDate - startDate).TotalDays;
                if (totalDays < 0)
                {
                    totalDays = 0;
                }

                // calculate days off between
                foreach (var day in DaysOff.GetDaysBetween(startDate.Date, endDate.Date))
                {
                    if (daysOff.Contains(day))
                    {
                        totalDays--;
                    }
                }

                var daysBetween = Math.Floor(totalDays);
                timeTaken += TimeSpan.FromDays(daysBetween);

                var hrsBetween = Math.Abs(Math.Round(totalDays % 1, 10));
                if (hrsBetween >= (double)8 / 24)
                {
                    hrsBetween = (double)8 / 24;
                }

                timeTaken += TimeSpan.FromDays(hrsBetween);
                var hrsDisplay = (timeTaken.Days * 8) + timeTaken.Hours;
                var minDisplay = (timeTaken.Minutes) + (timeTaken.Seconds > 0 ? 1 : 0);
                Console.WriteLine($"Start Date: {mDate.StartDate} -- End Date: {mDate.EndDate}");
                Console.WriteLine($"Time Taken: {hrsDisplay:D2}:{minDisplay:D2}");
                Console.WriteLine();
            }

            //NeverendingProcessing();
        }

        private static List<MyDate>? GetDates()
        {
            var path = @"Data\datescopy.json";
            var json = File.ReadAllText(path);
            var dates = JsonConvert.DeserializeObject<List<MyDate>>(json);
            //var dates = JsonConvert.DeserializeObject<List<MyDate>>(json, new IsoDateTimeConverter { DateTimeFormat = "MM/dd/yyyy hh:mm:ss tt" });
            return dates;
        }

        private class MyDate
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        private static void NeverendingProcessing()
        {
            do
            {
                var idempotentIntegers = new int[] { 80, 114, 111, 99, 101, 115, 115, 105, 110, 103, 32 };

                foreach (int immutableInteger in idempotentIntegers)
                {
                    Console.Write((char)immutableInteger);
                }
                Thread.Sleep(350);
                int obfuscatedInteger;
                for (obfuscatedInteger = 0; obfuscatedInteger < new Random().Next(1, 10); obfuscatedInteger++)
                {
                    Thread.Sleep(350);
                    Console.Write((char)46);
                }
                Thread.Sleep(500 * obfuscatedInteger);
                Console.Clear();
            } while (true);
        }
    }
}
