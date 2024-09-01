using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Interleave an emission from the `other` <see cref="Schedule"/>
    /// between each emission of `this` <see cref="Schedule"/> until either
    /// <see cref="Schedule"/> completes
    /// </summary>
    /// <param name="other">other <see cref="Schedule"/></param>
    /// <returns>interleaves the two <see cref="Schedule"/> together</returns>
    [Pure]
    public Schedule Interleave(Schedule other) => new SchInterleave(this, other);

    /// <summary>
    /// Interleaves an emission from the <see cref="Right"/> <see cref="Schedule"/> between each
    /// emission of the <see cref="Left"/> <see cref="Schedule"/> until either
    /// <see cref="Schedule"/> completes
    /// </summary>
    /// <param name="Left">left <see cref="Schedule"/></param>
    /// <param name="Right">right <see cref="Schedule"/></param>
    private sealed record SchInterleave(Schedule Left, Schedule Right) : Schedule
    {
        public override int? Count =>
            (Left.Count, Right.Count) switch
            {
                ({ } lCount, { } rCount) => Math.Min(lCount, rCount),
                ({ } lCount, _) => lCount,
                (_, { } rCount) => rCount,
                _ => null,
            } * 2;
        public override bool CanCount => Left.CanCount || Right.CanCount;

        public override IEnumerator<Duration> GetEnumerator()
        {
            using var left = Left.GetEnumerator();
            using var right = Right.GetEnumerator();
            while (left.MoveNext() && right.MoveNext())
            {
                yield return left.Current;
                yield return right.Current;
            }
        }
    }
}
