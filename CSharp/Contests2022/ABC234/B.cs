using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var r = 0D;

		for (int i = 0; i < n; i++)
		{
			var (x1, y1) = ps[i];

			for (int j = i + 1; j < n; j++)
			{
				var (x, y) = ps[j];
				x -= x1;
				y -= y1;
				r = Math.Max(r, Math.Sqrt(x * x + y * y));
			}
		}
		return r;
	}
}
