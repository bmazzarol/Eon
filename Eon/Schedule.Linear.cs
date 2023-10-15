using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    ///<see cref="Schedule"/> that recurs continuously using a linear backoff
    /// </summary>
    /// <param name="seed">seed</param>
    /// <param name="factor">optional factor to apply, defaults to 1</param>
    [Pure]
    public static Schedule Linear(Duration seed, double factor = 1) => new SchLinear(seed, factor);

    /// <summary>
    /// A <see cref="Schedule"/> that runs forever returning a linear increase in
    /// <see cref="Duration"/> starting from <see cref="Seed"/> by the provided
    /// <see cref="Factor"/>
    /// </summary>
    /// <param name="Seed">seed <see cref="Duration"/></param>
    /// <param name="Factor">factor to multiply by on each emission</param>
    private sealed record SchLinear(Duration Seed, double Factor) : Schedule
    {
        public override int? Count => null;
        public override bool CanCount => false;

        [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite")]
        public override IEnumerator<Duration> GetEnumerator()
        {
            Duration delayToAdd = Seed * Factor;
            var accumulator = Seed;
            yield return accumulator;
            while (true)
            {
                accumulator += delayToAdd;
                yield return accumulator;
            }
        }
    }
}
