using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Prepend `value` <see cref="Duration"/> in-front of the rest of the <see cref="Schedule"/>
    /// </summary>
    /// <param name="value"><see cref="Duration"/> to prepend</param>
    /// <returns><see cref="Schedule"/> with the `value` <see cref="Duration"/> prepended</returns>
    [Pure]
    public Schedule Prepend(Duration value) => new SchPrepend(value, this);

    /// <summary>
    /// Prepends <see cref="Left"/> to the start of <see cref="Right"/>
    /// </summary>
    /// <param name="Left">left <see cref="Duration"/></param>
    /// <param name="Right">right <see cref="Schedule"/> to prepend to</param>
    private sealed record SchPrepend(Duration Left, Schedule Right) : Schedule
    {
        public override IEnumerator<Duration> GetEnumerator()
        {
            using var enumerator = Right.GetEnumerator();
            yield return Left;
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
}
