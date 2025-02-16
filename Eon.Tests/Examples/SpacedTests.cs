using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class SpacedTests
{
    [Fact(DisplayName = "Spaced returns a stream of the provided duration")]
    public static void Case1()
    {
        #region Example1

        Schedule spaced = Schedule.Spaced(TimeSpan.FromSeconds(2));

        #endregion

        spaced.RenderSchedule().SaveResults();
    }
}
