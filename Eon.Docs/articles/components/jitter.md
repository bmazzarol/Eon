# Jitter

<xref:Eon.Schedule.Jitter*> adds a random jitter to any returned
<xref:Eon.Duration>

Using a defined `minRandom` and `maxRandom` <xref:Eon.Duration>,

[!code-csharp[Example1](../../../Eon.Tests/Examples/JitterTests.cs#Example1)]

[!INCLUDE[](../../../Eon.Tests/Examples/__examples__/JitterTests.Case1.md)]

Using a defined `factor` influenced by the current returned delay,

[!code-csharp[Example1](../../../Eon.Tests/Examples/JitterTests.cs#Example2)]

[!INCLUDE[](../../../Eon.Tests/Examples/__examples__/JitterTests.Case2.md)]
