using System.Diagnostics.CodeAnalysis;

namespace Eon;

internal static class RandomFactory
{
    private static readonly ThreadLocal<Random> SharedRandom = new();
    private static readonly ThreadLocal<Dictionary<int, Random>> SeededRandom = new();

    private static Random Instance(int? seed)
    {
        if (seed == null)
        {
            SharedRandom.Value ??= new Random();
            return SharedRandom.Value;
        }

        SeededRandom.Value ??= new Dictionary<int, Random>();
        var seededRandoms = SeededRandom.Value;
        if (!seededRandoms.ContainsKey(seed.Value))
        {
            seededRandoms[seed.Value] = new Random(seed.Value);
        }

        return seededRandoms[seed.Value];
    }

    /// <summary>
    /// Returns a random floating-point number that is greater than or equal to `a` and less than `b`
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static double Uniform(double a, double b, int? seed = null)
    {
        if (a.Equals(b))
        {
            return a;
        }
        var random = Instance(seed);
        return a + (b - a) * random.NextDouble();
    }
}
