namespace Eon.Tests.Examples;

public static class HourOfDayTests
{
    [Fact(
        DisplayName = "HourOfDay is a cron like schedule that repeats every specified hour of each day"
    )]
    public static void Case1()
    {
        #region Example1

        var dateTimes = DateTimes();
        Schedule hourOfDay = Schedule.HourOfDay(
            3,
            () => dateTimes.MoveNext() ? dateTimes.Current : DateTimeOffset.Now
        );

        using var enumerator = hourOfDay.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromHours(2)); // 1 + 2 = 3
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromHours(23)); // 4 + 23 = 27 - 24 = 3
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromHours(21)); // 6 + 21 = 27 - 24 = 3
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromHours(24)); // 3 + 24 = 27 - 24 = 3
        enumerator.MoveNext().Should().BeTrue();

        hourOfDay.CanCount.Should().BeFalse();
        hourOfDay.Count.Should().BeNull();

        IEnumerator<DateTimeOffset> DateTimes()
        {
            yield return new DateTime(2022, 1, 1, 1, 0, 0);
            yield return new DateTime(2022, 1, 1, 4, 0, 0);
            yield return new DateTime(2022, 1, 1, 6, 0, 0);
            yield return new DateTime(2022, 1, 1, 3, 0, 0);
        }
        #endregion
    }

    [Fact(DisplayName = "HourOfDay rounds hours greater than 24 to 24")]
    public static void Case2()
    {
        Schedule secondOfMinute = Schedule.HourOfDay(61, () => new DateTime(2022, 1, 1, 1, 0, 0));

        using var enumerator = secondOfMinute.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromHours(22));

        secondOfMinute.CanCount.Should().BeFalse();
        secondOfMinute.Count.Should().BeNull();
    }

    [Fact(DisplayName = "HourOfDay rounds hours less than 0 to 0")]
    public static void Case3()
    {
        Schedule secondOfMinute = Schedule.HourOfDay(0, () => new DateTime(2022, 1, 1, 1, 0, 0));

        using var enumerator = secondOfMinute.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromHours(23));

        secondOfMinute.CanCount.Should().BeFalse();
        secondOfMinute.Count.Should().BeNull();
    }
}
