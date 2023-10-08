using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// <see cref="Schedule"/> that recurs continuously using a exponential backoff
    /// </summary>
    /// <param name="seed">seed</param>
    /// <param name="factor">optional factor to apply, defaults to 2</param>
    [Pure]
    public static Schedule Exponential(Duration seed, double factor = 2) =>
        new SchExponential(seed, factor);

    /// <summary>
    /// A <see cref="Schedule"/> that runs forever returning an exponential increase in
    /// <see cref="Duration"/> starting from <see cref="Seed"/> by the provided
    /// <see cref="Factor"/>
    /// </summary>
    /// <param name="Seed">seed <see cref="Duration"/></param>
    /// <param name="Factor">factor to multiply by on each emission</param>
    private sealed record SchExponential(Duration Seed, double Factor) : Schedule
    {
        [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite")]
        public override IEnumerator<Duration> GetEnumerator()
        {
            double seed = Seed;
            var accumulator = seed;
            yield return accumulator;
            for (var i = 0; ; i++)
            {
                var delayToAdd = seed * Math.Pow(Factor, i);
                accumulator += delayToAdd;
                yield return accumulator;
            }
        }
    }
}
