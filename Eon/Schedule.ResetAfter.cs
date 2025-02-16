using System.Diagnostics.CodeAnalysis;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Resets the <see cref="Schedule"/> after a provided cumulative `max` <see cref="Duration"/>
    /// </summary>
    /// <param name="max">`max` <see cref="Duration"/> to reset the schedule after</param>
    public static ScheduleTransformer ResetAfter(Duration max) =>
        Transform(s => new SchResetAfter(s, max));

    private sealed record SchResetAfter(Schedule Schedule, Duration Max) : Schedule
    {
        public override int? Count => null;
        public override bool CanCount => false;

        [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite")]
        public override IEnumerator<Duration> GetEnumerator()
        {
            while (true)
            {
                using var enumerator = (Schedule & MaxCumulativeDelay(Max)).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }
    }
}
