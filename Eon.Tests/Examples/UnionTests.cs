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

        union.CanCount.Should().BeFalse();
        union.Count.Should().BeNull();

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

    [Fact(DisplayName = "Union operator can be used with transformers")]
    public static void Case4()
    {
        #region Example4
        Schedule union1 =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(4) | Schedule.NoDelayOnFirst;

        union1.Should().HaveCount(4).And.ContainInOrder(Duration.Zero, TimeSpan.FromSeconds(4));

        Schedule union2 =
            Schedule.NoDelayOnFirst | Schedule.Linear(TimeSpan.FromSeconds(1)).Take(4);

        union2
            .Should()
            .HaveCount(4)
            .And.ContainInOrder(Duration.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3));

        #endregion
    }

    [Fact(DisplayName = "Union can count is false if either schedules are infinite")]
    public static void Case5()
    {
        var union = Schedule.Forever.Union(Schedule.Forever);
        union.CanCount.Should().BeFalse();
        union.Count.Should().BeNull();
        var union2 = Schedule.Once.Union(Schedule.Forever);
        union2.CanCount.Should().BeFalse();
        union2.Count.Should().BeNull();
        var union3 = Schedule.Forever.Union(Schedule.Once);
        union3.CanCount.Should().BeFalse();
        union3.Count.Should().BeNull();
    }

    [Fact(DisplayName = "Union can count is true if both schedule are finite")]
    public static void Case6()
    {
        var union = Schedule.Once.Union(Schedule.Once);
        union.CanCount.Should().BeTrue();
        union.Count.Should().Be(1);
    }
}
