namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Cron-like <see cref="Schedule"/> that recurs every specified `hour` of each day
    /// </summary>
    /// <param name="hour">hour of the day, will be rounded to fit between 0 and 23</param>
    /// <param name="currentTimeFunction">current time function</param>
    public static Schedule HourOfDay(uint hour, Func<DateTimeOffset>? currentTimeFunction = null) =>
        new SchHourOfDay(hour, currentTimeFunction);

    private sealed record SchHourOfDay(uint Hour, Func<DateTimeOffset>? CurrentTimeFunction = null)
        : SchPositionWithinWindow(Hour, CurrentTimeFunction)
    {
        protected override double Current(DateTimeOffset now) => (uint)now.Hour;

        protected override uint Width => 24;

        protected override Duration DurationToPosition(double steps) => TimeSpan.FromHours(steps);
    }
}
