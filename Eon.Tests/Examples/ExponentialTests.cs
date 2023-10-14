namespace Eon.Tests.Examples;

public static class ExponentialTests
{
    [Fact(DisplayName = "Exponential returns a stream of the duration at a exponential rate")]
    public static void Case1()
    {
        #region Example1
        Schedule exponential = Schedule.Exponential(TimeSpan.FromSeconds(1)).Take(4);

        using var enumerator = exponential.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(8));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }

    [Fact(
        DisplayName = "Exponential returns a stream of the duration at a exponential rate with a custom factor"
    )]
    public static void Case2()
    {
        #region Example2
        Schedule exponential = Schedule.Exponential(TimeSpan.FromSeconds(1), factor: 3).Take(4);

        using var enumerator = exponential.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(5));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(14));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }
}
