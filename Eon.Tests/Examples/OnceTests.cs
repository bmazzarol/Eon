namespace Eon.Tests.Examples;

public static class OnceTests
{
    [Fact(DisplayName = "Once returns a single duration zero")]
    public static void Case1()
    {
        #region Example1

        Schedule once = Schedule.Once;

        using var enumerator = once.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(Duration.Zero, enumerator.Current);
        Assert.False(enumerator.MoveNext());

        Assert.True(once.CanCount);
        Assert.Equal(1, once.Count);

        #endregion
    }
}
