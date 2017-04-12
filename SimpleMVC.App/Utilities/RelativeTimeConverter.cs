using System;

namespace SimpleMVC.App.Utilities
{
    public static class RelativeTimeConverter
    {
        public static string ToRelativeTime(this DateTime currentDate)
        {
            const int Second = 1;
            const int Minute = 60 * Second;
            const int Hour = 60 * Minute;
            const int Day = 24 * Hour;
            const int Month = 30 * Day;

            var ts = DateTime.Now - currentDate;
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * Second)
                return "less than a second";

            if (delta < 1 * Minute)
                return "less than a minute";

            if (delta < Hour)
                return ts.Minutes + " minutes ago";

            if (delta < Day)
                return ts.Hours + " hours ago";

            if (delta < Month)
                return ts.Days + " days ago";

            if (delta < 12 * Month)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                return "more than a year";
            }
        }
    }
}
