using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class WhereTests
{
    [Fact(DisplayName = "Where filters durations in a schedule")]
    public static void Case1()
    {
        #region Example1

        Schedule where = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(8)
            .Where(x => ((TimeSpan)x).Seconds % 2 == 0);

        #endregion

        where.RenderSchedule().SaveResults();
    }
}
