using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class BoundsTest
{
    [Fact(DisplayName = "LessThan ensures nothing greater than max delay is returned")]
    public static void Case1()
    {
        #region Example1

        Schedule lessThan =
            Schedule.Linear(TimeSpan.FromSeconds(1)) & Schedule.LessThan(TimeSpan.FromSeconds(3));

        #endregion

        lessThan.RenderSchedule().SaveResults();
    }

    [Fact(DisplayName = "GreaterThan ensures nothing less than min delay is returned")]
    public static void Case2()
    {
        #region Example2

        Schedule greaterThan =
            Schedule.Linear(TimeSpan.FromSeconds(1))
            & Schedule.GreaterThan(TimeSpan.FromSeconds(2));

        #endregion

        greaterThan.RenderSchedule().SaveResults();
    }

    [Fact(
        DisplayName = "Between ensures nothing less than min or greater than max delay is returned"
    )]
    public static void Case3()
    {
        #region Example3

        Schedule between =
            Schedule.Linear(TimeSpan.FromSeconds(1))
            & Schedule.Between(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3));

        #endregion

        between.RenderSchedule().SaveResults();
    }
}
