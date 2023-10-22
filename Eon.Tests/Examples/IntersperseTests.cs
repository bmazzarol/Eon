namespace Eon.Tests.Examples;

public static class IntersperseTests
{
    [Fact(
        DisplayName = "Intersperse intersperses the emissions from a schedule between each emission from the first"
    )]
    public static void Case1()
    {
        #region Example1
        Schedule intersperse =
            Schedule.Linear(TimeSpan.FromSeconds(1)).Take(2)
            & Schedule.Intersperse(Schedule.From(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(4)));

        using var enumerator = intersperse.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeFalse();

        intersperse.CanCount.Should().BeTrue();
        intersperse.Count.Should().Be(6);

        #endregion
    }
}
