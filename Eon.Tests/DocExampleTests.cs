using Eon;

namespace Eon.Tests.Examples;

/// <summary>
/// Keeps the emissions quoted in the docs honest.
/// </summary>
public static class DocExampleTests
{
    private static string Dump(Schedule schedule) =>
        string.Join(
            ", ",
            schedule.Select(d =>
                d.AsTimeSpan()
                    .TotalSeconds.ToString(
                        "0.##",
                        System.Globalization.CultureInfo.InvariantCulture
                    )
            )
        );

    [Fact(DisplayName = "The getting started examples produce the emissions shown in the docs")]
    public static void GettingStarted()
    {
        Schedule linear = Schedule.Linear(TimeSpan.FromSeconds(1));

        Assert.Equal(
            "1, 2, 3, 4, 5, 5, 5, 5, 5, 5",
            Dump(linear.Take(10) & Schedule.LessThan(TimeSpan.FromSeconds(5)))
        );

        Assert.Equal(
            "1, 1, 1, 1, 1, 6, 7, 8, 9, 10",
            Dump(linear.Take(10) | Schedule.Spaced(TimeSpan.FromSeconds(1)).Take(5))
        );

        Assert.Equal(
            "5, 5, 5, 5, 5, 6, 7, 8, 9, 10",
            Dump(linear.Take(10) & Schedule.Spaced(TimeSpan.FromSeconds(5)).Take(10))
        );

        Assert.Equal(
            "2, 2, 2, 2, 2",
            Dump(Schedule.Spaced(TimeSpan.FromSeconds(2)) & Schedule.Recurs(5))
        );

        Assert.Equal(
            "5, 5, 5, 5, 5",
            Dump(Schedule.Spaced(TimeSpan.FromSeconds(5)) & Schedule.Recurs(5))
        );
    }
}
