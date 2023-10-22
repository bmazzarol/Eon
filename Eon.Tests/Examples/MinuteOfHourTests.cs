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
        Schedule minuteOfHour = Schedule.MinuteOfHour(
            2,
            () => dateTimes.MoveNext() ? dateTimes.Current : DateTimeOffset.Now
        );

        using var enumerator = minuteOfHour.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromMinutes(36)); // 26 + 36 = 62 - 60 = 2
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromMinutes(1)); // 1 + 1 = 2
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromMinutes(15)); // 47 + 15 = 62 - 60 = 2
        enumerator.MoveNext().Should().BeTrue();

        minuteOfHour.CanCount.Should().BeFalse();
        minuteOfHour.Count.Should().BeNull();

        IEnumerator<DateTimeOffset> DateTimes()
        {
            yield return new DateTime(2022, 1, 1, 1, 26, 0);
            yield return new DateTime(2022, 1, 1, 1, 1, 0);
            yield return new DateTime(2022, 1, 1, 1, 47, 0);
        }
        #endregion
    }

    [Fact(DisplayName = "MinuteOfHour rounds minutes greater than 59 to 59")]
    public static void Case2()
    {
        Schedule minuteOfHour = Schedule.MinuteOfHour(61, () => new DateTime(2022, 1, 1, 1, 26, 0));

        using var enumerator = minuteOfHour.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromMinutes(33)); // 26 + 33 = 59

        minuteOfHour.CanCount.Should().BeFalse();
        minuteOfHour.Count.Should().BeNull();
    }

    [Fact(DisplayName = "MinuteOfHour rounds minutes less than 0 to 0")]
    public static void Case3()
    {
        Schedule minuteOfHour = Schedule.MinuteOfHour(0, () => new DateTime(2022, 1, 1, 1, 26, 0));

        using var enumerator = minuteOfHour.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromMinutes(34)); // 26 + 34 = 60 - 60 = 0

        minuteOfHour.CanCount.Should().BeFalse();
        minuteOfHour.Count.Should().BeNull();
    }
}
