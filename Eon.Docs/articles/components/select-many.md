# SelectMany

<xref:Eon.Schedule.SelectMany*> transforms each emissions from a
<xref:Eon.Schedule> based on a provided `bind` function that returns a new
<xref:Eon.Schedule> that then emits all its <xref:Eon.Duration>

> [!WARNING]
> If the `bind` function returns an infinite <xref:Eon.Schedule> for any
> emission  from the original <xref:Eon.Schedule> no further emissions from
> the original <xref:Eon.Schedule> will occur

[!code-csharp[Example1](../../../Eon.Tests/Examples/SelectManyTests.cs#Example1)]

Using LINQ expressions,

[!code-csharp[Example2](../../../Eon.Tests/Examples/SelectManyTests.cs#Example2)]

Ignoring the results from the first <xref:Eon.Schedule>,

[!code-csharp[Example3](../../../Eon.Tests/Examples/SelectManyTests.cs#Example3)]
