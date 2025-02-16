namespace Eon.Tests.Examples;

public static class NeverTests
{
    [Fact(DisplayName = "Never returns no duration values")]
    public static void Case1()
    {
        #region Example1

        Schedule never = Schedule.Never;

        using var enumerator = never.GetEnumerator();

        Assert.False(enumerator.MoveNext());

        Assert.True(never.CanCount);
        Assert.Equal(0, never.Count);

        #endregion
    }
}
