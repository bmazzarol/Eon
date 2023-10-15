using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// <see cref="Schedule"/> that recurs continuously using a fibonacci based backoff
    /// </summary>
    /// <param name="seed">seed</param>
    [Pure]
    public static Schedule Fibonacci(Duration seed) => new SchFibonacci(seed);

    /// <summary>
    /// Defines a <see cref="Schedule"/> that recurs continuously emitting a fibonacci based sequence starting from <see cref="Seed"/>
    /// </summary>
    /// <param name="Seed">seed <see cref="Duration"/></param>
    private sealed record SchFibonacci(Duration Seed) : Schedule
    {
        public override int? Count => null;
        public override bool CanCount => false;

        [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite")]
        public override IEnumerator<Duration> GetEnumerator()
        {
            double last = Duration.Zero;
            double accumulator = Seed;
            yield return Seed;
            while (true)
            {
                var current = accumulator;
                accumulator += last;
                last = current;
                yield return accumulator;
            }
        }
    }
}
