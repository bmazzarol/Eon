# Exponential

@Eon.Schedule.Exponential* returns an infinite stream of @Eon.Duration
increasing based on
a [exponential sequence](https://en.wikipedia.org/wiki/Exponential_backoff) from
a starting `seed` @Eon.Duration

[!code-csharp[Example1](../../../Eon.Tests/Examples/ExponentialTests.cs#Example1)]

With an optional `factor`,

[!code-csharp[Example2](../../../Eon.Tests/Examples/ExponentialTests.cs#Example2)]
