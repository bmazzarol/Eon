using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Cron-like <see cref="Schedule"/> that recurs every specified `day` of each week
    /// </summary>
    /// <param name="day">day of the week</param>
    /// <param name="currentTimeFunction">current time function</param>
    [Pure]
    public static Schedule DayOfWeek(
        DayOfWeek day,
        Func<DateTimeOffset>? currentTimeFunction = null
    ) => new SchDayOfWeek(day, currentTimeFunction);

    private sealed record SchDayOfWeek(
        DayOfWeek Day,
        Func<DateTimeOffset>? CurrentTimeFunction = null
    ) : SchPositionWithinWindow((uint)Day, CurrentTimeFunction)
    {
        protected override double Current(DateTimeOffset now) => (uint)now.DayOfWeek;

        protected override uint Width => 7;

        protected override Duration DurationToPosition(double steps) => TimeSpan.FromDays(steps);
    }
}
