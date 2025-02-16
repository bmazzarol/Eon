namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Take `count` <see cref="Duration"/> from the <see cref="Schedule"/>
    /// </summary>
    /// <remarks>If `this` <see cref="Schedule"/> has less emissions than `count` it completes early</remarks>
    /// <param name="count">amount of <see cref="Duration"/> to take</param>
    /// <returns><see cref="Schedule"/> with `count` or less<see cref="Duration"/></returns>
    public Schedule Take(int count) => new SchTake(this, count);

    /// <summary>
    /// Takes <see cref="Count"/> elements from <see cref="Schedule"/>.
    /// If <see cref="Schedule"/> completes before <see cref="Count"/> emissions
    /// it completes
    /// </summary>
    private sealed record SchTake : Schedule
    {
        private Schedule Schedule { get; }
        public override int? Count { get; }

        public SchTake(Schedule Schedule, int Count)
        {
            this.Schedule = Schedule;
            this.Count = Count;
        }

        public override bool CanCount => true;

        public override IEnumerator<Duration> GetEnumerator()
        {
            using var enumerator = Schedule.GetEnumerator();
            for (var i = 0; i < Count && enumerator.MoveNext(); i++)
            {
                yield return enumerator.Current;
            }
        }
    }
}
