using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var rn = Enumerable.Range(0, 1 << n).ToArray();

		var r = new int[1 << n];
		var b1 = Array.FindAll(rn, i => a[i] == 1);
		if (b1.Length != 1) return -1;
		r[0] = b1[0] + 1;

		for (int c = 1 << n; c > 1; c >>= 1)
		{
			var b = Array.FindAll(rn, i => a[i] == c);
			if (b.Length != c >> 1) return -1;

			var d = (1 << n) / c;
			for (int i = 0; i < b.Length; i++)
			{
				r[d * 2 * i + d] = b[i] + 1;
			}
		}

		return string.Join(" ", r);
	}
}
