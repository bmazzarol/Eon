namespace Eon.Tests;

public static class TransformerTests
{
    [Fact(DisplayName = "Transformer converts to a schedule")]
    public static void Case1()
    {
        Schedule transformer = new ScheduleTransformer(s => s);
        using var enumerator = transformer.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(Duration.Zero, enumerator.Current);
    }

    [Fact(DisplayName = "Transformers can be composed")]
    public static void Case2()
    {
        ScheduleTransformer transformer =
            new ScheduleTransformer(s => s.Skip(1)) + new ScheduleTransformer(s => s.Take(1));
        using var enumerator = transformer
            .Apply(Schedule.Linear(TimeSpan.FromSeconds(1)))
            .GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(TimeSpan.FromSeconds(2), enumerator.Current);
        Assert.False(enumerator.MoveNext());
    }
}
