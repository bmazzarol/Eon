namespace Eon.Tests.Examples;

public static class SecondOfMinuteTests
{
    [Fact(
        DisplayName = "SecondOfMinute is a cron like schedule that repeats every specified second of each minute"
    )]
    public static void Case1()
    {
        #region Example1

        var dateTimes = DateTimes();
        Schedule secondOfMinute = Schedule.SecondOfMinute(
            2,
            () => dateTimes.MoveNext() ? dateTimes.Current : DateTimeOffset.Now
        );

        using var enumerator = secondOfMinute.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(36)); // 26 + 36 = 62 - 60 = 2
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1)); // 1 + 1 = 2
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(15)); // 47 + 15 = 62 - 60 = 2
        enumerator.MoveNext().Should().BeTrue();

        secondOfMinute.CanCount.Should().BeFalse();
        secondOfMinute.Count.Should().BeNull();

        IEnumerator<DateTimeOffset> DateTimes()
        {
            yield return new DateTime(2022, 1, 1, 1, 1, 26);
            yield return new DateTime(2022, 1, 1, 1, 1, 1);
            yield return new DateTime(2022, 1, 1, 1, 1, 47);
        }
        #endregion
    }

    [Fact(DisplayName = "SecondOfMinute rounds seconds greater than 59 to 59")]
    public static void Case2()
    {
        Schedule secondOfMinute = Schedule.SecondOfMinute(
            61,
            () => new DateTime(2022, 1, 1, 1, 1, 26)
        );

        using var enumerator = secondOfMinute.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(33)); // 26 + 33 = 59

        secondOfMinute.CanCount.Should().BeFalse();
        secondOfMinute.Count.Should().BeNull();
    }

    [Fact(DisplayName = "SecondOfMinute rounds seconds less than 0 to 0")]
    public static void Case3()
    {
        Schedule secondOfMinute = Schedule.SecondOfMinute(
            0,
            () => new DateTime(2022, 1, 1, 1, 1, 26)
        );

        using var enumerator = secondOfMinute.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(34)); // 26 + 34 = 60 - 60 = 0

        secondOfMinute.CanCount.Should().BeFalse();
        secondOfMinute.Count.Should().BeNull();
    }
}
