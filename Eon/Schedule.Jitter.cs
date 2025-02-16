namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// A <see cref="ScheduleTransformer"/> that adds a random jitter to any returned <see cref="Duration"/>
    /// </summary>
    /// <param name="minRandom">min random milliseconds</param>
    /// <param name="maxRandom">max random milliseconds</param>
    /// <param name="seed">optional seed</param>
    public static ScheduleTransformer Jitter(
        Duration minRandom,
        Duration maxRandom,
        int? seed = null
    ) => Transform(s => s.Select(x => x + RandomFactory.Uniform(minRandom, maxRandom, seed)));

    /// <summary>
    /// A <see cref="ScheduleTransformer"/> that adds a random jitter to any returned <see cref="Duration"/>
    /// </summary>
    /// <param name="factor">jitter factor based on the returned delay</param>
    /// <param name="seed">optional seed</param>
    public static ScheduleTransformer Jitter(double factor = 0.5, int? seed = null) =>
        Transform(s => s.Select(x => x + RandomFactory.Uniform(0, x * factor, seed)));
}
