using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int> { -1 });
		for (int i = 0; i < n; i++)
		{
			map[a[i]].Add(i);
		}

		for (int k = 0; k <= n; k++)
		{
			var l = map[k];
			l.Add(n);

			for (int i = 1; i < l.Count; i++)
			{
				if (l[i] - l[i - 1] > m) return k;
			}
		}
		return -1;
	}
}
