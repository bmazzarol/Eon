namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Applies the `predicate` on each emitted <see cref="Duration"/> from `this` <see cref="Schedule"/> returning only those that pass
    /// </summary>
    /// <param name="predicate">predicate to apply to each <see cref="Duration"/> emitted</param>
    /// <returns>filtered <see cref="Schedule"/></returns>
    public Schedule Where(Func<Duration, bool> predicate) => new SchWhere(this, predicate);

    /// <summary>
    /// Applies the <see cref="Predicate"/> on each emitted <see cref="Duration"/> from <see cref="Schedule"/> returning only those that pass
    /// </summary>
    /// <param name="Schedule"><see cref="Schedule"/></param>
    /// <param name="Predicate">predicate to apply to each <see cref="Duration"/> emitted</param>
    private sealed record SchWhere(Schedule Schedule, Func<Duration, bool> Predicate) : Schedule
    {
        public override int? Count => CanCount ? Schedule.AsEnumerable().Count(Predicate) : null;
        public override bool CanCount => Schedule.CanCount;

        public override IEnumerator<Duration> GetEnumerator()
        {
            using var enumerator = Schedule.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (Predicate(current))
                {
                    yield return current;
                }
            }
        }
    }
}
