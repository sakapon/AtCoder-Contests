using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		var pts = GetPrimeTypes(100000);

		var set = new HashSet<int>();

		foreach (var x in a)
		{
			foreach (var p in pts[x])
			{
				set.Add(p);
			}
		}

		var b = new bool[m + 1];
		foreach (var p in set)
			for (int x = p; x <= m; x += p)
				b[x] = true;

		var r = new List<int>();
		for (int x = 1; x <= m; ++x) if (!b[x]) r.Add(x);

		return $"{r.Count}\n" + string.Join("\n", r);
	}

	static int[][] GetPrimeTypes(int n)
	{
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int p = 2; p <= n; ++p)
			if (map[p].Count == 0)
				for (int x = p; x <= n; x += p)
					map[x].Add(p);
		return Array.ConvertAll(map, l => l.ToArray());
	}
}
