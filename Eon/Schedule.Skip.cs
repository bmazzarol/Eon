using System.Diagnostics.CodeAnalysis;

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
    public Schedule Skip(int count) => new SchSkip(this, count);

    /// <summary>
    /// Take all but the first <see cref="Duration"/> from the <see cref="Schedule"/>
    /// </summary>
    public Schedule Tail => new SchSkip(this, 1);

    /// <summary>
    /// Skip <see cref="SkipOver"/> elements from <see cref="Schedule"/>.
    /// If <see cref="Schedule"/> completes before <see cref="SkipOver"/> emissions
    /// it completes without emitting anything.
    /// </summary>
    /// <param name="Schedule">schedule</param>
    /// <param name="SkipOver">number of elements to take</param>
    private sealed record SchSkip(Schedule Schedule, int SkipOver) : Schedule
    {
        public override int? Count =>
            Schedule.Count.HasValue ? Math.Max(Schedule.Count.Value - SkipOver, 0) : null;
        public override bool CanCount => Schedule.CanCount;

        [SuppressMessage(
            "Critical Code Smell",
            "S1994:\"for\" loop increment clauses should modify the loops\' counters"
        )]
        public override IEnumerator<Duration> GetEnumerator()
        {
            using var enumerator = Schedule.GetEnumerator();
            for (var i = 0; enumerator.MoveNext(); i++)
            {
                if (i >= SkipOver)
                {
                    yield return enumerator.Current;
                }
            }
        }
    }
}
