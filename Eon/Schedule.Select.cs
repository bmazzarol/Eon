using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Select operation for <see cref="Schedule"/> which transforms each emitted <see cref="Duration"/> into a new <see cref="Duration"/>
    /// </summary>
    /// <param name="projection">projection function from <see cref="Duration"/> to <see cref="Duration"/></param>
    /// <returns>transformed <see cref="Schedule"/></returns>
    [Pure]
    public Schedule Select(Func<Duration, Duration> projection) => new SchSelect(this, projection);

    /// <summary>
    /// Select operation for <see cref="Schedule"/> which transforms each emitted <see cref="Duration"/> and index into a new <see cref="Duration"/>
    /// </summary>
    /// <param name="projection">projection function from <see cref="Duration"/> index to <see cref="Duration"/></param>
    /// <returns>transformed <see cref="Schedule"/></returns>
    [Pure]
    public Schedule Select(Func<Duration, int, Duration> projection) =>
        new SchSelectIndex(this, projection);

    /// <summary>
    /// Applies the <see cref="Projection"/> for each emission from <see cref="Schedule"/>
    /// </summary>
    /// <param name="Schedule"><see cref="Schedule"/></param>
    /// <param name="Projection">projection from <see cref="Duration"/> to <see cref="Duration"/></param>
    private sealed record SchSelect(Schedule Schedule, Func<Duration, Duration> Projection)
        : Schedule
    {
        public override IEnumerator<Duration> GetEnumerator()
        {
            using var enumerator = Schedule.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return Projection(enumerator.Current);
            }
        }
    }

    /// <summary>
    /// Applies the <see cref="Projection"/> for each emission from <see cref="Schedule"/>
    /// </summary>
    /// <param name="Schedule"><see cref="Schedule"/></param>
    /// <param name="Projection">projection from <see cref="Duration"/> and index of <see cref="int"/> to <see cref="Duration"/></param>
    private sealed record SchSelectIndex(
        Schedule Schedule,
        Func<Duration, int, Duration> Projection
    ) : Schedule
    {
        public override IEnumerator<Duration> GetEnumerator()
        {
            using var enumerator = Schedule.GetEnumerator();
            for (var i = 0; enumerator.MoveNext(); i++)
            {
                yield return Projection(enumerator.Current, i);
            }
        }
    }
}
