# Intersect (&)

<xref:Eon.Schedule.Intersect*> returns the intersect of two <xref:Eon.Schedule>.
As long as both of the two <xref:Eon.Schedule> are still running it will
return the maximum <xref:Eon.Duration>.

Using the <xref:Eon.Schedule.Intersect*> method,

[!code-csharp[Example1](../../../Eon.Tests/Examples/IntersectTests.cs#Example1)]

Using the <xref:Eon.Schedule.op_BitwiseAnd*>,

[!code-csharp[Example2](../../../Eon.Tests/Examples/IntersectTests.cs#Example2)]

The returned <xref:Eon.Schedule> will run as long as the shortest of the two
<xref:Eon.Schedule>

[!code-csharp[Example3](../../../Eon.Tests/Examples/IntersectTests.cs#Example3)]

<xref:Eon.Schedule.op_BitwiseAnd*> can be used with
<xref:Eon.ScheduleTransformer>,

[!code-csharp[Example4](../../../Eon.Tests/Examples/IntersectTests.cs#Example4)]
