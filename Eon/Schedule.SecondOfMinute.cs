using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Cron-like <see cref="Schedule"/> that recurs every specified `second` of each minute
    /// </summary>
    /// <param name="second">second of the minute, will be rounded to fit between 1 and 59</param>
    /// <param name="currentTimeFunction">current time function</param>
    [Pure]
    public static Schedule SecondOfMinute(
        int second,
        Func<DateTimeOffset>? currentTimeFunction = null
    ) => new SchSecondOfMinute(second, currentTimeFunction);

    [Pure]
    private static int DurationToIntervalStart(
        int intervalStart,
        int currentIntervalPosition,
        int intervalWidth
    )
    {
        var steps = intervalStart - currentIntervalPosition;
        return steps > 0 ? steps : steps + intervalWidth;
    }

    [Pure]
    private static int RoundBetween(int value, int min, int max)
    {
        if (value > max)
            return max;
        if (value < min)
            return min;
        return value;
    }

    private sealed record SchSecondOfMinute(
        int Second,
        Func<DateTimeOffset>? CurrentTimeFunction = null
    ) : Schedule
    {
        public override int? Count => null;
        public override bool CanCount => false;

        [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite")]
        public override IEnumerator<Duration> GetEnumerator()
        {
            var now = CurrentTimeFunction ?? LiveCurrentTimeFunction;
            while (true)
            {
                yield return TimeSpan.FromSeconds(
                    DurationToIntervalStart(RoundBetween(Second, 1, 59), now().Second, 60)
                );
            }
        }
    }
}
