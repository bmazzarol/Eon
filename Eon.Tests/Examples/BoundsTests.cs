namespace Eon.Tests.Examples;

public static class BoundsTest
{
    [Fact(DisplayName = "LessThan ensures nothing greater than max delay is returned")]
    public static void Case1()
    {
        #region Example1
        Schedule lessThan =
            Schedule.Linear(TimeSpan.FromSeconds(1)) & Schedule.LessThan(TimeSpan.FromSeconds(3));

        using var enumerator = lessThan.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3)); // was 4 now its 3

        lessThan.CanCount.Should().BeFalse();
        lessThan.Count.Should().BeNull();

        #endregion
    }

    [Fact(DisplayName = "GreaterThan ensures nothing less than min delay is returned")]
    public static void Case2()
    {
        #region Example2
        Schedule greaterThan =
            Schedule.Linear(TimeSpan.FromSeconds(1))
            & Schedule.GreaterThan(TimeSpan.FromSeconds(2));

        using var enumerator = greaterThan.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2)); // was 1 now its 2
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));

        greaterThan.CanCount.Should().BeFalse();
        greaterThan.Count.Should().BeNull();

        #endregion
    }

    [Fact(
        DisplayName = "Between ensures nothing less than min or greater than max delay is returned"
    )]
    public static void Case3()
    {
        #region Example3
        Schedule between =
            Schedule.Linear(TimeSpan.FromSeconds(1))
            & Schedule.Between(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3));

        using var enumerator = between.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2)); // was 1 now its 2
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3)); // was 4 now its 3

        between.CanCount.Should().BeFalse();
        between.Count.Should().BeNull();

        #endregion
    }
}
