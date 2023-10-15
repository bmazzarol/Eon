using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    [Pure]
    private static Duration SecondsToIntervalStart(
        DateTimeOffset startTime,
        DateTimeOffset currentTime,
        Duration interval
    ) => interval - (currentTime - startTime).TotalMilliseconds % interval;

    /// <summary>
    /// <para><see cref="Schedule"/> that recurs on a fixed `interval`.</para>
    /// <para>
    /// If the action run between updates takes longer than the `interval`, then the
    /// action will be run immediately, but re-runs will not "pile up".
    /// </para>
    /// <para>
    ///     |-----interval-----|-----interval-----|-----interval-----|
    ///     |---------action--------||action|-----|action|-----------|
    /// </para>
    ///
    /// </summary>
    /// <param name="interval"><see cref="Schedule"/> interval</param>
    /// <param name="currentTimeFunction">current time function</param>
    [Pure]
    public static Schedule Fixed(
        Duration interval,
        Func<DateTimeOffset>? currentTimeFunction = null
    ) => new SchFixed(interval, currentTimeFunction);

    /// <summary>
    /// <para><see cref="Schedule"/> that recurs on a fixed <see cref="Interval"/>.</para>
    /// <para>
    /// If the action run between updates takes longer than the <see cref="Interval"/>, then the
    /// action will be run immediately, but re-runs will not "pile up".
    /// </para>
    /// <para>
    ///     |-----interval-----|-----interval-----|-----interval-----|
    ///     |---------action--------||action|-----|action|-----------|
    /// </para>
    ///
    /// </summary>
    /// <param name="Interval"><see cref="Schedule"/> interval</param>
    /// <param name="CurrentTimeFunction">current time function</param>
    private sealed record SchFixed(
        Duration Interval,
        Func<DateTimeOffset>? CurrentTimeFunction = null
    ) : Schedule
    {
        public override int? Count => null;
        public override bool CanCount => false;

        [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite")]
        public override IEnumerator<Duration> GetEnumerator()
        {
            var now = CurrentTimeFunction ?? LiveCurrentTimeFunction;
            var startTime = now();
            var lastRunTime = startTime;
            while (true)
            {
                var currentTime = now();
                var runningBehind = currentTime > lastRunTime + (TimeSpan)Interval;

                var boundary =
                    Interval == Duration.Zero
                        ? Interval
                        : SecondsToIntervalStart(startTime, currentTime, Interval);

                var sleepTime = boundary == Duration.Zero ? Interval : boundary;

                lastRunTime = runningBehind ? currentTime : currentTime + (TimeSpan)sleepTime;
                yield return runningBehind ? Duration.Zero : sleepTime;
            }
        }
    }
}
