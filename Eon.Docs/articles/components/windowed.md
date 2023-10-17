# Windowed

@Eon.Schedule.Windowed* divides a timeline into `interval`-long windows,
and sleeps until the nearest window boundary every time it recurs.

For example,

```csharp
Schedule.Window(TimeSpan.FromSeconds(10));
```

Produces,

``` shell
     10s        10s        10s       10s
|----------|----------|----------|----------|
|action------|sleep---|act|-sleep|action----|
```

[!code-csharp[Example1](../../../Eon.Tests/Examples/WindowedTests.cs#Example1)]
