using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine().Select(c => c - '0').ToArray();

		var s0 = s[..^1];

		var d = s0.Where((v, i) => i % 2 == 0).Sum() * 3;
		d += s0.Where((v, i) => i % 2 == 1).Sum();
		d %= 10;

		return s[^1] == d;
	}
}
