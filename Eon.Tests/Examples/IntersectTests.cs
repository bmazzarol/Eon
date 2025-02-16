using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class IntersectTests
{
    [Fact(DisplayName = "Intersect returns the max duration of either schedule")]
    public static void Case1()
    {
        #region Example1

        Schedule intersect = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Intersect(Schedule.Spaced(TimeSpan.FromSeconds(3)));

        #endregion

        intersect.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "Intersect operator returns the max duration of either schedule")]
    public static void Case2()
    {
        #region Example2

        Schedule intersect =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(4)
            & Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2);

        #endregion

        intersect.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "Intersect operator returns the shortest of the 2 schedules")]
    public static void Case3()
    {
        #region Example3

        Schedule leftSideLonger =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(4)
            & Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2).Take(2);

        Assert.Equal(2, leftSideLonger.Count);

        Schedule rightSideLonger =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(3)
            & Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2).Take(4);

        Assert.Equal(3, rightSideLonger.Count);

        #endregion
    }

    [Fact(DisplayName = "Intersect operator can be used with transformers")]
    public static void Case4()
    {
        #region Example4

        Schedule intersect1 =
            Schedule.Spaced(TimeSpan.FromSeconds(4)).Take(4) & Schedule.NoDelayOnFirst;

        Assert.Equal(4, intersect1.Count);
        Assert.Equal(TimeSpan.Zero, intersect1[0]);
        Assert.Equal(TimeSpan.FromSeconds(4), intersect1[1]);
        Assert.Equal(TimeSpan.FromSeconds(4), intersect1[2]);
        Assert.Equal(TimeSpan.FromSeconds(4), intersect1[3]);

        Schedule intersect2 =
            Schedule.NoDelayOnFirst & Schedule.Linear(TimeSpan.FromSeconds(1)).Take(4);

        Assert.Equal(4, intersect2.Count);
        Assert.Equal(TimeSpan.Zero, intersect2[0]);
        Assert.Equal(TimeSpan.FromSeconds(2), intersect2[1]);
        Assert.Equal(TimeSpan.FromSeconds(3), intersect2[2]);
        Assert.Equal(TimeSpan.FromSeconds(4), intersect2[3]);

        #endregion
    }

    [Fact(DisplayName = "Intersect can count is false if both schedules are infinite")]
    public static void Case5()
    {
        var interleave = Schedule.Linear(TimeSpan.FromSeconds(1)).Intersect(Schedule.Forever);
        Assert.False(interleave.CanCount);
        Assert.Null(interleave.Count);
    }

    [Fact(DisplayName = "Intersect can count is true if either schedule are finite")]
    public static void Case6()
    {
        var intersect = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Intersect(Schedule.Forever.Take(2));
        Assert.True(intersect.CanCount);
        Assert.Equal(2, intersect.Count);
        Assert.Equal(2, intersect.AsEnumerable().Count());

        intersect = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(4)
            .Intersect(Schedule.Forever.Take(2));
        Assert.True(intersect.CanCount);
        Assert.Equal(2, intersect.Count);
        Assert.Equal(2, intersect.AsEnumerable().Count());
    }
}
