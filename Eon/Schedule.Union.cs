using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// <see cref="Union"/> of two <see cref="Schedule"/>
    /// </summary>
    /// <param name="first">first <see cref="Schedule"/></param>
    /// <param name="second">second <see cref="Schedule"/></param>
    /// <returns>union of the two <see cref="Schedule"/></returns>
    [Pure]
    public static Schedule operator |(Schedule first, Schedule second) => first.Union(second);

    /// <summary>
    /// <see cref="Union"/> of a <see cref="Schedule"/> and a <see cref="ScheduleTransformer"/>
    /// </summary>
    /// <param name="schedule"><see cref="Schedule"/></param>
    /// <param name="transformer"><see cref="ScheduleTransformer"/></param>
    /// <returns>union of a <see cref="Schedule"/> and a <see cref="ScheduleTransformer"/></returns>
    [Pure]
    public static Schedule operator |(Schedule schedule, ScheduleTransformer transformer) =>
        transformer.Apply(schedule);

    /// <summary>
    /// <see cref="Union"/> of a <see cref="ScheduleTransformer"/> and a <see cref="Schedule"/>
    /// </summary>
    /// <param name="transformer"><see cref="ScheduleTransformer"/></param>
    /// <param name="schedule"><see cref="Schedule"/></param>
    /// <returns>union of a <see cref="ScheduleTransformer"/> and a <see cref="Schedule"/></returns>
    [Pure]
    public static Schedule operator |(ScheduleTransformer transformer, Schedule schedule) =>
        transformer.Apply(schedule);

    /// <summary>
    /// Union of two <see cref="Schedule"/>.
    /// As long as any of the two <see cref="Schedule"/> are still running it will return the
    /// minimum <see cref="Duration"/>
    /// </summary>
    /// <param name="other">other <see cref="Schedule"/></param>
    /// <returns>union of `this` <see cref="Schedule"/> and the `other` <see cref="Schedule"/></returns>
    [Pure]
    public Schedule Union(Schedule other) => new SchUnion(this, other);

    /// <summary>
    /// Defines a <see cref="Schedule"/> that is the union of the <see cref="Left"/> and <see cref="Right"/>
    /// <see cref="Schedule"/>.
    /// As long as any of the two <see cref="Schedule"/> are still running it will return the
    /// minimum <see cref="Duration"/> of both <see cref="Left"/> or <see cref="Right"/>
    /// </summary>
    private sealed record SchUnion(Schedule Left, Schedule Right) : Schedule
    {
        public override IEnumerator<Duration> GetEnumerator()
        {
            using var left = Left.GetEnumerator();
            using var right = Right.GetEnumerator();

            var hasLeft = left.MoveNext();
            var hasRight = right.MoveNext();

            while (hasLeft || hasRight)
            {
                yield return hasLeft switch
                {
                    true when hasRight => Math.Min((double)left.Current, right.Current),
                    true => left.Current,
                    _ => right.Current
                };

                hasLeft = hasLeft && left.MoveNext();
                hasRight = hasRight && right.MoveNext();
            }
        }
    }
}
