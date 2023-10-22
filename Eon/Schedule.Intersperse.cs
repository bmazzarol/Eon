using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// A <see cref="ScheduleTransformer"/> that intersperses the provided
    /// <see cref="Schedule"/>`s emissions between each emission in the <see cref="Schedule"/>
    /// </summary>
    /// <param name="schedule"><see cref="Schedule"/> to intersperse</param>
    [Pure]
    public static ScheduleTransformer Intersperse(Schedule schedule) =>
        Transform(s => s.SelectMany(schedule.Prepend));
}
