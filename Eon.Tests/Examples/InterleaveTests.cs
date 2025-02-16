using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class InterleaveTests
{
    [Fact(DisplayName = "Interleave returns the durations from both schedules interleaved")]
    public static void Case1()
    {
        #region Example1

        Schedule interleave = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(3)
            .Interleave(Schedule.Forever);

        #endregion

        interleave.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "Interleave can count is false if both schedules are infinite")]
    public static void Case2()
    {
        var interleave = Schedule.Linear(TimeSpan.FromSeconds(1)).Interleave(Schedule.Forever);
        Assert.False(interleave.CanCount);
        Assert.Null(interleave.Count);
    }

    [Fact(DisplayName = "Interleave can count is true if either schedule are finite")]
    public static void Case3()
    {
        var interleave = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Interleave(Schedule.Forever.Take(2));
        Assert.True(interleave.CanCount);
        Assert.Equal(4, interleave.Count);
        Assert.Equal(4, interleave.AsEnumerable().Count());

        interleave = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(4)
            .Interleave(Schedule.Forever.Take(2));
        Assert.True(interleave.CanCount);
        Assert.Equal(4, interleave.Count);
        Assert.Equal(4, interleave.AsEnumerable().Count());
    }
}
