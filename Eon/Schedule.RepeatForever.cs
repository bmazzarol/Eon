using System.Diagnostics.CodeAnalysis;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// A <see cref="ScheduleTransformer"/> which repeats any <see cref="Schedule"/> forever
    /// </summary>
    public static readonly ScheduleTransformer RepeatForever = Transform(
        s => new SchRepeatForever(s)
    );

    /// <summary>
    /// Repeats <see cref="Schedule"/> forever
    /// </summary>
    /// <param name="Schedule">schedule to repeat forever</param>
    private sealed record SchRepeatForever(Schedule Schedule) : Schedule
    {
        public override int? Count => null;
        public override bool CanCount => false;

        [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite")]
        public override IEnumerator<Duration> GetEnumerator()
        {
            while (true)
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
