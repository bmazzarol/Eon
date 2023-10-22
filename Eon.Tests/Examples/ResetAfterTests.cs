namespace Eon.Tests.Examples;

public static class ResetAfterTests
{
    [Fact(DisplayName = "ResetAfter repeats a schedule after a given duration")]
    public static void Case1()
    {
        #region Example1
        Schedule resetAfter =
            Schedule.Linear(TimeSpan.FromSeconds(1))
            & Schedule.ResetAfter(max: TimeSpan.FromSeconds(6));

        using var enumerator = resetAfter.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1)); // resets again
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();

        resetAfter.CanCount.Should().BeFalse();
        resetAfter.Count.Should().BeNull();

        #endregion
    }
}
