using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var p = Read();

		var r = new int[n];
		Array.Fill(r, -1);
		var map = new WBMap<int, List<int>>();

		for (int i = 1; i <= n; i++)
		{
			var x = p[i - 1];

			if (map.RemoveFirst(kv => kv.Key >= x).TryGetValue(out var l))
			{
				l.Add(x);

				if (l.Count < k)
				{
					map[x] = l;
				}
				else
				{
					l.ForEach(v => r[v - 1] = i);
				}
			}
			else
			{
				if (k > 1)
				{
					map[x] = new List<int> { x };
				}
				else
				{
					r[x - 1] = i;
				}
			}
		}

		return string.Join("\n", r);
	}
}
