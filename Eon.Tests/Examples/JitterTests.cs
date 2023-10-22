namespace Eon.Tests.Examples;

public static class JitterTest
{
    [Fact(DisplayName = "Jitter adds random jitter")]
    public static void Case1()
    {
        #region Example1
        Schedule jitter =
            Schedule.Spaced(TimeSpan.FromSeconds(1))
            & Schedule.Jitter(
                minRandom: TimeSpan.FromSeconds(1),
                maxRandom: TimeSpan.FromSeconds(2),
                seed: 12345678
            );

        using var enumerator = jitter.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        ((TimeSpan)enumerator.Current)
            .Should()
            .BeCloseTo(TimeSpan.FromSeconds(1), precision: TimeSpan.FromSeconds(2))
            .And.NotBe(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        ((TimeSpan)enumerator.Current)
            .Should()
            .BeCloseTo(TimeSpan.FromSeconds(1), precision: TimeSpan.FromSeconds(2))
            .And.NotBe(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        ((TimeSpan)enumerator.Current)
            .Should()
            .BeCloseTo(TimeSpan.FromSeconds(1), precision: TimeSpan.FromSeconds(2))
            .And.NotBe(TimeSpan.FromSeconds(1));

        jitter.CanCount.Should().BeFalse();
        jitter.Count.Should().BeNull();

        #endregion
    }

    [Fact(DisplayName = "Jitter adds random jitter using a fixed factor")]
    public static void Case2()
    {
        #region Example2
        Schedule jitter = Schedule.Linear(TimeSpan.FromSeconds(1)) & Schedule.Jitter(factor: 0.5);

        using var enumerator = jitter.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        ((TimeSpan)enumerator.Current)
            .Should()
            .BeCloseTo(TimeSpan.FromSeconds(1), precision: TimeSpan.FromSeconds(1) * 0.5)
            .And.NotBe(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        ((TimeSpan)enumerator.Current)
            .Should()
            .BeCloseTo(TimeSpan.FromSeconds(2), precision: TimeSpan.FromSeconds(2) * 0.5)
            .And.NotBe(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        ((TimeSpan)enumerator.Current)
            .Should()
            .BeCloseTo(TimeSpan.FromSeconds(3), precision: TimeSpan.FromSeconds(3) * 0.5)
            .And.NotBe(TimeSpan.FromSeconds(3));

        jitter.CanCount.Should().BeFalse();
        jitter.Count.Should().BeNull();

        #endregion
    }
}
