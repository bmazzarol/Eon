using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class RepeatTests
{
    [Fact(DisplayName = "Repeat repeats a schedule n number of times")]
    public static void Case1()
    {
        #region Example1

        Schedule repeat =
            Schedule.From(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
            & Schedule.Repeat(times: 2);

        #endregion

        repeat.RenderSchedule().SaveResults();
    }
}
