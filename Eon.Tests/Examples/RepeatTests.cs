namespace Eon.Tests.Examples;

public static class RepeatTests
{
    [Fact(DisplayName = "Repeat repeats a schedule n number of times")]
    public static void Case1()
    {
        #region Example1
        Schedule repeat =
            Schedule.From(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
            & Schedule.Repeat(times: 2);

        using var enumerator = repeat.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeFalse();

        repeat.CanCount.Should().BeTrue();
        repeat.Count.Should().Be(4);

        #endregion
    }
}
