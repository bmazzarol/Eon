# Append (+)

<xref:Eon.Schedule.Append*> appends the `second` <xref:Eon.Schedule> to the
`first` <xref:Eon.Schedule>.

> [!WARNING]
> Appending to the end of an infinite <xref:Eon.Schedule> will not result in
> the `second` <xref:Eon.Schedule> getting run, as the `first` will never
> complete

Using the <xref:Eon.Schedule.Append*> method,

[!code-csharp[Example1](../../../Eon.Tests/Examples/AppendTests.cs#Example1)]

[!INCLUDE[](../../../Eon.Tests/Examples/__examples__/AppendTests.Case1.md)]

Using the <xref:Eon.Schedule.op_Addition*>,

[!code-csharp[Example2](../../../Eon.Tests/Examples/AppendTests.cs#Example2)]

[!INCLUDE[](../../../Eon.Tests/Examples/__examples__/AppendTests.Case2.md)]
