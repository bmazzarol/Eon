using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class DecorrelateTests
{
    [Fact(DisplayName = "Decorrelate adds random jitter in both directions")]
    public static void Case1()
    {
        #region Example1

        Schedule decorrelate =
            Schedule.Linear(TimeSpan.FromSeconds(1)).Take(2)
            & Schedule.Decorrelate(factor: 0.5, seed: 1234567);

        #endregion

        decorrelate.RenderSchedule().SaveResults();
    }
}
