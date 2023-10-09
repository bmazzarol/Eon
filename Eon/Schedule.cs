using System.Collections;
using System.Diagnostics.Contracts;

namespace Eon;

/// <summary>
/// A <see cref="Schedule"/> is defined as a potentially infinite stream of <see cref="Duration"/>, combined with mechanisms for composing them.
/// </summary>
public abstract partial record Schedule : IEnumerable<Duration>
{
    /// <summary>
    /// Realise the underlying time-series of <see cref="Duration"/>
    /// </summary>
    /// <returns>The underlying time-series of <see cref="Duration"/></returns>
    [Pure]
    public abstract IEnumerator<Duration> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
