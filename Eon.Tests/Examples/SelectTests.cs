namespace Eon.Tests.Examples;

public static class SelectTests
{
    [Fact(DisplayName = "Select transforms durations in a schedule")]
    public static void Case1()
    {
        #region Example1
        Schedule select = Schedule.Linear(TimeSpan.FromSeconds(1)).Take(4).Select(x => x + x);

        using var enumerator = select.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(6));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(8));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }

    [Fact(DisplayName = "Select with index transforms durations in a schedule")]
    public static void Case2()
    {
        #region Example2
        Schedule select = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(4)
            .Select((x, i) => x + (Duration)TimeSpan.FromSeconds(i));

        using var enumerator = select.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(5));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(7));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }
}
