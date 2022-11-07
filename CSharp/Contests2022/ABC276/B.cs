using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var sb = new StringBuilder();

		var map = ToMapList(n + 1, es, false);
		foreach (var l in map[1..])
		{
			sb.Append(l.Count).Append(' ').AppendLine(string.Join(" ", l.OrderBy(x => x)));
		}
		Console.Write(sb);
	}

	public static List<int>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
