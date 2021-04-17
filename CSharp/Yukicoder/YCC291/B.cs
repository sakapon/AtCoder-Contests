using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var a = Read();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var k = int.Parse(Console.ReadLine());
		var b = Read();

		var map = EdgesToMap1(n + 1, es, false);
		var u = new bool[n + 1];
		foreach (var v in b) u[v] = true;

		var r = new List<int>();

		foreach (var (x, v) in a.Select((x, i) => (x, i: i + 1)).OrderBy(t => t.x))
		{
			if (!u[v]) continue;

			r.Add(v);
			u[v] = false;

			foreach (var nv in map[v])
			{
				if (a[v - 1] < a[nv - 1])
				{
					u[nv] ^= true;
				}
			}
		}

		Console.WriteLine(r.Count);
		Console.WriteLine(string.Join("\n", r));
	}

	public static List<int>[] EdgesToMap1(int n, int[][] es, bool directed)
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
