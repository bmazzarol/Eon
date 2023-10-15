namespace Eon.Tests.Examples;

public static class NeverTests
{
    [Fact(DisplayName = "Never returns no duration values")]
    public static void Case1()
    {
        #region Example1
        Schedule never = Schedule.Never;

        using var enumerator = never.GetEnumerator();
        enumerator.MoveNext().Should().BeFalse();

        never.CanCount.Should().BeTrue();
        never.Count.Should().Be(0);

        #endregion
    }
}
