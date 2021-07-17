using System;
using System.Linq;

class Q086
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read4L());

		var rn = Enumerable.Range(0, n).ToArray();
		var r = 1L;

		for (int f = 0; f < 60; f++)
		{
			var c = 0;
			for (int x = 0; x < 1 << n; x++)
			{
				if (Check(x, f)) c++;
			}
			r *= c;
			r %= M;
		}
		return r;

		bool Check(int x, int f)
		{
			var fs = Array.ConvertAll(rn, i => (x & (1 << i)) != 0);

			foreach (var (a, b, c, w) in qs)
			{
				var v = fs[a - 1] | fs[b - 1] | fs[c - 1];
				if (((w & (1L << f)) != 0) ^ v) return false;
			}
			return true;
		}
	}
}
