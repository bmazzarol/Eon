# Getting Started

To use this library, simply include `Eon.dll` in your project or
grab
it from [NuGet](https://www.nuget.org/packages/Eon/), and add
this to the top of each `.cs` file that needs it:

```C#
using Eon;
```

Now a <xref:Eon.Schedule> can be created.

Lets start with a simple <xref:Eon.Schedule> that runs forever emitting
<xref:Eon.Duration.Zero> values

```csharp
Schedule forever = Schedule.Forever;
```

A <xref:Eon.Schedule> implements <xref:System.Collections.Generic.IEnumerable`1>
so can be iterated like any collection

```csharp
foreach(Duration zero in forever)
{
    await zero;        
}
```

The power of <xref:Eon.Schedule> is that new <xref:Eon.Schedule> can be built
out of simpler <xref:Eon.Schedule>.

Lets create a <xref:Eon.Schedule> that runs 10 times, with a linear backoff to a
max <xref:Eon.Duration>.

```csharp
Schedule custom = Schedule.Linear(TimeSpan.FromSeconds(1)).Take(10) 
                & Schedule.LessThan(TimeSpan.FromSeconds(5));
```

This produces the following emissions, `1, 2, 3, 4, 5, 5, 5, 5, 5, 5`

This library exposes a large number of <xref:Eon.Schedule> `components` that can
be used and composed to create new <xref:Eon.Schedule> combinations.

The two main combinators are [Union (|)](xref:Eon.Schedule.Union*)
and [Intersect (&)](xref:Eon.Schedule.Intersect*).

Union can be used when you want the emissions from `either` <xref:Eon.Schedule>.

It will take the `minimum` <xref:Eon.Duration> from either <xref:Eon.Schedule>
and will only stop running when `both` <xref:Eon.Schedule> complete.

```csharp
Schedule custom = Schedule.Linear(TimeSpan.FromSeconds(1)).Take(10)
                | Schedule.Spaced(TimeSpan.FromSeconds(1)).Take(5)
```

This will producer the following emissions, `1, 1, 1, 1, 1, 6, 7, 8, 9, 10`

Intersect can be used when you want the emissions from `both`
<xref:Eon.Schedule>.

It will take the `maximum` <xref:Eon.Duration> from both <xref:Eon.Schedule>
and will stop running when `any` <xref:Eon.Schedule> complete.

```csharp
Schedule custom = Schedule.Linear(TimeSpan.FromSeconds(1)).Take(10)
                & Schedule.Spaced(TimeSpan.FromSeconds(5)).Take(10)
```

This will producer the following emissions, `5, 5, 5, 5, 5, 6, 7, 8, 9, 10`
