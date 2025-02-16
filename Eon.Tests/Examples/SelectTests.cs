using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class SelectTests
{
    [Fact(DisplayName = "Select transforms durations in a schedule")]
    public static void Case1()
    {
        #region Example1

        Schedule select = Schedule.Linear(TimeSpan.FromSeconds(1)).Take(4).Select(x => x + x);

        #endregion

        select.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "Select with index transforms durations in a schedule")]
    public static void Case2()
    {
        #region Example2

        Schedule select = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(4)
            .Select((x, i) => x + (Duration)TimeSpan.FromSeconds(i));

        #endregion

        select.RenderSchedule().SaveResults();
    }
}
