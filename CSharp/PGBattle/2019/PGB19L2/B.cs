using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2L();
		var (a, b, c) = Read3L();

		// x+y+z = n
		// (a-c)*x + (b-c)*y = m-c*n
		// ac*x + bc*y = mn
		var ac = a - c;
		var bc = b - c;
		var mn = m - c * n;

		if (bc == 0)
		{
			if (ac == 0)
			{
				if (mn != 0) return "-1 -1 -1";
				return $"{n} {0} {0}";
			}

			if (mn % ac != 0) return "-1 -1 -1";
			var x = mn / ac;
			if (x < 0 || n < x) return "-1 -1 -1";
			var z = n - x;
			if (z < 0 || n < z) return "-1 -1 -1";
			return $"{x} {0} {z}";
		}

		for (int x = 0; x <= n; x++)
		{
			var s = mn - ac * x;
			if (s % bc != 0) continue;
			var y = s / bc;
			if (y < 0 || n < y) continue;
			var z = n - x - y;
			if (z < 0 || n < z) continue;
			return $"{x} {y} {z}";
		}
		return "-1 -1 -1";
	}
}
