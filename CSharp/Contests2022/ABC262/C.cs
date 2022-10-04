using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read().Prepend(-1).ToArray();

		var c = Enumerable.Range(1, n).LongCount(i => a[i] == i);
		var r = Enumerable.Range(1, n).LongCount(i => a[a[i]] == i);
		return c * (c - 1) / 2 + (r - c) / 2;
	}
}
