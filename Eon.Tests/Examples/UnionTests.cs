using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class UnionTests
{
    [Fact(DisplayName = "Union returns the min duration of either schedule")]
    public static void Case1()
    {
        #region Example1

        Schedule union = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Union(Schedule.Spaced(TimeSpan.FromSeconds(3)));

        #endregion

        union.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "Union operator returns the min duration of either schedule")]
    public static void Case2()
    {
        #region Example2

        Schedule union =
            Schedule.Spaced(TimeSpan.FromSeconds(4))
            | Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2);

        #endregion

        union.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "Union operator returns the longest of the 2 schedules")]
    public static void Case3()
    {
        #region Example3

        Schedule leftSideLonger =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(4)
            | Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2).Take(2);

        Assert.Equal(4, leftSideLonger.Count);

        Schedule rightSideLonger =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(2)
            | Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2).Take(3);

        Assert.Equal(3, rightSideLonger.Count);

        #endregion
    }

    [Fact(DisplayName = "Union operator can be used with transformers")]
    public static void Case4()
    {
        #region Example4
        Schedule union1 =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(4) | Schedule.NoDelayOnFirst;

        Assert.Equal(4, union1.Count);
        Assert.Equal(Duration.Zero, union1[0]);
        Assert.Equal(TimeSpan.FromSeconds(4), union1[1]);
        Assert.Equal(TimeSpan.FromSeconds(4), union1[2]);
        Assert.Equal(TimeSpan.FromSeconds(4), union1[3]);

        Schedule union2 =
            Schedule.NoDelayOnFirst | Schedule.Linear(TimeSpan.FromSeconds(1)).Take(4);

        Assert.Equal(4, union2.Count);
        Assert.Equal(Duration.Zero, union2[0]);
        Assert.Equal(TimeSpan.FromSeconds(2), union2[1]);
        Assert.Equal(TimeSpan.FromSeconds(3), union2[2]);
        Assert.Equal(TimeSpan.FromSeconds(4), union2[3]);

        #endregion
    }

    [Fact(DisplayName = "Union can count is false if either schedules are infinite")]
    public static void Case5()
    {
        var union = Schedule.Forever.Union(Schedule.Forever);
        Assert.False(union.CanCount);
        Assert.Null(union.Count);

        var union2 = Schedule.Once.Union(Schedule.Forever);
        Assert.False(union2.CanCount);
        Assert.Null(union2.Count);

        var union3 = Schedule.Forever.Union(Schedule.Once);
        Assert.False(union3.CanCount);
        Assert.Null(union3.Count);
    }

    [Fact(DisplayName = "Union can count is true if both schedule are finite")]
    public static void Case6()
    {
        var union = Schedule.Once.Union(Schedule.Once);
        Assert.True(union.CanCount);
        Assert.Equal(1, union.Count);
    }
}
