using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Eon;

/// <summary>
/// Period of time (duration) in milliseconds.
/// It differs from <see cref="TimeSpan"/> in that it can never be negative
/// </summary>
public readonly struct Duration : IEquatable<Duration>, IComparable<Duration>
{
    /// <summary>
    /// Number of milliseconds the duration constitutes
    /// </summary>
    public readonly double Milliseconds;

    /// <summary>
    /// Duration constructor
    /// </summary>
    /// <param name="milliseconds">Magnitude of the duration.  Must be zero or a positive value</param>
    /// <exception cref="ArgumentException">Throws if `milliseconds` is less than `0`</exception>
    public Duration(double milliseconds)
    {
        if (milliseconds < 0)
        {
            throw new ArgumentException(
                $"{nameof(milliseconds)} must be a positive number.",
                nameof(milliseconds)
            );
        }
        Milliseconds = milliseconds;
    }

    /// <summary>
    /// Zero magnitude <see cref="Duration"/> (instant)
    /// </summary>
    public static readonly Duration Zero = new(0);

    /// <summary>
    /// Converts a <see cref="double"/> to a <see cref="Duration"/>
    /// </summary>
    /// <param name="milliseconds">milliseconds</param>
    /// <returns>duration</returns>
    [Pure]
    public static implicit operator Duration(double milliseconds) => new(milliseconds);

    /// <summary>
    /// Converts a <see cref="TimeSpan"/> to a <see cref="Duration"/>
    /// </summary>
    /// <param name="timeSpan">time span</param>
    /// <returns>duration</returns>
    [Pure]
    public static implicit operator Duration(TimeSpan timeSpan) => new(timeSpan.TotalMilliseconds);

    /// <summary>
    /// Converts a <see cref="Duration"/> to a <see cref="double"/>
    /// </summary>
    /// <param name="duration">duration</param>
    /// <returns>milliseconds</returns>
    [Pure]
    public static implicit operator double(in Duration duration) => duration.Milliseconds;

    /// <summary>
    /// Converts a <see cref="Duration"/> to a <see cref="TimeSpan"/>
    /// </summary>
    /// <param name="duration">duration</param>
    /// <returns>timespan</returns>
    [Pure]
    public static explicit operator TimeSpan(in Duration duration) =>
        TimeSpan.FromMilliseconds(duration.Milliseconds);

    /// <summary>
    /// Compares this instance to a specified <see cref="Duration"/> and returns
    /// an integer that indicates whether the value of this instance is less than (> 0), equal to (0), or greater than (0 >)
    /// the value of the specified <see cref="Duration"/>
    /// </summary>
    /// <param name="other">other <see cref="Duration"/></param>
    /// <returns>integer which is either less than 0, 0 or greater than 0</returns>
    [Pure]
    private int CompareToInternal(in Duration other) => Milliseconds.CompareTo(other);

    /// <inheritdoc />
    [Pure]
    public int CompareTo(Duration other) => CompareToInternal(other);

    /// <summary>
    /// Returns true if the `first` <see cref="Duration"/> is greater than the `second` <see cref="Duration"/>
    /// </summary>
    /// <param name="first">first <see cref="Duration"/></param>
    /// <param name="second">second <see cref="Duration"/></param>
    /// <returns>true if `first` is greater than `second`</returns>
    [Pure]
    public static bool operator >(in Duration first, in Duration second) =>
        first.CompareToInternal(second) > 0;

    /// <summary>
    /// Returns true if the `first` <see cref="Duration"/> is greater than or equal to the `second` <see cref="Duration"/>
    /// </summary>
    /// <param name="first">first <see cref="Duration"/></param>
    /// <param name="second">second <see cref="Duration"/></param>
    /// <returns>true if `first` is greater than or equal to the `second`</returns>
    [Pure]
    public static bool operator >=(in Duration first, in Duration second) =>
        first.CompareToInternal(second) >= 0;

    /// <summary>
    /// Returns true if the `first` <see cref="Duration"/> is less than the `second` <see cref="Duration"/>
    /// </summary>
    /// <param name="first">first <see cref="Duration"/></param>
    /// <param name="second">second <see cref="Duration"/></param>
    /// <returns>true if `first` is less than `second`</returns>
    [Pure]
    public static bool operator <(in Duration first, in Duration second) =>
        first.CompareToInternal(second) < 0;

    /// <summary>
    /// Returns true if the `first` <see cref="Duration"/> is less than or equal to the `second` <see cref="Duration"/>
    /// </summary>
    /// <param name="first">first <see cref="Duration"/></param>
    /// <param name="second">second <see cref="Duration"/></param>
    /// <returns>true if `first` is less than or equal to the `second`</returns>
    [Pure]
    public static bool operator <=(in Duration first, in Duration second) =>
        first.CompareToInternal(second) <= 0;

    /// <summary>
    /// Returns true if the `first` <see cref="Duration"/> equals the `second` <see cref="Duration"/>
    /// </summary>
    /// <param name="first">first <see cref="Duration"/></param>
    /// <param name="second">second <see cref="Duration"/></param>
    /// <returns>true if the `first` is equal to the `second`</returns>
    [Pure]
    public static bool operator ==(Duration first, Duration second) =>
        first.Milliseconds.Equals(second.Milliseconds);

    /// <summary>
    /// Returns true if the `first` <see cref="Duration"/> does not equal the `second` <see cref="Duration"/>
    /// </summary>
    /// <param name="first">first <see cref="Duration"/></param>
    /// <param name="second">second <see cref="Duration"/></param>
    /// <returns>true if the `first` is not equal to the `second`</returns>
    [Pure]
    public static bool operator !=(Duration first, Duration second) =>
        !first.Milliseconds.Equals(second.Milliseconds);

    /// <inheritdoc />
    [Pure]
    public bool Equals(Duration other) => Milliseconds.Equals(other);

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is Duration other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => Milliseconds.GetHashCode();

    /// <summary>
    /// Exposes an awaiter from <see cref="Task.Delay(int)"/> to allow direct await on a <see cref="Duration"/>
    /// </summary>
    /// <returns><see cref="Task.Delay(int)"/> awaiter</returns>
    public TaskAwaiter GetAwaiter() => Task.Delay((int)Milliseconds).GetAwaiter();

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"{nameof(Duration)}({(TimeSpan)this})";
}
