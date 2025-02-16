using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

#pragma warning disable MA0132, S6562

namespace Eon.Tests.Examples;

public static class DayOfWeekTests
{
    [Fact(
        DisplayName = "DayOfWeek is a cron like schedule that repeats every specified day of each week"
    )]
    public static void Case1()
    {
        #region Example1

        DateTime[] dates =
        [
            new(2022, 1, 1), // Saturday
            new(2022, 1, 2), // Sunday
            new(2022, 1, 3), // Monday
            new(2022, 1, 4), // Tuesday
            new(2022, 1, 5), // Wednesday
            new(2022, 1, 6), // Thursday
            new(2022, 1, 7), // Friday
        ];

        Schedule hourOfDay = Schedule.DayOfWeek(DayOfWeek.Monday, dates.ToTestProvider());

        #endregion

        hourOfDay.RenderSchedule(dates, "dddd MM").SaveResults();
    }
}
