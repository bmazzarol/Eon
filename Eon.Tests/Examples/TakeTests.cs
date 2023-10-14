namespace Eon.Tests.Examples;

public static class TakeTests
{
    [Fact(DisplayName = "Take limits the number of emissions from a schedule")]
    public static void Case1()
    {
        #region Example1
        Schedule take = Schedule.Forever.Take(2);

        using var enumerator = take.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }

    [Fact(DisplayName = "Recurs limits the number of emissions from a schedule")]
    public static void Case2()
    {
        #region Example2
        Schedule take = Schedule.Forever & Schedule.Recurs(2);

        using var enumerator = take.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }
}
