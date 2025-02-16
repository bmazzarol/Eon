namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Transforms the <see cref="Schedule"/> by de-correlating each of the <see cref="Duration"/> both up and down in a jittered way
    /// </summary>
    /// <remarks>
    /// Given a <see cref="Linear"/> starting at 100. (100, 200, 300...)
    /// Adding de-correlation to it might produce a result like this, (103.2342, 97.123, 202.3213, 197.321...)
    /// The overall <see cref="Schedule"/> runs twice as long but should be less correlated when used in parallel.
    /// </remarks>
    /// <param name="factor">jitter factor based on the returned delay</param>
    /// <param name="seed">optional seed</param>
    public static ScheduleTransformer Decorrelate(double factor = 0.1, int? seed = null) =>
        Transform(s => new SchDecorrelate(s, factor, seed));

    private sealed record SchDecorrelate(Schedule Schedule, double Factor, int? Seed) : Schedule
    {
        public override int? Count => CanCount ? Schedule.Count * 2 : null;
        public override bool CanCount => Schedule.CanCount;

        public override IEnumerator<Duration> GetEnumerator()
        {
            using var enumerator = Schedule.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                yield return current + RandomFactory.Uniform(0, current * Factor, Seed);
                yield return current - RandomFactory.Uniform(0, current * Factor, Seed);
            }
        }
    }
}
