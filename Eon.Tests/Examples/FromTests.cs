using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class FromTests
{
    [Fact(DisplayName = "Schedule can be built from an array")]
    public static void Case1()
    {
        #region Example1

        Schedule schedule = Schedule.From(
            TimeSpan.FromSeconds(2),
            TimeSpan.FromSeconds(4),
            TimeSpan.FromSeconds(6)
        );

        #endregion

        schedule.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "Schedule can be built from an array")]
    public static void Case2()
    {
        #region Example2

        Schedule schedule = Schedule.From(
            Enumerable.Range(1, 3).Select(x => (Duration)TimeSpan.FromSeconds(x)).ToList()
        );

        #endregion

        schedule.RenderSchedule().SaveResults();
    }
}
