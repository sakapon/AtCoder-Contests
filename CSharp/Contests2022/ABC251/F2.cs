using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoderLib8.Graphs.Specialized.Int;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());
		var sb = new StringBuilder();

		var graph = new UnweightedTreeGraph(n + 1, es, true);
		var dfs = graph.DFSTree(1);
		var bfs = graph.BFSTree(1);

		foreach (var v in dfs)
		{
			if (v.Parent == null) continue;
			sb.AppendLine($"{v.Parent.Id} {v.Id}");
		}

		foreach (var v in bfs)
		{
			if (v.Parent == null) continue;
			sb.AppendLine($"{v.Parent.Id} {v.Id}");
		}

		Console.Write(sb);
	}
}
