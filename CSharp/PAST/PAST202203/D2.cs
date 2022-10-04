using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (t, n) = Read2();
		var p = Array.ConvertAll(new bool[t], _ => Read());

		var rt = Enumerable.Range(0, t).ToArray();
		var rn = Enumerable.Range(0, n).ToArray();
		return string.Join("\n", rt.Select(c => rn.Sum(j => rt[..(c + 1)].Max(i => p[i][j]))));
	}
}
