namespace Eon.Tests.Examples;

public static class ForeverTests
{
    [Fact(DisplayName = "Forever be used as a looping construct")]
    public static void Case1()
    {
        #region Example1
        var count = 0;
        // Forever be used as a looping construct
        foreach (Duration duration in Schedule.Forever)
        {
            Assert.True(duration == Duration.Zero);
            count++;
            if (count == 10)
            {
                break;
            }
        }
        #endregion
        Assert.True(true);
    }

    [Fact(DisplayName = "Forever can be limited by take")]
    public static void Case2()
    {
        #region Example2

        Schedule forever = Schedule.Forever;
        Schedule result = forever.Take(10);

        Assert.False(forever.CanCount);
        Assert.Null(forever.Count);

        Assert.Contains(result, x => x == Duration.Zero);

        #endregion
    }
}
