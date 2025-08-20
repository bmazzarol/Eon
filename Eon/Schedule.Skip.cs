using System.Diagnostics.CodeAnalysis;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Skip <paramref name="count"/> <see cref="Duration"/> values from this <see cref="Schedule"/>.
    /// </summary>
    /// <remarks>
    /// If <paramref name="count"/> is greater than the number of emissions from this <see cref="Schedule"/>, it emits nothing.
    /// </remarks>
    /// <param name="count">The number of emissions to skip.</param>
    /// <returns>A <see cref="Schedule"/> with <paramref name="count"/> <see cref="Duration"/> values skipped.</returns>
    public Schedule Skip(int count) => new SchSkip(this, count);

    /// <summary>
    /// Take all but the first <see cref="Duration"/> from the <see cref="Schedule"/>
    /// </summary>
    public Schedule Tail => new SchSkip(this, 1);

    /// <summary>
    /// Skip <paramref name="SkipOver"/> emissions from the <see cref="Schedule"/>.
    /// If the <see cref="Schedule"/> completes before <paramref name="SkipOver"/> emissions,
    /// it completes without emitting anything.
    /// </summary>
    /// <param name="Schedule">The source schedule.</param>
    /// <param name="SkipOver">The number of emissions to skip.</param>
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
