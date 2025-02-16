using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class MaxCumulativeDelayTests
{
    [Fact(DisplayName = "MaxCumulativeDelay enforces a total max delay")]
    public static void Case1()
    {
        #region Example1

        Schedule maxCumulativeDelay =
            Schedule.Linear(TimeSpan.FromSeconds(1)).Take(10)
            & Schedule.MaxCumulativeDelay(TimeSpan.FromSeconds(5));

        #endregion

        maxCumulativeDelay.RenderSchedule(addRunningTotal: true).SaveResults();
    }
}
