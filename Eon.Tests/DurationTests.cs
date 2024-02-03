namespace Eon.Tests;

public static class DurationTests
{
    [Fact(DisplayName = "Durations can be compared")]
    public static void Case1()
    {
        Duration one = TimeSpan.FromSeconds(1);
        Duration two = TimeSpan.FromSeconds(2);

        (one > two).Should().BeFalse();
        (one >= two).Should().BeFalse();
        (one <= two).Should().BeTrue();
        (one < two).Should().BeTrue();

        two.CompareTo(two).Should().Be(0);
#pragma warning disable CS1718
        (two >= two).Should().BeTrue();
        (two <= two).Should().BeTrue();
        (two < two).Should().BeFalse();
#pragma warning restore CS1718
        (two > one).Should().BeTrue();
        (two >= one).Should().BeTrue();
        (two <= one).Should().BeFalse();
        (two < one).Should().BeFalse();
    }

    [Fact(DisplayName = "Durations support equality")]
    public static void Case2()
    {
        Duration one = TimeSpan.FromSeconds(1);
        Duration two = TimeSpan.FromSeconds(2);

        (one != two).Should().BeTrue();
        (one == two).Should().BeFalse();
    }

    [Fact(DisplayName = "Durations do not support negative durations")]
    public static void Case3()
    {
        Assert.Throws<ArgumentException>(() => new Duration(TimeSpan.FromTicks(-1)));
    }

    [Fact(DisplayName = "Durations can be awaited")]
    public static async Task Case4()
    {
        await Duration.Zero;
        Assert.True(true);
    }

    [Fact(DisplayName = "Durations support ToString")]
    public static void Case5()
    {
        Duration.Zero.ToString().Should().Be("Duration(00:00:00)");
    }

    [Fact(DisplayName = "Durations support GetHashCode")]
    public static void Case6()
    {
        Duration.Zero.GetHashCode().Should().Be(((double)0).GetHashCode());
    }

    [Fact(DisplayName = "Durations can be random")]
    public static void Case7()
    {
        Duration.Random(1, 2).Should().BeGreaterOrEqualTo(1).And.BeLessThanOrEqualTo(2);
    }
}
