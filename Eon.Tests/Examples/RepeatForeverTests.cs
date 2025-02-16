using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class RepeatForeverTests
{
    [Fact(DisplayName = "RepeatForever repeats a schedule forever")]
    public static void Case1()
    {
        #region Example1

        Schedule repeatForever =
            Schedule.From(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
            & Schedule.RepeatForever;

        #endregion

        repeatForever.RenderSchedule().SaveResults();
    }
}
