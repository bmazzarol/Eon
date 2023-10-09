using System.Diagnostics.Contracts;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// <see cref="Schedule"/> constructor that recurs for the specified `durations`
    /// </summary>
    /// <param name="durations">durations to apply</param>
    [Pure]
    public static Schedule From(params Duration[] durations) => new SchFrom(durations);

    /// <summary>
    /// <see cref="Schedule"/> constructor that recurs for the specified `durations`
    /// </summary>
    /// <param name="durations">durations to apply</param>
    [Pure]
    public static Schedule From(IReadOnlyList<Duration> durations) => new SchFrom(durations);

    /// <summary>
    /// <see cref="Schedule"/> built from a provided set of <see cref="Items"/>
    /// </summary>
    private sealed record SchFrom(IReadOnlyList<Duration> Items) : Schedule
    {
        public override IEnumerator<Duration> GetEnumerator()
        {
            using var enumerator = Items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
}
