using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class SkipTests
{
    [Fact(DisplayName = "Skip skips count emissions from a schedule")]
    public static void Case1()
    {
        #region Example1

        Schedule skip = Schedule.Linear(TimeSpan.FromSeconds(1)).Take(4).Skip(2);

        #endregion

        skip.RenderSchedule().SaveResults();
    }
}
