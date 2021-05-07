using System;
using System.Collections.Generic;

class Q034
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = Read();

		var d = new Dictionary<int, int>();
		var c = 0;
		var m = 0;

		for (var (l, r) = (0, 0); r < n; r++)
		{
			var x = a[r];
			d[x] = d.GetValueOrDefault(x) + 1;
			if (d[x] == 1) c++;

			for (; c > k; l++)
			{
				var xl = a[l];
				d[xl]--;
				if (d[xl] == 0) c--;
			}

			m = Math.Max(m, r - l + 1);
		}
		return m;
	}
}
