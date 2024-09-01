using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// <see cref="Intersect"/> of two <see cref="Schedule"/>
    /// </summary>
    /// <param name="first">first <see cref="Schedule"/></param>
    /// <param name="second">second <see cref="Schedule"/></param>
    /// <returns>intersect of the two <see cref="Schedule"/></returns>
    [Pure]
    public static Schedule operator &(Schedule first, Schedule second) => first.Intersect(second);

    /// <summary>
    /// <see cref="Intersect"/> of a <see cref="Schedule"/> and a <see cref="ScheduleTransformer"/>
    /// </summary>
    /// <param name="schedule"><see cref="Schedule"/></param>
    /// <param name="transformer"><see cref="ScheduleTransformer"/></param>
    /// <returns>intersect of a <see cref="Schedule"/> and a <see cref="ScheduleTransformer"/></returns>
    [Pure]
    public static Schedule operator &(Schedule schedule, ScheduleTransformer transformer) =>
        transformer.Apply(schedule);

    /// <summary>
    /// <see cref="Intersect"/> of a <see cref="ScheduleTransformer"/> and a <see cref="Schedule"/>
    /// </summary>
    /// <param name="transformer"><see cref="ScheduleTransformer"/></param>
    /// <param name="schedule"><see cref="Schedule"/></param>
    /// <returns>intersect of a <see cref="ScheduleTransformer"/> and a <see cref="Schedule"/></returns>
    [Pure]
    public static Schedule operator &(ScheduleTransformer transformer, Schedule schedule) =>
        transformer.Apply(schedule);

    /// <summary>
    /// Intersection of two <see cref="Schedule"/>.
    /// As long as both of the two <see cref="Schedule"/> are still running it will return the
    /// maximum <see cref="Duration"/>
    /// </summary>
    /// <param name="other">other <see cref="Schedule"/></param>
    /// <returns>intersection of `this` <see cref="Schedule"/> and the `other` <see cref="Schedule"/></returns>
    [Pure]
    public Schedule Intersect(Schedule other) => new SchIntersect(this, other);

    /// <summary>
    /// Defines a <see cref="Schedule"/> that is the intersect of the <see cref="Left"/> and <see cref="Right"/>
    /// <see cref="Schedule"/>.
    /// As long as both of the two <see cref="Schedule"/> are still running it will return the
    /// maximum <see cref="Duration"/> of both <see cref="Left"/> and <see cref="Right"/>
    /// </summary>
    private sealed record SchIntersect(Schedule Left, Schedule Right) : Schedule
    {
        public override int? Count =>
            (Left.Count, Right.Count) switch
            {
                ({ } lCount, { } rCount) => Math.Min(lCount, rCount),
                ({ } lCount, _) => lCount,
                (_, { } rCount) => rCount,
                _ => null,
            };
        public override bool CanCount => Left.CanCount || Right.CanCount;

        public override IEnumerator<Duration> GetEnumerator()
        {
            using var left = Left.GetEnumerator();
            using var right = Right.GetEnumerator();
            while (left.MoveNext() && right.MoveNext())
            {
                yield return Math.Max(left.Current, right.Current);
            }
        }
    }
}
