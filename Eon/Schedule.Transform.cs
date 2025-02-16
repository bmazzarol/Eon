namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// <see cref="ScheduleTransformer"/> constructor which provides mapping capabilities for <see cref="Schedule"/> instances
    /// </summary>
    /// <param name="transform">transformation function</param>
    /// <returns><see cref="ScheduleTransformer"/></returns>
    public static ScheduleTransformer Transform(Func<Schedule, Schedule> transform) =>
        new(transform);

    /// <summary>
    /// A <see cref="ScheduleTransformer"/> that will enforce the first emission from the <see cref="Schedule"/> is a <see cref="Duration.Zero"/>
    /// </summary>
    public static readonly ScheduleTransformer NoDelayOnFirst = Transform(s =>
        s.Tail.Prepend(Duration.Zero)
    );

    /// <summary>
    /// A <see cref="ScheduleTransformer"/> that limits the schedule to run the specified number of `times`
    /// </summary>
    /// <param name="times">number of times to run the <see cref="Schedule"/></param>
    public static ScheduleTransformer Recurs(int times) => Transform(s => s.Take(times));
}
