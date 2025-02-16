using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class IntersperseTests
{
    [Fact(
        DisplayName = "Intersperse intersperses the emissions from a schedule between each emission from the first"
    )]
    public static void Case1()
    {
        #region Example1

        Schedule intersperse =
            Schedule.Linear(TimeSpan.FromSeconds(1)).Take(2)
            & Schedule.Intersperse(Schedule.From(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(4)));

        #endregion

        intersperse.RenderSchedule().SaveResults();
    }
}
