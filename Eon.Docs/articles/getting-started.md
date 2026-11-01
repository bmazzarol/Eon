# Getting Started

Add the package, then use `Eon` in the file that needs a schedule.

```shell
dotnet add package eon
```

```c#
using Eon;
```

A schedule is a stream of durations. The shortest one that runs forever is,

```c#
Schedule forever = Schedule.Forever;
```

`Schedule` implements `IEnumerable<Duration>`, so it can be iterated like any
collection, and each `Duration` can be awaited.

```c#
foreach (Duration zero in forever.Take(3))
{
    await zero;   // Duration.Zero, so this returns immediately
}
```

## Build schedules from parts

Schedules are composed with two operators.

`&` (intersect) takes the larger duration from each side and stops when either
side ends.

```c#
Schedule linear = Schedule.Linear(TimeSpan.FromSeconds(1));

Schedule custom = linear.Take(10) & Schedule.LessThan(TimeSpan.FromSeconds(5));
```

emits,

```text
1, 2, 3, 4, 5, 5, 5, 5, 5, 5
```

`|` (union) takes the smaller duration from each side and stops when both sides
end.

```c#
Schedule custom = linear.Take(10) | Schedule.Spaced(TimeSpan.FromSeconds(1)).Take(5);
```

emits,

```text
1, 1, 1, 1, 1, 6, 7, 8, 9, 10
```

Intersecting with a spaced schedule gives the same cadence as the slower side
until the faster one catches up.

```c#
Schedule custom = linear.Take(10) & Schedule.Spaced(TimeSpan.FromSeconds(5)).Take(10);
```

emits,

```text
5, 5, 5, 5, 5, 6, 7, 8, 9, 10
```

## Run a schedule

Nothing in Eon executes code. A schedule only says when something should
happen, so the consumer decides how to use it.

```c#
Schedule everyFiveSeconds = Schedule.Spaced(TimeSpan.FromSeconds(5)) & Schedule.Recurs(5);

foreach (Duration duration in everyFiveSeconds)
{
    await DoWork();
    await duration;   // waits five seconds
}
```\n
emits five durations of five seconds,

```text
5, 5, 5, 5, 5
```

## What it does not do

* It does not run anything. There is no scheduler, no thread, no timer
* It does not parse cron expressions
* A schedule can be infinite, so use `Take` or a bound when a finite stream is
  needed

## See also

* [Build A Schedule](how-to/build-schedule.md) - composing components
* [Schedule Components](components/toc.yml) - every combinator, with examples
