using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// <para>
    /// A <see cref="Schedule"/> that divides the timeline into `interval`-long windows, and sleeps
    /// until the nearest window boundary every time it recurs.
    /// </para>
    /// <para>For example, 10 second `interval` would produce a schedule as follows:</para>
    /// <code>
    ///          10s        10s        10s       10s
    ///     |----------|----------|----------|----------|
    ///     |action------|sleep---|act|-sleep|action----|
    /// </code>
    /// </summary>
    /// <param name="interval"><see cref="Schedule"/> interval</param>
    /// <param name="currentTimeFunction">current time function</param>
    [Pure]
    public static Schedule Windowed(
        Duration interval,
        Func<DateTimeOffset>? currentTimeFunction = null
    ) => new SchWindowed(interval, currentTimeFunction);

    /// <summary>
    /// <para>
    /// A <see cref="Schedule"/> that divides the timeline into `interval`-long windows, and sleeps
    /// until the nearest window boundary every time it recurs.
    /// </para>
    /// <para>For example, 10 second <see cref="Interval"/> would produce a schedule as follows:</para>
    /// <code>
    ///          10s        10s        10s       10s
    ///     |----------|----------|----------|----------|
    ///     |action------|sleep---|act|-sleep|action----|
    /// </code>
    /// </summary>
    /// <param name="Interval"><see cref="Schedule"/> interval</param>
    /// <param name="CurrentTimeFunction">current time function</param>
    private sealed record SchWindowed(
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
            while (true)
            {
                var currentTime = now();
                yield return SecondsToIntervalStart(startTime, currentTime, Interval);
            }
        }
    }
}
