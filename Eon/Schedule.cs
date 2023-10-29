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

    private readonly object _lock = new();
    private int _position;
    private Duration[] _buffer = Array.Empty<Duration>();
    private IEnumerator<Duration>? _enumerator;

    /// <summary>
    /// Provides `index` based access to a shared enumerated version of `this` <see cref="Schedule"/>.
    /// This will grow the internal buffer in a synchronized way by doubling the current
    /// length of the buffer when it is filled.
    /// </summary>
    /// <remarks>This can only ever be as large as <see cref="int.MaxValue"/></remarks>
    /// <remarks>This can be used to integrate <see cref="Schedule"/> into places that
    /// take delegate based back-off algorithms</remarks>
    /// <param name="index">index</param>
    /// <exception cref="IndexOutOfRangeException">when the <see cref="Schedule"/> has
    /// less emissions than the provided `index`</exception>
    public Duration this[int index]
    {
        get
        {
            lock (_lock)
            {
                TryFillBuffer(index);
                return _buffer[index];
            }
        }
    }

    private void TryFillBuffer(int index)
    {
        _enumerator ??= GetEnumerator();

        var currentLength = _position;
        if (currentLength != 0 && currentLength > index)
        {
            return;
        }

        var size = currentLength == 0 ? 4 : currentLength * 2;

        if (size <= index)
        {
            size = index + 1;
        }

        var newBuffer = new Duration[size];

        if (_buffer.Length != 0)
        {
            Array.Copy(_buffer, 0, newBuffer, 0, _buffer.Length);
        }

        for (; _position < size && _enumerator.MoveNext(); _position++)
        {
            newBuffer[_position] = _enumerator.Current;
        }

        _buffer = newBuffer;
    }
}
