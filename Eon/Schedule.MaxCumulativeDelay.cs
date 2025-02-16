namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// A <see cref="Schedule"/> transformer that places a `max` cumulative ceiling on the returned delays
    /// </summary>
    /// <param name="max">max cumulative delay</param>
    public static ScheduleTransformer MaxCumulativeDelay(Duration max) =>
        Transform(s => new SchMaxCumulativeDelay(s, max));

    private sealed record SchMaxCumulativeDelay(Schedule Schedule, Duration Max) : Schedule
    {
        public override int? Count => CanCount ? this.Aggregate(0, (i, _) => i + 1) : null;
        public override bool CanCount => Schedule.CanCount;

        public override IEnumerator<Duration> GetEnumerator()
        {
            var totalAppliedDelay = Duration.Zero;
            using var enumerator = Schedule.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (totalAppliedDelay >= Max)
                {
                    yield break;
                }
                totalAppliedDelay += enumerator.Current;

                if (totalAppliedDelay > Max)
                {
                    yield return enumerator.Current - (totalAppliedDelay - Max);
                }
                else
                {
                    yield return enumerator.Current;
                }
            }
        }
    }
}
