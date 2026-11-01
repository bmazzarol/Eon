![Eon](https://raw.githubusercontent.com/bmazzarol/Eon/main/schedule-icon-small.png)

# Eon

**Say when something should run, as data.**

A Schedule is a stream of durations. Compose it, iterate it, await it, and never start a thread.

[![Nuget](https://img.shields.io/nuget/v/eon)](https://www.nuget.org/packages/eon/)

```shell
dotnet add package eon
```

[Getting started](https://bmazzarol.github.io/Eon/articles/getting-started.html) - [Full documentation](https://bmazzarol.github.io/Eon/)

## Why?

Cron is the usual way to describe a repeating cadence, but it is a separate
language and it assumes a scheduler. `Schedule` is a plain value instead: an
iterable stream of durations built from combinators.

## First example

```csharp
// a schedule that emits 5 durations, each 2 seconds long
Schedule schedule = Schedule.Spaced(TimeSpan.FromSeconds(2)) & Schedule.Recurs(5);

// a Schedule is an IEnumerable<Duration>, so the work is a plain loop
foreach (Duration duration in schedule)
{
    await SomeService();
    await duration; // waits two seconds
}
```

Combinators cover most schedules, so extending `Schedule` is rarely necessary.
Where it is not enough, a custom `IEnumerator<Duration>` is all it takes.

## What it does not do

- Nothing runs. There is no scheduler, timer or background thread
- It does not parse cron expressions
- It does not persist a schedule or read the clock

## See also

- [Getting started](https://bmazzarol.github.io/Eon/articles/getting-started.html)
- [Schedule components](https://bmazzarol.github.io/Eon/articles/components/toc.html)
