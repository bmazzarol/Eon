namespace Eon.Tests;

public static class DurationTests
{
    [Fact(DisplayName = "Durations can be compared")]
    public static void Case1()
    {
        Duration one = TimeSpan.FromSeconds(1);
        Duration two = TimeSpan.FromSeconds(2);

        Assert.False(one > two);
        Assert.False(one >= two);
        Assert.True(one <= two);
        Assert.True(one < two);

        Assert.True(two.CompareTo(one) > 0);

#pragma warning disable CS1718, S1764
        Assert.True(two >= two);
        Assert.True(two <= two);
        Assert.False(two < two);
#pragma warning restore S1764, CS1718
        Assert.True(two > one);
        Assert.True(two >= one);
        Assert.False(two <= one);
        Assert.False(two < one);
    }

    [Fact(DisplayName = "Durations support equality")]
    public static void Case2()
    {
        Duration one = TimeSpan.FromSeconds(1);
        Duration two = TimeSpan.FromSeconds(2);

        Assert.True(one != two);
        Assert.False(one == two);
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
        Assert.Equal("Duration(00:00:00)", Duration.Zero.ToString());
    }

    [Fact(DisplayName = "Durations support GetHashCode")]
    public static void Case6()
    {
        Assert.Equal(((double)0).GetHashCode(), Duration.Zero.GetHashCode());
    }

    [Fact(DisplayName = "Durations can be random")]
    public static void Case7()
    {
        var random = Duration.Random(1, 2);
        Assert.True(random >= 1);
        Assert.True(random <= 2);
    }
}
