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
        uint second,
        Func<DateTimeOffset>? currentTimeFunction = null
    ) => new SchSecondOfMinute(second, currentTimeFunction);

    private sealed record SchSecondOfMinute(
        uint Second,
        Func<DateTimeOffset>? CurrentTimeFunction = null
    ) : SchPositionWithinWindow(Second, CurrentTimeFunction)
    {
        protected override double Current(DateTimeOffset now) => (uint)now.Second;

        protected override uint Width => 60;

        protected override Duration DurationToPosition(double steps) => TimeSpan.FromSeconds(steps);
    }
}
