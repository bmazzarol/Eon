using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class ExponentialTests
{
    [Fact(DisplayName = "Exponential returns a stream of the duration at a exponential rate")]
    public static void Case1()
    {
        #region Example1

        Schedule exponential = Schedule.Exponential(TimeSpan.FromSeconds(1)).Take(4);

        #endregion

        exponential.RenderSchedule().SaveResults();
    }

    [Fact(
        DisplayName = "Exponential returns a stream of the duration at a exponential rate with a custom factor"
    )]
    public static void Case2()
    {
        #region Example2

        Schedule exponential = Schedule.Exponential(TimeSpan.FromSeconds(1), factor: 3);

        #endregion

        exponential.RenderSchedule().SaveResults();
    }
}
