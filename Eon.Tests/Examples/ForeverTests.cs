﻿namespace Eon.Tests.Examples;

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
            duration.Should().Be(Duration.Zero);
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

        forever.CanCount.Should().BeFalse();
        forever.Count.Should().BeNull();

        result.Should().HaveCount(10).And.OnlyContain(x => x == Duration.Zero);

        #endregion
    }
}
