namespace Eon.Tests.Examples;

public static class MaxCumulativeDelayTests
{
    [Fact(DisplayName = "MaxCumulativeDelay enforces a total max delay")]
    public static void Case1()
    {
        #region Example1
        Schedule maxCumulativeDelay =
            Schedule.Linear(TimeSpan.FromSeconds(1)).Take(10)
            & Schedule.MaxCumulativeDelay(TimeSpan.FromSeconds(5));

        using var enumerator = maxCumulativeDelay.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2)); // was 3 now 2
        enumerator.MoveNext().Should().BeFalse();

        maxCumulativeDelay
            .Aggregate(Duration.Zero, (a, b) => a + b)
            .Should()
            .Be(TimeSpan.FromSeconds(5)); // total is 5

        maxCumulativeDelay.CanCount.Should().BeTrue();
        maxCumulativeDelay.Count.Should().Be(3); // was 10 now 3
        #endregion
    }
}
