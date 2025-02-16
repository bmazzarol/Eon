using Docfx.ResultSnippets;
using Eon.Tests.Extensions;

namespace Eon.Tests.Examples;

public static class PrependTests
{
    [Fact(DisplayName = "prepend appends to the start of a schedule")]
    public static void Case1()
    {
        #region Example1

        Schedule prepend = Schedule.Never.Prepend(Duration.Zero);

        #endregion

        prepend.RenderSchedule().SaveResults();
    }
}
