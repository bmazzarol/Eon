using System.Collections;

namespace Eon.Tests;

public static class ScheduleTests
{
    [Fact(DisplayName = "Schedule can be infinite")]
    public static void Case1()
    {
        var schedule = Schedule.Forever;
        schedule.Take(50).Should().HaveCount(50).And.OnlyContain(x => x == Duration.Zero);
    }

    [Fact(DisplayName = "Schedule is an Enumerable")]
    public static void Case2()
    {
        var schedule = Schedule.Forever;
        ((IEnumerable)schedule).GetEnumerator().MoveNext().Should().BeTrue();
    }
}
