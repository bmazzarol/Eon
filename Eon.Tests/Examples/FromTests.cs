namespace Eon.Tests.Examples;

public static class FromTests
{
    [Fact(DisplayName = "Schedule can be built from an array")]
    public static void Case1()
    {
        #region Example1
        Schedule schedule = Schedule.From(
            TimeSpan.FromSeconds(2),
            TimeSpan.FromSeconds(4),
            TimeSpan.FromSeconds(6)
        );

        schedule.CanCount.Should().BeTrue();
        schedule.Count.Should().Be(3);

        schedule
            .Should()
            .HaveCount(3)
            .And.ContainInOrder(
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(4),
                TimeSpan.FromSeconds(6)
            );

        #endregion
    }

    [Fact(DisplayName = "Schedule can be built from an array")]
    public static void Case2()
    {
        #region Example2
        Schedule schedule = Schedule.From(
            Enumerable.Range(1, 3).Select(x => (Duration)TimeSpan.FromSeconds(x)).ToList()
        );

        schedule
            .Should()
            .HaveCount(3)
            .And.ContainInOrder(
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3)
            );

        #endregion
    }
}
