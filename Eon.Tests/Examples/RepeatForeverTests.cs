namespace Eon.Tests.Examples;

public static class RepeatForeverTests
{
    [Fact(DisplayName = "RepeatForever repeats a schedule forever")]
    public static void Case1()
    {
        #region Example1
        Schedule repeatForever =
            Schedule.From(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
            & Schedule.RepeatForever;

        using var enumerator = repeatForever.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();

        #endregion
    }
}
