namespace Eon.Tests.Examples;

#region Example1

/// <summary>
/// <see cref="Schedule"/> that returns all the odd seconds up to <see cref="Max"/>
/// seconds
/// </summary>
/// <param name="Max">max seconds</param>
public sealed record OddSecondsToMax(uint Max) : Schedule
{
    public override int? Count => (int)Max;
    public override bool CanCount => true;

    public override IEnumerator<Duration> GetEnumerator()
    {
        for (int i = 1, j = 0; j < Max; i++)
        {
            if (i % 2 == 0)
            {
                continue;
            }

            j++;
            yield return TimeSpan.FromSeconds(i);
        }
    }
}

public static class OddSecondsToMaxTests
{
    [Fact(DisplayName = "returns all the odd seconds up to max seconds")]
    public static void Case1()
    {
        Schedule schedule = new OddSecondsToMax(10);
        schedule
            .Should()
            .ContainInOrder(
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(7),
                TimeSpan.FromSeconds(9),
                TimeSpan.FromSeconds(11),
                TimeSpan.FromSeconds(13),
                TimeSpan.FromSeconds(15),
                TimeSpan.FromSeconds(17)
            );
    }
}

#endregion
