using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

#pragma warning disable S127, S1994

namespace Eon.Tests.Examples;

#region Example1

/// <summary>
/// <see cref="Schedule"/> that returns all the odd seconds up to <see cref="Max"/>
/// seconds
/// </summary>
/// <param name="Max">max seconds</param>
public sealed record OddSecondsToMax(uint Max) : Schedule
{
    public override int? Count => (int)Max;
    public override bool CanCount => true;

    public override IEnumerator<Duration> GetEnumerator()
    {
        for (int i = 1, j = 0; j < Max; i++)
        {
            if (i % 2 == 0)
            {
                continue;
            }

            j++;
            yield return TimeSpan.FromSeconds(i);
        }
    }
}

public static class OddSecondsToMaxTests
{
    [Fact(DisplayName = "returns all the odd seconds up to max seconds")]
    public static void Case1()
    {
        Schedule schedule = new OddSecondsToMax(10);

#endregion

        schedule.RenderSchedule().SaveResults();
    }
}
