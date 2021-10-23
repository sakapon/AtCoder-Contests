using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Takahashi" : "Aoki");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		return Enumerable.Range(0, n).First(i => s[i] == '1') % 2 == 0;
	}
}
