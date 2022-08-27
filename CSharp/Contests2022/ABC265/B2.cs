using System;
using System.Collections.Generic;
using System.Linq;

class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m, t) = Read3();
		var a = Read();
		var ps = Array.ConvertAll(new bool[m], _ => Read());

		var b = new int[n];
		foreach (var p in ps)
		{
			b[p[0]] = p[1];
		}

		long r = t;
		for (int i = 1; i < n; i++)
		{
			r += b[i];
			r -= a[i - 1];
			if (r <= 0) return false;
		}
		return true;
	}
}
