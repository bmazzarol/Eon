namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// A <see cref="ScheduleTransformer"/> that repeats the <see cref="Schedule"/> a given number of `times`
    /// </summary>
    /// <param name="times">number of times to repeat the <see cref="Schedule"/></param>
    public static ScheduleTransformer Repeat(int times) => Transform(s => new SchRepeat(s, times));

    private sealed record SchRepeat(Schedule Schedule, int Times) : Schedule
    {
        public override int? Count => CanCount ? Schedule.Count * Times : null;
        public override bool CanCount => Schedule.CanCount;

        public override IEnumerator<Duration> GetEnumerator()
        {
            for (var i = 0; i < Times; i++)
            {
                using var enumerator = Schedule.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }
    }
}
