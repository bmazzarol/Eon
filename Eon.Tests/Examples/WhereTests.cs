namespace Eon.Tests.Examples;

public static class WhereTests
{
    [Fact(DisplayName = "Where filters durations in a schedule")]
    public static void Case1()
    {
        #region Example1
        Schedule interleave = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(8)
            .Where(x => ((TimeSpan)x).Seconds % 2 == 0);

        using var enumerator = interleave.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(6));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(8));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }
}
