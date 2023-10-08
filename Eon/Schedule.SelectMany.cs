using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Applies the `bind` for each emission from `this` <see cref="Schedule"/> which then emits all
    /// <see cref="Duration"/> from that returned <see cref="Schedule"/>.
    /// </summary>
    /// <param name="bind">projection from <see cref="Duration"/> to <see cref="Schedule"/></param>
    /// <returns>chained <see cref="Schedule"/></returns>
    [Pure]
    public Schedule SelectMany(Func<Duration, Schedule> bind) =>
        new SchSelectMany(this, bind, static (_, duration) => duration);

    /// <summary>
    /// Applies the `bind` for each emission from `this` <see cref="Schedule"/> which then emits all
    /// <see cref="Duration"/> from that returned <see cref="Schedule"/>.
    /// `projection` is then called for each pair of <see cref="Duration"/> emitted
    /// </summary>
    /// <param name="bind">projection from <see cref="Duration"/> to <see cref="Schedule"/></param>
    /// <param name="projection"></param>
    /// <returns>chained <see cref="Schedule"/>projection from <see cref="Duration"/>, <see cref="Duration"/> to <see cref="Duration"/></returns>
    [Pure]
    public Schedule SelectMany(
        Func<Duration, Schedule> bind,
        Func<Duration, Duration, Duration> projection
    ) => new SchSelectMany(this, bind, projection);

    /// <summary>
    /// Applies the <see cref="Bind"/> for each emission from <see cref="Schedule"/> which then emits all
    /// <see cref="Duration"/> from that returned <see cref="Schedule"/>.
    /// <see cref="Projection"/> is then called for each pair of <see cref="Duration"/> emitted
    /// </summary>
    /// <param name="Schedule"><see cref="Schedule"/></param>
    /// <param name="Bind">projection from <see cref="Duration"/> to <see cref="Schedule"/></param>
    /// <param name="Projection">projection from <see cref="Duration"/>, <see cref="Duration"/> to <see cref="Duration"/></param>
    private sealed record SchSelectMany(
        Schedule Schedule,
        Func<Duration, Schedule> Bind,
        Func<Duration, Duration, Duration> Projection
    ) : Schedule
    {
        public override IEnumerator<Duration> GetEnumerator()
        {
            using var enumerator = Schedule.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                using var childEnumerator = Bind(current).GetEnumerator();
                while (childEnumerator.MoveNext())
                {
                    yield return Projection(current, childEnumerator.Current);
                }
            }
        }
    }
}
