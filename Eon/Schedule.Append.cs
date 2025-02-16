namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Appends the `second` <see cref="Schedule"/> after the `first` <see cref="Schedule"/>
    /// </summary>
    /// <param name="first">first <see cref="Schedule"/></param>
    /// <param name="second">second <see cref="Schedule"/></param>
    /// <returns>the two <see cref="Schedule"/> appended</returns>
    public static Schedule operator +(Schedule first, Schedule second) => first.Append(second);

    /// <summary>
    /// Append the `other` <see cref="Schedule"/> to the end of `this` <see cref="Schedule"/>
    /// </summary>
    /// <param name="other">other <see cref="Schedule"/></param>
    /// <returns>the two <see cref="Schedule"/> appended</returns>
    public Schedule Append(Schedule other) => new SchAppend(this, other);

    /// <summary>
    /// Appends the <see cref="Right"/> <see cref="Schedule"/> to the end of the
    /// <see cref="Left"/> <see cref="Schedule"/>
    /// </summary>
    /// <param name="Left">left <see cref="Schedule"/></param>
    /// <param name="Right">right <see cref="Schedule"/></param>
    private sealed record SchAppend(Schedule Left, Schedule Right) : Schedule
    {
        public override int? Count => Left.Count + Right.Count;
        public override bool CanCount => Left.CanCount && Right.CanCount;

        public override IEnumerator<Duration> GetEnumerator()
        {
            using var left = Left.GetEnumerator();
            using var right = Right.GetEnumerator();
            while (left.MoveNext())
            {
                yield return left.Current;
            }
            while (right.MoveNext())
            {
                yield return right.Current;
            }
        }
    }
}
