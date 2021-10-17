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
		var c = Console.ReadLine().ToArray();

		var count = 0;
		var l = 0;
		var r = n - 1;

		while (l < r)
		{
			while (l < n && c[l] == 'R') l++;
			while (0 <= r && c[r] == 'W') r--;

			if (l >= r) break;

			count++;
			l++;
			r--;
		}

		return count;
	}
}
