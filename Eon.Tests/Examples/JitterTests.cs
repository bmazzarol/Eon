using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class JitterTest
{
    [Fact(DisplayName = "Jitter adds random jitter")]
    public static void Case1()
    {
        #region Example1

        Schedule jitter =
            Schedule.Spaced(TimeSpan.FromSeconds(1))
            & Schedule.Jitter(
                minRandom: TimeSpan.FromSeconds(1),
                maxRandom: TimeSpan.FromSeconds(2),
                seed: 12345678
            );

        #endregion

        jitter.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "Jitter adds random jitter using a fixed factor")]
    public static void Case2()
    {
        #region Example2

        Schedule jitter =
            Schedule.Linear(TimeSpan.FromSeconds(1)) & Schedule.Jitter(factor: 0.5, seed: 12345678);

        #endregion

        jitter.RenderSchedule().SaveResults();
    }
}
