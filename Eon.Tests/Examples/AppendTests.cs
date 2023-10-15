namespace Eon.Tests.Examples;

public static class AppendTests
{
    [Fact(DisplayName = "Append returns the durations from both")]
    public static void Case1()
    {
        #region Example1
        Schedule append = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(2)
            .Append(Schedule.Spaced(TimeSpan.FromSeconds(3)).Take(2));

        using var enumerator = append.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeFalse();

        append.CanCount.Should().BeTrue();
        append.Count.Should().Be(4);

        #endregion
    }

    [Fact(DisplayName = "Append operator returns the durations from both")]
    public static void Case2()
    {
        #region Example2
        Schedule append =
            Schedule.Linear(TimeSpan.FromSeconds(1)).Take(2)
            + Schedule.Spaced(TimeSpan.FromSeconds(3)).Take(2);

        using var enumerator = append.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }
}
