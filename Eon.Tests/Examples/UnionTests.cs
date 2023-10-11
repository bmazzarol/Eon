namespace Eon.Tests.Examples;

public static class UnionTests
{
    [Fact(DisplayName = "Union returns the min duration of either schedule")]
    public static void Case1()
    {
        #region Example1
        Schedule union = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Union(Schedule.Spaced(TimeSpan.FromSeconds(3)));

        using var enumerator = union.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));

        #endregion
    }

    [Fact(DisplayName = "Union operator returns the min duration of either schedule")]
    public static void Case2()
    {
        #region Example2
        Schedule union =
            Schedule.Spaced(TimeSpan.FromSeconds(4))
            | Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2);

        using var enumerator = union.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));

        #endregion
    }

    [Fact(DisplayName = "Union operator returns the longest of the 2 schedules")]
    public static void Case3()
    {
        #region Example3
        Schedule leftSideLonger =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(4)
            | Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2).Take(2);

        leftSideLonger.Should().HaveCount(4);

        Schedule rightSideLonger =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(2)
            | Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2).Take(3);

        rightSideLonger.Should().HaveCount(3);

        #endregion
    }
}
