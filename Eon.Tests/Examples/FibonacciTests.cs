namespace Eon.Tests.Examples;

public static class FibonacciTests
{
    [Fact(
        DisplayName = "Fibonacci returns a stream of the duration based on a fibonacci sequence from a starting seed"
    )]
    public static void Case1()
    {
        #region Example1
        Schedule fibonacci = Schedule.Fibonacci(TimeSpan.FromSeconds(1)).Take(5);

        using var enumerator = fibonacci.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(5));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }
}
