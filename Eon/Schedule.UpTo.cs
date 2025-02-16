namespace Eon;

public abstract partial record Schedule
{
    internal static readonly Func<DateTimeOffset> LiveCurrentTimeFunction = () =>
        DateTimeOffset.UtcNow;

    /// <summary>
    /// <see cref="Schedule"/> that will continue to emmit <see cref="Duration.Zero"/>
    /// until `max` <see cref="Duration"/> has passed.
    /// </summary>
    /// <remarks>It behaves like a <see cref="Forever"/> with a deadline</remarks>
    /// <param name="max">max <see cref="Duration"/> to run the <see cref="Schedule"/> for</param>
    /// <param name="currentTimeFunction">current time function</param>
    public static Schedule UpTo(Duration max, Func<DateTimeOffset>? currentTimeFunction = null) =>
        new SchUpTo(max, currentTimeFunction);

    /// <summary>
    /// <see cref="Schedule"/> that will continue to emmit <see cref="Duration.Zero"/>
    /// until <see cref="Max"/> <see cref="Duration"/> has passed.
    /// </summary>
    private sealed record SchUpTo : Schedule
    {
        private readonly Func<DateTimeOffset> _now;
        public override int? Count => null;
        public override bool CanCount => false;
        private Duration Max { get; }

        public SchUpTo(Duration max, Func<DateTimeOffset>? currentTimeFunction = null)
        {
            Max = max;
            _now = currentTimeFunction ?? LiveCurrentTimeFunction;
        }

        public override IEnumerator<Duration> GetEnumerator()
        {
            var startTime = _now();
            while (_now() - startTime < Max)
            {
                yield return Duration.Zero;
            }
        }
    }
}
