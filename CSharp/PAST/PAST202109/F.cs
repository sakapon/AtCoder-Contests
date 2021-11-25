using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var z = Enumerable.Range(1, n).Where(i => s[i - 1] == '0').ToArray();
		var m = z.Length;
		if (m == 1) return -1;

		var p = Enumerable.Range(0, n + 1).ToArray();
		for (int j = 0; j < m; j++)
		{
			p[z[j]] = z[(j + 1) % m];
		}

		return string.Join(" ", p[1..]);
	}
}
