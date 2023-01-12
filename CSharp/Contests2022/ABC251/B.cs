using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, w) = Read2();
		var a = Read();

		var u = new bool[3000000 + 1];

		for (int i = 0; i < n; i++)
		{
			var ti = a[i];
			u[ti] = true;

			for (int j = i + 1; j < n; j++)
			{
				var tj = ti + a[j];
				u[tj] = true;

				for (int k = j + 1; k < n; k++)
				{
					var tk = tj + a[k];
					u[tk] = true;
				}
			}
		}
		return u[..(w + 1)].Count(b => b);
	}
}
