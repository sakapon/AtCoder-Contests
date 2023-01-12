using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoderLib8.Graphs.SPPs.Arrays.PathCore111;

class A61
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var sb = new StringBuilder();

		var map = PathCore.ToListMap(n + 1, es, true);
		for (int v = 1; v <= n; v++)
		{
			sb.AppendLine($"{v}: {{{string.Join(", ", map[v])}}}");
		}
		Console.Write(sb);
	}
}
