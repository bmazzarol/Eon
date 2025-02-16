using System.Collections;

namespace Eon.Tests;

public static class ScheduleTests
{
    [Fact(DisplayName = "Schedule can be infinite")]
    public static void Case1()
    {
        var schedule = Schedule.Forever;
        var take = schedule.Take(50);
        Assert.Equal(50, take.Count);
        Assert.All(take, x => Assert.Equal(Duration.Zero, x));
    }

    [Fact(DisplayName = "Schedule is an Enumerable")]
    public static void Case2()
    {
        var schedule = Schedule.Forever;
        Assert.True(schedule.GetEnumerator().MoveNext());
    }

    [Fact(DisplayName = "Schedule can be accessed by index")]
    public static void Case3()
    {
        var schedule = Schedule.Linear(100);
        Assert.Equal(100, schedule[0]);
        Assert.Equal(200, schedule[1]);
        Assert.Equal(300, schedule[2]);
        Assert.Equal(400, schedule[3]);
        Assert.Equal(500, schedule[4]);
        Assert.Equal(600, schedule[5]);
        Assert.Equal(600, schedule[5]);
        Assert.Equal(10100, schedule[100]);
        Assert.Equal(200, schedule[1]);
    }
}
