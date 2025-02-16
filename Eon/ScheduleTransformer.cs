namespace Eon;

/// <summary>
/// Transforms a <see cref="Schedule"/> into another <see cref="Schedule"/>
/// </summary>
public readonly struct ScheduleTransformer
{
    private readonly Func<Schedule, Schedule> _map;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="map">map function</param>
    public ScheduleTransformer(Func<Schedule, Schedule> map)
    {
        _map = map;
    }

    /// <summary>
    /// Apply a schedule to the transformer
    /// </summary>
    /// <param name="schedule">`Schedule` to run through the transformer</param>
    /// <returns>`Schedule` that has been run through the transformer</returns>
    public Schedule Apply(Schedule schedule)
    {
        return _map.Invoke(schedule) ?? schedule;
    }

    /// <summary>
    /// Compose the two <see cref="ScheduleTransformer"/> into one
    /// </summary>
    /// <param name="f">first <see cref="ScheduleTransformer"/> to run in the composition</param>
    /// <param name="g">second <see cref="ScheduleTransformer"/> to run in the composition</param>
    /// <returns>composition of the 2 <see cref="ScheduleTransformer"/></returns>
    public static ScheduleTransformer operator +(ScheduleTransformer f, ScheduleTransformer g) =>
        new(x => g.Apply(f.Apply(x)));

    /// <summary>
    /// Converts a <see cref="ScheduleTransformer"/> to a <see cref="Schedule"/>
    /// </summary>
    /// <remarks>this will feed a <see cref="Schedule.Forever"/> into the <see cref="ScheduleTransformer"/></remarks>
    /// <param name="t"><see cref="ScheduleTransformer"/></param>
    /// <returns><see cref="Schedule"/></returns>
    public static implicit operator Schedule(ScheduleTransformer t) => Schedule.Forever | t;
}
