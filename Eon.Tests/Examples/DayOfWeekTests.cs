namespace Eon.Tests.Examples;

public static class DayOfWeekTests
{
    [Fact(
        DisplayName = "DayOfWeek is a cron like schedule that repeats every specified day of each week"
    )]
    public static void Case1()
    {
        #region Example1

        var dateTimes = DateTimes();
        Schedule hourOfDay = Schedule.DayOfWeek(
            DayOfWeek.Monday,
            () => dateTimes.MoveNext() ? dateTimes.Current : DateTimeOffset.Now
        );

        using var enumerator = hourOfDay.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromDays(2)); // Saturday + 2 Days = Monday
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromDays(7)); // Monday + 7 Days = Monday
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromDays(3)); // Friday + 3 Days = Monday
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromDays(5)); // Wednesday + 5 Days = Monday
        enumerator.MoveNext().Should().BeTrue();

        hourOfDay.CanCount.Should().BeFalse();
        hourOfDay.Count.Should().BeNull();

        IEnumerator<DateTimeOffset> DateTimes()
        {
            yield return new DateTime(2022, 1, 1, 0, 0, 0); // Saturday
            yield return new DateTime(2022, 1, 3, 0, 0, 0); // Monday
            yield return new DateTime(2022, 1, 7, 0, 0, 0); // Friday
            yield return new DateTime(2022, 1, 5, 0, 0, 0); // Wednesday
        }
        #endregion
    }
}
