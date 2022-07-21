using System;
using System.Collections.Generic;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var counts = new int[m + 1];

		var q = Array.ConvertAll(new bool[m + 1], _ => new List<int>());
		foreach (var p in ps)
		{
			counts[p[0]]++;
			counts[p[1]]++;
			q[p[1]].Add(p[0]);
		}

		var raq = new StaticRAQ1(m);
		var count = 0;
		var min = 0;

		for (int j = 1; j <= m; j++)
		{
			foreach (var a in q[j]) counts[a]--;
			count += counts[j] - q[j].Count;
			if (count < n) continue;

			while (counts[min] == 0) min++;
			raq.Add(j - min, j, 1);
		}

		var sum = raq.GetSum();
		return string.Join(" ", sum);
	}
}
