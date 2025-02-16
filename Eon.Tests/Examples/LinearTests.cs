using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class LinearTests
{
    [Fact(
        DisplayName = "Linear returns a stream of the duration at a linear rate from a starting seed"
    )]
    public static void Case1()
    {
        #region Example1

        Schedule linear = Schedule.Linear(TimeSpan.FromSeconds(1));

        #endregion

        linear.RenderSchedule().SaveResults();
    }

    [Fact(
        DisplayName = "Linear returns a stream of the duration at a linear rate from a starting seed and factor"
    )]
    public static void Case2()
    {
        #region Example2

        Schedule linear = Schedule.Linear(TimeSpan.FromSeconds(1), factor: 2);

        #endregion

        linear.RenderSchedule().SaveResults();
    }
}
