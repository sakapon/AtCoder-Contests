using System;
using System.Collections.Generic;
using CoderLib8.Collections.Dynamics.Int;

class DS
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
		var set = new IntSegmentMultiSet(n + 1);
		var lmap = new List<int>[n + 1];

		for (int i = 1; i <= n; i++)
		{
			var x = p[i - 1];

			var geq = set.RemoveFirstGeq(x);
			if (geq <= n)
			{
				var l = lmap[geq];
				l.Add(x);

				if (l.Count < k)
				{
					set.Add(x);
					lmap[x] = l;
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
					set.Add(x);
					lmap[x] = new List<int> { x };
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
