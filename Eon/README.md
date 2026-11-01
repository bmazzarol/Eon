<!-- markdownlint-disable MD013 -->

# ![Eon](https://raw.githubusercontent.com/bmazzarol/Eon/main/schedule-icon-small.png) Eon

<!-- markdownlint-enable MD013 -->

[![Nuget](https://img.shields.io/nuget/v/Eon)](https://www.nuget.org/packages/Eon/)

Schedules are a common requirement when building software. Whenever you need
some operation to repeat at some configured cadence, a way to encode that is
needed.

The standard abstraction that many developers would be familiar with
is [Cron](https://en.wikipedia.org/wiki/Cron). This is a job scheduler that
comes paired with its own little language, crontab, that says how the job
repeats.

Eon exposes a type called `Schedule`, that is a modern alternative
to Cron. It is not concerned with how it's executed; only the cadence at which
an execution engine could run it.

It's an immutable blueprint; a potentially infinite stream of durations that
can be iterated and potentially awaited.

```csharp
// a schedule that emits 5 durations, each 2 seconds long
Schedule schedule =
    Schedule.Spaced(TimeSpan.FromSeconds(2)) & Schedule.Recurs(5);

// a Schedule is an IEnumerable<Duration>, so the work is a plain loop
// and each duration is awaited after it
foreach(Duration duration in schedule)
{
    // some operation
    var result = await SomeService();
    // now wait 2 seconds
    await duration;
}
```

Schedules are composed from combinators, so extending `Schedule` is rarely
necessary. The combinators are documented on the site.

Where that is not enough, a custom `IEnumerator<Duration>` is all it takes.

For more details/information have a look at the test projects or create an issue.
