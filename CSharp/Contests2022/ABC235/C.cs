using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int k) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var map = GroupIndexes(a);
		return string.Join("\n", qs.Select(q => map.ContainsKey(q.x) && map[q.x].Count >= q.k ? map[q.x][q.k - 1] + 1 : -1));
	}

	public static Dictionary<T, List<int>> GroupIndexes<T>(T[] a)
	{
		var d = new Dictionary<T, List<int>>();
		for (int i = 0; i < a.Length; ++i)
			if (d.ContainsKey(a[i])) d[a[i]].Add(i);
			else d[a[i]] = new List<int> { i };
		return d;
	}
}
