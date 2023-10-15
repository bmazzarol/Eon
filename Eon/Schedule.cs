using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Eon;

/// <summary>
/// A <see cref="Schedule"/> is defined as a potentially infinite stream of <see cref="Duration"/>, combined with mechanisms for composing them.
/// </summary>
public abstract partial record Schedule : IEnumerable<Duration>
{
    /// <summary>
    /// Returns the number of <see cref="Duration"/> emissions the <see cref="Schedule"/>
    /// will emit; or null if the <see cref="Schedule"/> is infinite.
    /// Call <see cref="CanCount"/> to determine if a non-null count will be returned
    /// </summary>
    public abstract int? Count { get; }

    /// <summary>
    /// Returns true if the <see cref="Schedule"/> is finite.
    /// False indicates the <see cref="Schedule"/> is infinite and <see cref="Count"/> will return null
    /// </summary>
    public abstract bool CanCount { get; }

    /// <summary>
    /// Realise the underlying time-series of <see cref="Duration"/>
    /// </summary>
    /// <returns>The underlying time-series of <see cref="Duration"/></returns>
    [Pure]
    public abstract IEnumerator<Duration> GetEnumerator();

    [ExcludeFromCodeCoverage]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
