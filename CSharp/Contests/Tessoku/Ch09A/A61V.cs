using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoderLib8.Graphs.SPPs.Int.UnweightedGraph211;

class A61V
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());
		var sb = new StringBuilder();

		var graph = new UnweightedGraph(n + 1, es, true);
		for (int v = 1; v <= n; v++)
		{
			sb.AppendLine($"{v}: {{{string.Join(", ", graph[v].Edges)}}}");
		}
		Console.Write(sb);
	}
}
