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

    [Fact(DisplayName = "Schedule can be accessed by index")]
    public static void Case3()
    {
        var schedule = Schedule.Linear(100);
        schedule[0].Should().Be(100);
        schedule[2].Should().Be(300);
        schedule[3].Should().Be(400);
        schedule[4].Should().Be(500);
        schedule[5].Should().Be(600);
        schedule[5].Should().Be(600);
        schedule[100].Should().Be(10100);
        schedule[1].Should().Be(200);
    }
}
