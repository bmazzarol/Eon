<!-- markdownlint-disable MD033 MD041 -->
<div align="center">

<img src="images/schedule-icon.png" alt="Eon" width="150px"/>

# Eon

---

[![Nuget](https://img.shields.io/nuget/v/eon)](https://www.nuget.org/packages/eon/)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Eon&metric=coverage)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Eon)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Eon&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Eon)
[![CD Build](https://github.com/bmazzarol/eon/actions/workflows/cd-build.yml/badge.svg)](https://github.com/bmazzarol/eon/actions/workflows/cd-build.yml)
[![Check Markdown](https://github.com/bmazzarol/eon/actions/workflows/check-markdown.yml/badge.svg)](https://github.com/bmazzarol/eon/actions/workflows/check-markdown.yml)

:watch: A Schedule type for C# and dotnet

---

</div>
<!-- markdownlint-enable MD033 MD041 -->

## Why?

Schedules are a common requirement when building software. Whenever you need
some operation to repeat at some configured cadence, a way to encode that is
needed.

The standard abstraction that many developers would be familiar with
is [Cron](https://en.wikipedia.org/wiki/Cron). This is a job scheduler that
comes paired with a simple language; crontab that is used to define how the
job is to be repeated.

Eon exposes a type <xref:Eon.Schedule>, that is a modern alternative
to Cron, that is not concerned with how its executed, just the cadence at which
an execution engine could run it.

Its an immutable blueprint; a potentially infinite stream of <xref:Eon.Duration>
that can be iterated and potentially awaited.

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

There are many combinators which allow <xref:Eon.Schedule> to be composed out of
simple building blocks reducing the need to extend <xref:Eon.Schedule>
directly. Eon exposes many of these combinators, which are documented here.

However if its required it is as simple as writing a custom
<xref:System.Collections.Generic.IEnumerable`1> of <xref:Eon.Duration>.

For more details/information keep reading the docs or have a look at the test
projects or create an issue.
