using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var r = 1000L;
		var kabu = 0L;

		for (int i = 0; i < n - 1; i++)
		{
			if (a[i] < a[i + 1])
			{
				var q = r / a[i];
				r -= a[i] * q;
				kabu += q;
			}
			else if (a[i] > a[i + 1])
			{
				r += a[i] * kabu;
				kabu = 0;
			}
		}

		r += a[^1] * kabu;

		return r;
	}
}
