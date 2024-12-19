namespace mind.Core.Extensions;

public static class DateTimeExtensions
{
    public static string GetTimeWorked(this DateTime hireDate)
    {
        var today = DateTime.Today;
        var years = today.Year - hireDate.Year;
        var months = today.Month - hireDate.Month;
        var days = today.Day - hireDate.Day;

        if (days < 0)
        {
            months--;
            days += DateTime.DaysInMonth(today.Year, ((today.Month - 1) == 0 ? 12 : today.Month - 1));
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        return $"({years}y - {months}m - {days}d)";
    }
} 