namespace Eon.Tests.Examples;

public static class FixedTests
{
    [Fact(DisplayName = "Fixed returns durations enforcing a fixed schedule")]
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

        Schedule @fixed = Schedule.Fixed(
            TimeSpan.FromSeconds(5),
            currentTimeFunction: () => dates.MoveNext() ? dates.Current : now
        );

        using var enumerator = @fixed.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));

        @fixed.CanCount.Should().BeFalse();
        @fixed.Count.Should().BeNull();

        #endregion
    }
}
