namespace Eon.Tests.Examples;

public static class WindowedTests
{
    [Fact(DisplayName = "Windowed returns durations enforcing a windowed schedule")]
    public static void Case1()
    {
        #region Example1
        DateTimeOffset now = DateTimeOffset.UtcNow;
        using var dates = new[]
        {
            now,
            now + TimeSpan.FromSeconds(6),
            now + TimeSpan.FromSeconds(1),
            now + TimeSpan.FromSeconds(4),
        }
            .AsEnumerable()
            .GetEnumerator();

        Schedule windowed = Schedule.Windowed(
            TimeSpan.FromSeconds(5),
            currentTimeFunction: () => dates.MoveNext() ? dates.Current : now
        );

        using var enumerator = windowed.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));

        windowed.CanCount.Should().BeFalse();
        windowed.Count.Should().BeNull();

        #endregion
    }
}
