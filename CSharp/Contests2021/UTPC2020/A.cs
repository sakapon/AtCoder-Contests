using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int a) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, l) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2()).Prepend((x: 0, 0)).ToArray();

		Console.WriteLine(First(0, 1L << 60, v =>
		{
			var t = v;
			for (int i = 1; i <= n; i++)
			{
				var (x, a) = ps[i];
				t = Math.Min(v, t + x - ps[i - 1].x);
				if ((t -= a) < 0) return false;
			}
			return true;
		}));
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
