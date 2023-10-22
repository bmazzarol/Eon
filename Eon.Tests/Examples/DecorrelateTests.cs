namespace Eon.Tests.Examples;

public static class DecorrelateTests
{
    [Fact(DisplayName = "Decorrelate adds random jitter in both directions")]
    public static void Case1()
    {
        #region Example1
        Schedule decorrelate =
            Schedule.Linear(TimeSpan.FromSeconds(1)).Take(2)
            & Schedule.Decorrelate(factor: 0.5, seed: 1234567);

        using var enumerator = decorrelate.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.ToString().Should().Be("Duration(00:00:01.3895529)");
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.ToString().Should().Be("Duration(00:00:00.6201061)");
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.ToString().Should().Be("Duration(00:00:02.2744626)");
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.ToString().Should().Be("Duration(00:00:01.8433547)");
        enumerator.MoveNext().Should().BeFalse();

        decorrelate.CanCount.Should().BeTrue();
        decorrelate.Count.Should().Be(4);

        #endregion
    }
}
