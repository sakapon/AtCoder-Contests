using System;
using System.Collections.Generic;
using System.Linq;

class O
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var p = Read();
		var wls = Array.ConvertAll(new bool[m], _ => Read2());

		var ps = new int[n + 1][];
		for (int i = 0; i < n; i++)
			ps[i] = new int[1 << i];
		ps[n] = p;

		var map = ToInverseMap(p, p.Length);

		foreach (var (w, l) in wls)
		{
			var wi = map[w];
			var li = map[l];

			for (int i = n - 1; i >= 0; i--)
			{
				if ((wi >>= 1) == (li >>= 1))
				{
					if (ps[i][wi] != 0 && ps[i][wi] != w) return 0;
					ps[i][wi] = w;
					break;
				}
				else
				{
					if (ps[i][wi] != 0 && ps[i][wi] != w) return 0;
					ps[i][wi] = w;

					if (ps[i][li] != 0 && ps[i][li] != l) return 0;
					ps[i][li] = l;
				}
			}
		}

		return MPow(2, ps.Sum(a => a.Count(x => x == 0)) + 1);
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}

	public static int[] ToInverseMap(int[] a, int max)
	{
		var d = Array.ConvertAll(new bool[max + 1], _ => -1);
		for (int i = 0; i < a.Length; ++i) d[a[i]] = i;
		return d;
	}
}
