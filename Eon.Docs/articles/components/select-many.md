# SelectMany

@Eon.Schedule.SelectMany* transforms each emissions from a @Eon.Schedule based
on a provided `bind` function that returns a new @Eon.Schedule that then
emits all its @Eon.Duration

> [!WARNING]
> If the `bind` function returns an infinite @Eon.Schedule for any emission
> from the original @Eon.Schedule no further emissions from the original
> @Eon.Schedule will occur

[!code-csharp[Example1](../../../Eon.Tests/Examples/SelectManyTests.cs#Example1)]

Using LINQ expressions,

[!code-csharp[Example2](../../../Eon.Tests/Examples/SelectManyTests.cs#Example2)]

Ignoring the results from the first @Eon.Schedule,

[!code-csharp[Example3](../../../Eon.Tests/Examples/SelectManyTests.cs#Example3)]
