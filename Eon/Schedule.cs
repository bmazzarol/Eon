using System.Diagnostics.Contracts;

namespace Eon;

/// <summary>
/// A <see cref="Schedule"/> is defined as a potentially infinite stream of <see cref="Duration"/>, combined with mechanisms for composing them.
/// </summary>
public abstract partial record Schedule
{
    /// <summary>
    /// Realise the underlying time-series of <see cref="Duration"/>
    /// </summary>
    /// <returns>The underlying time-series of <see cref="Duration"/></returns>
    [Pure]
    public abstract IEnumerator<Duration> GetEnumerator();

    /// <summary>
    /// Returns the <see cref="Schedule"/> as a <see cref="IEnumerable{T}"/> of
    /// <see cref="Duration"/>
    /// </summary>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="Duration"/></returns>
    [Pure]
    public IEnumerable<Duration> AsEnumerable()
    {
        using var enumerator = GetEnumerator();
        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;
        }
    }
}
