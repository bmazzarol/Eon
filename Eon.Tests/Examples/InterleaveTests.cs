namespace Eon.Tests.Examples;

public static class InterleaveTests
{
    [Fact(DisplayName = "Interleave returns the durations from both schedules interleaved")]
    public static void Case1()
    {
        #region Example1
        Schedule interleave = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(3)
            .Interleave(Schedule.Forever);

        using var enumerator = interleave.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(0));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(0));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(0));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }
}
