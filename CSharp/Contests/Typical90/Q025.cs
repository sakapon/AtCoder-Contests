using System;
using System.Linq;

class Q025
{
	const long max = 100000000000;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, b) = Read2L();

		static long f(long x) => x.ToString().Select(c => (long)(c - '0')).Aggregate((x, y) => x * y);
		int Count(long v)
		{
			var m = v + b;
			if (m > n) return 0;
			return f(m) == v ? 1 : 0;
		}

		var r = Count(0);

		for (var (i2, v2) = (0, 1L); i2 < 33 && v2 < max; i2++, v2 *= 2)
		{
			for (var (i3, v3) = (0, v2); i3 < 22 && v3 < max; i3++, v3 *= 3)
			{
				for (var (i5, v5) = (0, v3); i5 < 11 && v5 < max; i5++, v5 *= 5)
				{
					for (var (i7, v7) = (0, v5); i7 < 11 && v7 < max; i7++, v7 *= 7)
					{
						r += Count(v7);
					}
				}
			}
		}
		return r;
	}
}
