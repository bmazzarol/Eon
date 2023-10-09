namespace Eon.Tests.Examples;

public static class SpacedTests
{
    [Fact(DisplayName = "Spaced returns a stream of the provided duration")]
    public static void Case1()
    {
        #region Example1
        Schedule spaced = Schedule.Spaced(TimeSpan.FromSeconds(2));

        using var enumerator = spaced.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));

        #endregion
    }
}
