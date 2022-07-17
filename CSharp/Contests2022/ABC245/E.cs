using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var b = Read();
		var c = Read();
		var d = Read();

		var qs = Enumerable.Range(0, n).Select(i => (a[i], b[i], 1))
			.Concat(Enumerable.Range(0, m).Select(j => (c[j], d[j], 2)))
			.ToArray();
		Array.Sort(qs);

		var set = new WBMultiSet<int>();

		foreach (var (x, y, q) in qs)
		{
			if (q == 1)
			{
				set.Add(y);
			}
			else
			{
				set.RemoveLast(v => v <= y);
			}
		}

		return set.Count == 0;
	}
}
