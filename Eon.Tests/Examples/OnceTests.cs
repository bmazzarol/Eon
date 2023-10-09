namespace Eon.Tests.Examples;

public static class OnceTests
{
    [Fact(DisplayName = "Once returns a single duration zero")]
    public static void Case1()
    {
        #region Example1
        Schedule once = Schedule.Once;

        using var enumerator = once.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }
}
