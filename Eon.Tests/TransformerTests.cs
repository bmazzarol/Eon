namespace Eon.Tests;

public static class TransformerTests
{
    [Fact(DisplayName = "Transformer converts to a schedule")]
    public static void Case1()
    {
        Schedule transformer = new ScheduleTransformer(_ => _);
        using var enumerator = transformer.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(Duration.Zero);
    }

    [Fact(DisplayName = "Transformers can be composed")]
    public static void Case2()
    {
        ScheduleTransformer transformer =
            new ScheduleTransformer(_ => _.Skip(1)) + new ScheduleTransformer(_ => _.Take(1));
        using var enumerator = transformer
            .Apply(Schedule.Linear(TimeSpan.FromSeconds(1)))
            .GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeFalse();
    }
}
