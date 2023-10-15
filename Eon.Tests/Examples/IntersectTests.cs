namespace Eon.Tests.Examples;

public static class IntersectTests
{
    [Fact(DisplayName = "Intersect returns the max duration of either schedule")]
    public static void Case1()
    {
        #region Example1
        Schedule intersect = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Intersect(Schedule.Spaced(TimeSpan.FromSeconds(3)));

        using var enumerator = intersect.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));

        intersect.CanCount.Should().BeFalse();
        intersect.Count.Should().BeNull();

        #endregion
    }

    [Fact(DisplayName = "Intersect operator returns the max duration of either schedule")]
    public static void Case2()
    {
        #region Example2
        Schedule intersect =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(4)
            & Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2);

        using var enumerator = intersect.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(5));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(7));
        enumerator.MoveNext().Should().BeFalse();

        intersect.CanCount.Should().BeTrue();
        intersect.Count.Should().Be(4);

        #endregion
    }

    [Fact(DisplayName = "Intersect operator returns the shortest of the 2 schedules")]
    public static void Case3()
    {
        #region Example3
        Schedule leftSideLonger =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(4)
            & Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2).Take(2);

        leftSideLonger.Should().HaveCount(2);

        Schedule rightSideLonger =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(3)
            & Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2).Take(4);

        rightSideLonger.Should().HaveCount(3);

        #endregion
    }

    [Fact(DisplayName = "Intersect operator can be used with transformers")]
    public static void Case4()
    {
        #region Example4
        Schedule intersect1 =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(4) & Schedule.NoDelayOnFirst;

        intersect1.Should().HaveCount(4).And.ContainInOrder(Duration.Zero, TimeSpan.FromSeconds(4));

        Schedule intersect2 =
            Schedule.NoDelayOnFirst & Schedule.Linear(TimeSpan.FromSeconds(1)).Take(4);

        intersect2
            .Should()
            .HaveCount(4)
            .And.ContainInOrder(Duration.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3));

        #endregion
    }

    [Fact(DisplayName = "Intersect can count is false if both schedules are infinite")]
    public static void Case5()
    {
        var interleave = Schedule.Linear(TimeSpan.FromSeconds(1)).Intersect(Schedule.Forever);
        interleave.CanCount.Should().BeFalse();
        interleave.Count.Should().BeNull();
    }

    [Fact(DisplayName = "Intersect can count is true if either schedule are finite")]
    public static void Case6()
    {
        var intersect = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Intersect(Schedule.Forever.Take(2));
        intersect.CanCount.Should().BeTrue();
        intersect.Count.Should().Be(2);
        intersect.AsEnumerable().Should().HaveCount(2);

        intersect = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(4)
            .Intersect(Schedule.Forever.Take(2));
        intersect.CanCount.Should().BeTrue();
        intersect.Count.Should().Be(2);
        intersect.AsEnumerable().Should().HaveCount(2);
    }
}
