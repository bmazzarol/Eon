using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class FibonacciTests
{
    [Fact(
        DisplayName = "Fibonacci returns a stream of the duration based on a fibonacci sequence from a starting seed"
    )]
    public static void Case1()
    {
        #region Example1

        Schedule fibonacci = Schedule.Fibonacci(TimeSpan.FromSeconds(1)).Take(5);

        #endregion

        fibonacci.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "Fibonacci is infinite")]
    public static void Case2()
    {
        Schedule fibonacci = Schedule.Fibonacci(TimeSpan.FromSeconds(1));

        Assert.False(fibonacci.CanCount);
        Assert.Null(fibonacci.Count);
    }
}
