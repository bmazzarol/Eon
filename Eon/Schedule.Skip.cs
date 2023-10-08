using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Skip `count` <see cref="Duration"/> from the `this` <see cref="Schedule"/>
    /// </summary>
    /// <remarks>If `count` is greater than the number of emissions from `this` <see cref="Schedule"/>
    /// it will emmit nothing</remarks>
    /// <param name="count">`count` emissions to skip</param>
    /// <returns><see cref="Schedule"/> with `count` <see cref="Duration"/> skipped</returns>
    [Pure]
    public Schedule Skip(int count) => new SchSkip(this, count);

    /// <summary>
    /// Take all but the first <see cref="Duration"/> from the <see cref="Schedule"/>
    /// </summary>
    [Pure]
    public Schedule Tail => new SchSkip(this, 1);

    /// <summary>
    /// Skip <see cref="Count"/> elements from <see cref="Schedule"/>.
    /// If <see cref="Schedule"/> completes before <see cref="Count"/> emissions
    /// it completes without emitting anything.
    /// </summary>
    /// <param name="Schedule">schedule</param>
    /// <param name="Count">number of elements to take</param>
    private sealed record SchSkip(Schedule Schedule, int Count) : Schedule
    {
        public override IEnumerator<Duration> GetEnumerator()
        {
            using var enumerator = Schedule.GetEnumerator();
            for (var i = 0; enumerator.MoveNext(); i++)
            {
                if (i >= Count)
                {
                    yield return enumerator.Current;
                }
            }
        }
    }
}
