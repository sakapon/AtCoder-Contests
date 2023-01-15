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
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var r = Enumerable.Range(1, n - 1)
			.Select(i => Enumerable.Range(1, n - i).TakeWhile(j => s[j - 1] != s[j + i - 1]).LastOrDefault());
		return string.Join("\n", r);
	}
}
