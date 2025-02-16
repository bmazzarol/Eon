namespace Eon.Tests.Examples;

public static class UpToTests
{
    [Fact(DisplayName = "UpTo returns duration zero until max duration time")]
    public static void Case1()
    {
        #region Example1

        DateTimeOffset now = DateTimeOffset.UtcNow;
        Schedule upTo = Schedule.UpTo(
            TimeSpan.FromSeconds(5),
            currentTimeFunction: () =>
            {
                now += TimeSpan.FromSeconds(1);
                return now;
            }
        );

        using var enumerator = upTo.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.Zero, enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.Zero, enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.Zero, enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.Zero, enumerator.Current);
        Assert.False(enumerator.MoveNext());

        Assert.False(upTo.CanCount);
        Assert.Null(upTo.Count);

        #endregion
    }

    [Fact(
        DisplayName = "LiveCurrentTimeFunction should return something close to the current time"
    )]
    public static void Case2()
    {
        Assert.Equal(
            DateTimeOffset.UtcNow,
            Schedule.LiveCurrentTimeFunction(),
            TimeSpan.FromSeconds(1)
        );
    }
}
