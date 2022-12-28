namespace FA.JustBlog.Utility;

public static class Helper
{
    public static string TimeCompareToNow(this DateTime time)
    {
        var now = DateTime.Now;
        var timeDiff = now - time;
        if (now.Day - time.Day == 1 && now.Month == time.Month && time.Year == now.Year)
        {
            return $"yesterday at {time.ToString("t")}";
        }
        else if (now.Day - time.Day == 0 && now.Month == time.Month && time.Year == now.Year)
        {
            if (timeDiff.Hours > 0)
            {
                return $"{timeDiff.Hours} hours and {timeDiff.Minutes} minutes ago";
            }
            else
            {
                return $"{timeDiff.Minutes} minutes ago";
            }
        }
        return $"{(int)timeDiff.TotalDays} days ago";
    }
}
