<!-- markdownlint-disable MD013 -->

# ![Eon](https://raw.githubusercontent.com/bmazzarol/Eon/main/schedule-icon-small.png) Eon

<!-- markdownlint-enable MD013 -->

[![Nuget](https://img.shields.io/nuget/v/Eon)](https://www.nuget.org/packages/Eon/)

Schedules are a common requirement when building software. Whenever you need
some operation to repeat at some configured cadence, a way to encode that is
needed.

The standard abstraction that many developers would be familiar with
is [Cron](https://en.wikipedia.org/wiki/Cron). This is a job scheduler that
comes paired with a simple language; crontab that is used to define how the
job is to be repeated.

Eon exposes a type called `Schedule`, that is a modern alternative
to Cron, that is not concerned with how its executed, just the cadence at which
an execution engine could run it.

Its a immutable blueprint; a potentially infinite stream of durations that
can be iterated and potentially awaited.

```csharp
// defines a simple schedule that emmits 5 durations each 2 seconds long
Schedule schedule = 
    Schedule.Spaced(TimeSpan.FromSeconds(2)) & Schedule.Recurs(5);

// the schedule can be used in any way; its an IEnumerable<Duration>
// so we can just foreach it and do the operation; awaiting the durations
// after
foreach(Duration duration in schedule)
{
    // some operation
    var result = await SomeService();
    // now wait 2 seconds
    await duration;
}
```

There are many combinators which allow Schedules to be composed out of simple
building blocks reducing the need to extend Schedule directly. Eon
exposes many of these combinators, which are documented here.

However if its required it is as simple as writing a custom `IEnumerator` of
`Duration`.

For more details/information have a look a the test projects or create an issue.
