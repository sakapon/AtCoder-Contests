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
		var p = Read();

		var s = new int[n];
		for (int i = 0; i < n; i++)
		{
			var j = p[i] - i + n;
			s[(j - 1) % n]++;
			s[j % n]++;
			s[(j + 1) % n]++;
		}
		return s.Max();
	}
}
