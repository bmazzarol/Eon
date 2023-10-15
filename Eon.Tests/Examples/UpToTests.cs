namespace Eon.Tests.Examples;

public static class UpToTests
{
    [Fact(DisplayName = "UpTo returns duration zero until max duration time")]
    public static void Case1()
    {
        #region Example1
        DateTimeOffset now = DateTimeOffset.UtcNow;
        Schedule upTo = Schedule.UpTo(
            TimeSpan.FromSeconds(5),
            currentTimeFunction: () =>
            {
                now += TimeSpan.FromSeconds(1);
                return now;
            }
        );

        using var enumerator = upTo.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeFalse();

        upTo.CanCount.Should().BeFalse();
        upTo.Count.Should().BeNull();

        #endregion
    }

    [Fact(
        DisplayName = "LiveCurrentTimeFunction should return something close to the current time"
    )]
    public static void Case2()
    {
        Schedule
            .LiveCurrentTimeFunction()
            .Should()
            .BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }
}
