using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Docfx.ResultSnippets;
using Humanizer;

namespace Eon.Tests.Extensions;

internal static class ScheduleExtensions
{
    public static Func<DateTimeOffset> ToTestProvider(this DateTime[] dates)
    {
        var queue = new Queue<DateTime>(dates);

#pragma warning disable MA0132
        return () => queue.Dequeue();
#pragma warning restore MA0132
    }

    public static string RenderSchedule(this Schedule schedule, bool addRunningTotal = false)
    {
        if (!schedule.CanCount)
        {
            schedule = schedule.Take(5);
        }

        if (addRunningTotal)
        {
            var total = TimeSpan.Zero;
            return schedule
                .Select(d =>
                {
                    var ts = d.AsTimeSpan();
                    total += ts;
                    return new
                    {
                        Duration = ts.Humanize(precision: 2),
                        Total = total.Humanize(precision: 2),
                    };
                })
                .ToTableResult();
        }

        return schedule
            .Select(d => new { Duration = d.AsTimeSpan().Humanize(precision: 2) })
            .ToTableResult();
    }

    public static string RenderSchedule(
        this Schedule schedule,
        DateTime[] dates,
        [StringSyntax(StringSyntaxAttribute.DateTimeFormat)] string format
    )
    {
        using var enumerator = schedule.GetEnumerator();

        return Enumerator()
            .Select(d => new
            {
                d.Date,
                d.Duration,
                d.Result,
            })
            .ToTableResult();

        IEnumerable<(string Date, string Duration, string Result)> Enumerator()
        {
            foreach (var date in dates)
            {
                enumerator.MoveNext();
                var ts = enumerator.Current.AsTimeSpan();
                yield return (
                    Date: date.ToString(format, CultureInfo.InvariantCulture),
                    Duration: ts.Humanize(precision: 2),
                    Result: date.Add(ts).ToString(format, CultureInfo.InvariantCulture)
                );
            }
        }
    }
}
