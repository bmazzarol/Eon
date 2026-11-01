# Eon

![Eon](images/schedule-icon.png)

**Say when something should run, as data.**

An immutable Schedule type: a potentially infinite stream of durations you can compose, iterate and await, with no execution engine attached.

```shell
dotnet add package eon
```

## Why?

Schedules are a common requirement when building software. Whenever you need
some operation to repeat at some configured cadence, a way to encode that is
needed.

The standard abstraction that many developers would be familiar with
is [Cron](https://en.wikipedia.org/wiki/Cron). This is a job scheduler that
comes paired with a simple language; crontab that is used to define how the
job is to be repeated.

Eon exposes a type <xref:Eon.Schedule>, that is a modern alternative
to Cron. It is not concerned with how it's executed; only the cadence at which
an execution engine could run it.

It's an immutable blueprint; a potentially infinite stream of <xref:Eon.Duration>
that can be iterated and potentially awaited.

```csharp
// defines a simple schedule that emits 5 durations each 2 seconds long
Schedule schedule = 
    Schedule.Spaced(TimeSpan.FromSeconds(2)) & Schedule.Recurs(5);

// the schedule can be used in any way; it's an IEnumerable<Duration>
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

There are many combinators which allow <xref:Eon.Schedule> to be composed out of
simple building blocks reducing the need to extend <xref:Eon.Schedule>
directly. Eon exposes many of these combinators, which are documented here.

However, if it's required, it is as simple as writing a custom
<xref:System.Collections.Generic.IEnumerable{Eon.Duration}>.

For more details/information keep reading the docs or have a look at the test
projects or create an issue.
