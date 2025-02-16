using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class WindowedTests
{
    [Fact(DisplayName = "Windowed returns durations enforcing a windowed schedule")]
    public static void Case1()
    {
        #region Example1

        DateTimeOffset now = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero);
        using var dates = new[]
        {
            now,
            now + TimeSpan.FromSeconds(6),
            now + TimeSpan.FromSeconds(1),
            now + TimeSpan.FromSeconds(4),
        }
            .AsEnumerable()
            .GetEnumerator();

        Schedule windowed = Schedule.Windowed(
            TimeSpan.FromSeconds(5),
            currentTimeFunction: () => dates.MoveNext() ? dates.Current : now
        );

        #endregion

        windowed.RenderSchedule().SaveResults();
    }
}
