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
		var (t, n) = Read2();
		var p = Array.ConvertAll(new bool[t], _ => Read());

		var ri = 0;
		var r = new int[t];
		var m = new int[n];

		for (int i = 0; i < t; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (m[j] < p[i][j])
				{
					ri += p[i][j] - m[j];
					m[j] = p[i][j];
				}
			}
			r[i] = ri;
		}
		return string.Join("\n", r);
	}
}
