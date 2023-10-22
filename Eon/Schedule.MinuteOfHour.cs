using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Cron-like  <see cref="Schedule"/> that recurs every specified `minute` of each hour
    /// </summary>
    /// <param name="minute">minute of the hour, will be rounded to fit between 1 and 59</param>
    /// <param name="currentTimeFunction">current time function</param>
    [Pure]
    public static Schedule MinuteOfHour(
        uint minute,
        Func<DateTimeOffset>? currentTimeFunction = null
    ) => new SchMinuteOfHour(minute, currentTimeFunction);

    private sealed record SchMinuteOfHour(
        uint Minute,
        Func<DateTimeOffset>? CurrentTimeFunction = null
    ) : SchPositionWithinWindow(Minute, CurrentTimeFunction)
    {
        protected override double Current(DateTimeOffset now) => (uint)now.Minute;

        protected override uint Width => 60;

        protected override Duration DurationToPosition(double steps) => TimeSpan.FromMinutes(steps);
    }
}
