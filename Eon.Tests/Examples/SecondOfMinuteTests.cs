using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class SecondOfMinuteTests
{
    [Fact(
        DisplayName = "SecondOfMinute is a cron like schedule that repeats every specified second of each minute"
    )]
    public static void Case1()
    {
        #region Example1

        DateTime[] dates =
        [
            new(2022, 1, 1, 1, 1, 26, DateTimeKind.Utc), // 26
            new(2022, 1, 1, 1, 1, 1, DateTimeKind.Utc), // 1
            new(2022, 1, 1, 1, 1, 47, DateTimeKind.Utc), // 47
        ];

        Schedule secondOfMinute = Schedule.SecondOfMinute(2, dates.ToTestProvider());

        #endregion

        secondOfMinute.RenderSchedule(dates, "F").SaveResults();
    }

    [Fact(DisplayName = "SecondOfMinute rounds seconds greater than 59 to 59")]
    public static void Case2()
    {
        Schedule secondOfMinute = Schedule.SecondOfMinute(
            61,
            () => new DateTimeOffset(2022, 1, 1, 1, 1, 26, TimeSpan.Zero)
        );

        using var enumerator = secondOfMinute.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.FromSeconds(33), enumerator.Current); // 26 + 33 = 59

        Assert.False(secondOfMinute.CanCount);
        Assert.Null(secondOfMinute.Count);
    }

    [Fact(DisplayName = "SecondOfMinute rounds seconds less than 0 to 0")]
    public static void Case3()
    {
        Schedule secondOfMinute = Schedule.SecondOfMinute(
            0,
            () => new DateTimeOffset(2022, 1, 1, 1, 1, 26, TimeSpan.Zero)
        );

        using var enumerator = secondOfMinute.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.FromSeconds(34), enumerator.Current); // 26 + 34 = 60 - 60 = 0

        Assert.False(secondOfMinute.CanCount);
        Assert.Null(secondOfMinute.Count);
    }
}
