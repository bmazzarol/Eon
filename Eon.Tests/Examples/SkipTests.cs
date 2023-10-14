namespace Eon.Tests.Examples;

public static class SkipTests
{
    [Fact(DisplayName = "Skip skips count emissions from a schedule")]
    public static void Case1()
    {
        #region Example1
        Schedule skip = Schedule.Linear(TimeSpan.FromSeconds(1)).Take(4).Skip(2);

        using var enumerator = skip.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }
}
