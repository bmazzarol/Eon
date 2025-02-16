using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class FixedTests
{
    [Fact(DisplayName = "Fixed returns durations enforcing a fixed schedule")]
    public static void Case1()
    {
        #region Example1
        DateTimeOffset now = DateTimeOffset.UtcNow;
        using var dates = new[]
        {
            now,
            now + TimeSpan.FromSeconds(6),
            now + TimeSpan.FromSeconds(1),
            now + TimeSpan.FromSeconds(4),
        }
            .AsEnumerable()
            .GetEnumerator();

        Schedule @fixed = Schedule.Fixed(
            TimeSpan.FromSeconds(5),
            currentTimeFunction: () => dates.MoveNext() ? dates.Current : now
        );

        #endregion

        @fixed.RenderSchedule().SaveResults();
    }
}
