using Docfx.ResultSnippets;
using Eon.Tests.Extensions;
using Humanizer;

namespace Eon.Tests.Examples;

public static class AppendTests
{
    [Fact(DisplayName = "Append returns the durations from both")]
    public static void Case1()
    {
        #region Example1

        Schedule append = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(2)
            .Append(Schedule.Spaced(TimeSpan.FromSeconds(3)).Take(2));

        #endregion

        append.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "Append operator returns the durations from both")]
    public static void Case2()
    {
        #region Example2

        Schedule append =
            Schedule.Linear(TimeSpan.FromSeconds(1)).Take(2)
            + Schedule.Spaced(TimeSpan.FromSeconds(3)).Take(2);

        #endregion

        append.RenderSchedule().SaveResults();
    }
}
