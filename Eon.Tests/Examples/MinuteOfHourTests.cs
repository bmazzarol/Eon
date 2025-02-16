using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class MinuteOfHourTests
{
    [Fact(
        DisplayName = "MinuteOfHour is a cron like schedule that repeats every specified minute of each hour"
    )]
    public static void Case1()
    {
        #region Example1

        DateTime[] dates =
        [
            new(2022, 1, 1, 1, 26, 0, DateTimeKind.Utc),
            new(2022, 1, 1, 1, 1, 0, DateTimeKind.Utc),
            new(2022, 1, 1, 1, 47, 0, DateTimeKind.Utc),
        ];

        Schedule minuteOfHour = Schedule.MinuteOfHour(2, dates.ToTestProvider());

        #endregion

        minuteOfHour.RenderSchedule(dates, "f").SaveResults();
    }

    [Fact(DisplayName = "MinuteOfHour rounds minutes greater than 59 to 59")]
    public static void Case2()
    {
        Schedule minuteOfHour = Schedule.MinuteOfHour(
            61,
            () => new DateTimeOffset(2022, 1, 1, 1, 26, 0, TimeSpan.Zero)
        );

        using var enumerator = minuteOfHour.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.FromMinutes(33), enumerator.Current); // 26 + 33 = 59

        Assert.False(minuteOfHour.CanCount);
        Assert.Null(minuteOfHour.Count);
    }

    [Fact(DisplayName = "MinuteOfHour rounds minutes less than 0 to 0")]
    public static void Case3()
    {
        Schedule minuteOfHour = Schedule.MinuteOfHour(
            0,
            () => new DateTimeOffset(2022, 1, 1, 1, 26, 0, TimeSpan.Zero)
        );

        using var enumerator = minuteOfHour.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.FromMinutes(34), enumerator.Current); // 26 + 34 = 60 - 60 = 0

        Assert.False(minuteOfHour.CanCount);
        Assert.Null(minuteOfHour.Count);
    }
}
