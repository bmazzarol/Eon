namespace Eon.Tests.Examples;

public static class SelectManyTests
{
    [Fact(DisplayName = "SelectMany transforms durations in a schedule")]
    public static void Case1()
    {
        #region Example1
        Schedule selectMany = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(2)
            .SelectMany(
                bind: _ => Schedule.From(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1)),
                projection: (first, second) => first + second
            );
        selectMany.Should().HaveCount(4);
        using var enumerator = selectMany.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }

    [Fact(DisplayName = "SelectMany LINQ expression transforms durations in a schedule")]
    public static void Case2()
    {
        #region Example2

        Schedule selectMany =
            from linear in Schedule.Linear(TimeSpan.FromSeconds(1)).Take(2)
            from twoThenOne in Schedule.From(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1))
            select linear + twoThenOne;

        selectMany.Should().HaveCount(4);
        using var enumerator = selectMany.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(4));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(3));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }

    [Fact(
        DisplayName = "SelectMany transforms durations in a schedule ignoring the results from the first"
    )]
    public static void Case3()
    {
        #region Example3

        Schedule selectMany = Schedule
            .Linear(TimeSpan.FromSeconds(1))
            .Take(2)
            .SelectMany(_ => Schedule.From(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1)));

        selectMany.Should().HaveCount(4);
        using var enumerator = selectMany.GetEnumerator();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(2));
        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(TimeSpan.FromSeconds(1));
        enumerator.MoveNext().Should().BeFalse();

        #endregion
    }
}
