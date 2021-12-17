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

		var r7 = Enumerable.Range(0, 7).ToArray();
		var d = r7.Sum(i => s[2 * i]) * 3 + r7.Sum(i => s[2 * i + 1]);
		return s[^1] == d % 10;
	}
}
