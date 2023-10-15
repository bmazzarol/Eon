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

        interleave.CanCount.Should().BeTrue();
        interleave.Count.Should().Be(6);

        #endregion
    }

    [Fact(DisplayName = "Interleave can count is false if both schedules are infinite")]
    public static void Case2()
    {
        var interleave = Schedule.Linear(TimeSpan.FromSeconds(1)).Interleave(Schedule.Forever);
        interleave.CanCount.Should().BeFalse();
        interleave.Count.Should().BeNull();
    }

    [Fact(DisplayName = "Interleave can count is true if either schedule are finite")]
    public static void Case3()
    {
        var interleave = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Interleave(Schedule.Forever.Take(2));
        interleave.CanCount.Should().BeTrue();
        interleave.Count.Should().Be(4);
        interleave.AsEnumerable().Should().HaveCount(4);

        interleave = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(4)
            .Interleave(Schedule.Forever.Take(2));
        interleave.CanCount.Should().BeTrue();
        interleave.Count.Should().Be(4);
        interleave.AsEnumerable().Should().HaveCount(4);
    }
}
