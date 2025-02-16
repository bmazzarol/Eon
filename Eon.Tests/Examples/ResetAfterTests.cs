using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class ResetAfterTests
{
    [Fact(DisplayName = "ResetAfter repeats a schedule after a given duration")]
    public static void Case1()
    {
        #region Example1

        Schedule resetAfter =
            Schedule.Linear(TimeSpan.FromSeconds(1))
            & Schedule.ResetAfter(max: TimeSpan.FromSeconds(6));

        #endregion

        resetAfter.RenderSchedule(addRunningTotal: true).SaveResults();
    }
}
