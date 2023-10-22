namespace Eon.Tests.Examples;

public static class MinuteOfHourTests
{
    [Fact(
        DisplayName = "MinuteOfHour is a cron like schedule that repeats every specified minute of each hour"
    )]
    public static void Case1()
    {
        #region Example1

        var dateTimes = DateTimes();
        Schedule secondOfMinute = Schedule.MinuteOfHour(
            2,
            () => dateTimes.MoveNext() ? dateTimes.Current : DateTimeOffset.Now
        );

        using var enumerator = secondOfMinute.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromMinutes(36)); // 26 + 36 = 62 - 60 = 2
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromMinutes(1)); // 1 + 1 = 2
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromMinutes(15)); // 47 + 15 = 62 - 60 = 2
        enumerator.MoveNext().Should().BeTrue();

        secondOfMinute.CanCount.Should().BeFalse();
        secondOfMinute.Count.Should().BeNull();

        IEnumerator<DateTimeOffset> DateTimes()
        {
            yield return new DateTime(2022, 1, 1, 1, 26, 0);
            yield return new DateTime(2022, 1, 1, 1, 1, 0);
            yield return new DateTime(2022, 1, 1, 1, 47, 0);
        }
        #endregion
    }

    [Fact(DisplayName = "MinuteOfHour rounds seconds greater than 59 to 59")]
    public static void Case2()
    {
        Schedule secondOfMinute = Schedule.MinuteOfHour(
            61,
            () => new DateTime(2022, 1, 1, 1, 26, 0)
        );

        using var enumerator = secondOfMinute.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromMinutes(33)); // 26 + 33 = 59

        secondOfMinute.CanCount.Should().BeFalse();
        secondOfMinute.Count.Should().BeNull();
    }

    [Fact(DisplayName = "MinuteOfHour rounds seconds less than 1 to 1")]
    public static void Case3()
    {
        Schedule secondOfMinute = Schedule.MinuteOfHour(
            0,
            () => new DateTime(2022, 1, 1, 1, 26, 0)
        );

        using var enumerator = secondOfMinute.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromMinutes(35)); // 26 + 35 = 61 - 60 = 1

        secondOfMinute.CanCount.Should().BeFalse();
        secondOfMinute.Count.Should().BeNull();
    }
}
