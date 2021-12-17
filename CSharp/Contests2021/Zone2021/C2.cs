using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var rn = Enumerable.Range(0, n).ToArray();

		int ToBits(int i, int min)
		{
			var p = ps[i];
			var r = 0;

			for (int f = 0; f < 5; f++)
			{
				if (p[f] >= min)
				{
					r |= 1 << f;
				}
			}
			return r;
		}

		return Max(-1, max, v =>
		{
			var fs = rn.Select(i => ToBits(i, v)).Distinct().ToArray();

			for (int i = 0; i < fs.Length; i++)
			{
				for (int j = 0; j < fs.Length; j++)
				{
					for (int k = 0; k < fs.Length; k++)
					{
						if ((fs[i] | fs[j] | fs[k]) == 31) return true;
					}
				}
			}
			return false;
		});
	}

	static int Max(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
