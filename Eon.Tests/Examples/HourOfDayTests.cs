using System.Diagnostics.CodeAnalysis;
using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

[SuppressMessage(
    "Major Code Smell",
    "S6562:Always set the \"DateTimeKind\" when creating new \"DateTime\" instances"
)]
[SuppressMessage("Design", "MA0132:Do not convert implicitly to DateTimeOffset")]
public static class HourOfDayTests
{
    [Fact(
        DisplayName = "HourOfDay is a cron like schedule that repeats every specified hour of each day"
    )]
    public static void Case1()
    {
        #region Example1

        DateTime[] dates =
        [
            new(2022, 1, 1, 1, 0, 0), // 1
            new(2022, 1, 1, 4, 0, 0), // 4
            new(2022, 1, 1, 6, 0, 0), // 6
            new(2022, 1, 1, 3, 0, 0), // 3
        ];

        Schedule hourOfDay = Schedule.HourOfDay(3, dates.ToTestProvider());

        #endregion

        hourOfDay.RenderSchedule(dates, "f").SaveResults();
    }

    [Fact(DisplayName = "HourOfDay rounds hours greater than 24 to 24")]
    public static void Case2()
    {
        Schedule secondOfMinute = Schedule.HourOfDay(61, () => new DateTime(2022, 1, 1, 1, 0, 0));

        using var enumerator = secondOfMinute.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.FromHours(22), enumerator.Current);

        Assert.False(secondOfMinute.CanCount);
        Assert.Null(secondOfMinute.Count);
    }

    [Fact(DisplayName = "HourOfDay rounds hours less than 0 to 0")]
    public static void Case3()
    {
        Schedule secondOfMinute = Schedule.HourOfDay(0, () => new DateTime(2022, 1, 1, 1, 0, 0));

        using var enumerator = secondOfMinute.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.FromHours(23), enumerator.Current);

        Assert.False(secondOfMinute.CanCount);
        Assert.Null(secondOfMinute.Count);
    }
}
