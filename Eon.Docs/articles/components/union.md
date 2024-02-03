# Union (|)

<xref:Eon.Schedule.Union*> returns the union of two <xref:Eon.Schedule>.
As long as any of the two <xref:Eon.Schedule> are still running it will
return the minimum <xref:Eon.Duration>.

Using the <xref:Eon.Schedule.Union*> method,

[!code-csharp[Example1](../../../Eon.Tests/Examples/UnionTests.cs#Example1)]

Using the <xref:Eon.Schedule.op_BitwiseOr*>,

[!code-csharp[Example2](../../../Eon.Tests/Examples/UnionTests.cs#Example2)]

The returned <xref:Eon.Schedule> will run as long as the longest of the two
<xref:Eon.Schedule>,

[!code-csharp[Example3](../../../Eon.Tests/Examples/UnionTests.cs#Example3)]

<xref:Eon.Schedule.op_BitwiseOr*> can be used with
<xref:Eon.ScheduleTransformer>,

[!code-csharp[Example4](../../../Eon.Tests/Examples/UnionTests.cs#Example4)]
