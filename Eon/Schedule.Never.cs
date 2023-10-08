namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// <see cref="Schedule"/> that never runs returning nothing
    /// </summary>
    public static readonly Schedule Never = new SchNever();

    /// <summary>
    /// <see cref="Schedule"/> that never runs returning nothing
    /// </summary>
    private sealed record SchNever : Schedule
    {
        public override IEnumerator<Duration> GetEnumerator()
        {
            yield break;
        }
    }
}
