using System.Diagnostics.CodeAnalysis;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// <see cref="Schedule"/> that runs forever returning <see cref="Duration.Zero"/>
    /// </summary>
    public static readonly Schedule Forever = new SchForever(Duration.Zero);

    /// <summary>
    /// <see cref="Schedule"/> that runs once returning <see cref="Duration.Zero"/>
    /// </summary>
    public static readonly Schedule Once = Forever.Take(1);

    /// <summary>
    /// <see cref="Schedule"/> that recurs continuously with the given spacing
    /// </summary>
    /// <param name="space">space</param>
    public static Schedule Spaced(Duration space) => new SchForever(space);

    /// <summary>
    /// Defines a <see cref="Schedule"/> that run forever returning <see cref="Duration"/>
    /// </summary>
    private sealed record SchForever(Duration Duration) : Schedule
    {
        public override int? Count => null;
        public override bool CanCount => false;

        [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite")]
        public override IEnumerator<Duration> GetEnumerator()
        {
            while (true)
            {
                yield return Duration;
            }
        }
    }
}
