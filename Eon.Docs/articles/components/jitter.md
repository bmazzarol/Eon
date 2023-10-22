# Jitter

@Eon.Schedule.Jitter* adds a random jitter to any returned @Eon.Duration

Using a defined `minRandom` and `maxRandom` @Eon.Duration,

[!code-csharp[Example1](../../../Eon.Tests/Examples/JitterTests.cs#Example1)]

Using a defined `factor` influenced by the current returned delay,

[!code-csharp[Example1](../../../Eon.Tests/Examples/JitterTests.cs#Example2)]
