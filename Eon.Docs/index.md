# Eon

![Eon](images/schedule-icon.png)

**Say when something should run, as data.**

A Schedule is a stream of durations. Compose it, iterate it, await it, and never start a thread.

```shell
dotnet add package eon
```

## Why?

Anything that repeats on a cadence needs a way to describe that cadence. Cron
is the familiar answer, but it is a scheduler with a text format attached, and
the schedule cannot be inspected, composed or tested as a value.

Eon makes the cadence a type. <xref:Eon.Schedule> is an
`IEnumerable<Eon.Duration>`, so a schedule can be built from parts, enumerated,
and awaited without any engine deciding when it runs.

```c#
// five durations, each two seconds long
Schedule schedule = Schedule.Spaced(TimeSpan.FromSeconds(2)) & Schedule.Recurs(5);

foreach (Duration duration in schedule)
{
    var result = await SomeService();
    await duration;   // waits two seconds
}
```

Combinators such as `Spaced`, `Recurs`, `Daily`, `Between` and `During` cover
most cadences. Anything else is a custom `IEnumerable<Duration>`.

## What it does not do

* It does not run anything. There is no scheduler, timer or background thread;
  Eon only says when something should happen
* It does not parse cron expressions
* It does not persist a schedule or react to the clock on its own

## See also

* [Getting Started](articles/getting-started.md) - install and run a schedule
* [Schedule Components](articles/components/toc.yml) - the combinators that
  build schedules
* [Build a Schedule](articles/how-to/build-schedule.md) - compose schedules from
  components
