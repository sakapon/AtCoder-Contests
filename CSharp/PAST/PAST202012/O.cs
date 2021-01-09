using System;
using System.Collections.Generic;
using System.Linq;

class O
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var map = EdgesToMap1(n + 1, es, false);
		var notes = new int[n + 1];
		var ends = new int[n + 1];

		foreach (var q in qs)
		{
			var x = q.Item2;
			if (q.Item1 == 1)
			{
				notes[x]++;
			}
			else
			{
				var sum = map[x].Sum(y => notes[y]);
				Console.WriteLine(sum - ends[x]);
				ends[x] = sum;
			}
		}
	}

	static List<int>[] EdgesToMap1(int n, int[][] es, bool directed)
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
