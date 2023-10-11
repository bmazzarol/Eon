namespace Eon.Tests.Examples;

public static class LinearTests
{
    [Fact(
        DisplayName = "Linear returns a stream of the duration at a linear rate from a starting seed"
    )]
    public static void Case1()
    {
        #region Example1
        Schedule linear = Schedule.Linear(TimeSpan.FromSeconds(1));

        using var enumerator = linear.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));

        #endregion
    }

    [Fact(
        DisplayName = "Linear returns a stream of the duration at a linear rate from a starting seed and factor"
    )]
    public static void Case2()
    {
        #region Example2
        Schedule linear = Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2);

        using var enumerator = linear.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(5));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(7));

        #endregion
    }
}
