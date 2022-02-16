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
		var a = Read();

		var l = new List<int> { 360, 0 };

		foreach (var x in a)
		{
			l.Add((l[^1] + x) % 360);
		}
		l.Sort();

		return Enumerable.Range(0, n + 1).Max(i => l[i + 1] - l[i]);
	}
}
