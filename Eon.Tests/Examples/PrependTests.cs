namespace Eon.Tests.Examples;

public static class PrependTests
{
    [Fact(DisplayName = "prepend appends to the start of a schedule")]
    public static void Case1()
    {
        #region Example1
        Schedule prepend = Schedule.Never.Prepend(Duration.Zero);

        using var enumerator = prepend.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeFalse();

        prepend.CanCount.Should().BeTrue();
        prepend.Count.Should().Be(1);

        #endregion
    }
}
