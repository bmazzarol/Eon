using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// A <see cref="Schedule"/> transformer that places a `max` ceiling on the returned delays
    /// </summary>
    /// <param name="max">max delay to return</param>
    [Pure]
    public static ScheduleTransformer LessThan(Duration max) =>
        Transform(s => s.Select(x => max < x ? max : x));

    /// <summary>
    /// A <see cref="Schedule"/> transformer that places a `min` floor on the returned delays
    /// </summary>
    /// <param name="min">min delay to return</param>
    [Pure]
    public static ScheduleTransformer GreaterThan(Duration min) =>
        Transform(s => s.Select(x => min > x ? min : x));

    /// <summary>
    /// A <see cref="Schedule"/> transformer that places a `min` floor and `max` ceiling on the returned delays
    /// </summary>
    /// <param name="min">min delay to return</param>
    /// <param name="max">max delay to return</param>
    [Pure]
    public static ScheduleTransformer Between(Duration min, Duration max) =>
        Transform(s =>
            s.Select(x =>
            {
                if (x < min)
                {
                    return min;
                }

                if (x > max)
                {
                    return max;
                }

                return x;
            })
        );
}
