namespace Eon.Tests;

public static class ScheduleTests
{
    [Fact(DisplayName = "Schedule can be infinite")]
    public static void Case1()
    {
        var schedule = Schedule.Forever;
        schedule.Take(50).Should().HaveCount(50).And.OnlyContain(x => x == Duration.Zero);
    }
}
