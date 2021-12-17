using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		var (l, r) = Read2();

		if (s.Length > 1 && s[0] == '0') return false;
		if (s.Length > 10) return false;

		var n = long.Parse(s);
		return l <= n && n <= r;
	}
}
