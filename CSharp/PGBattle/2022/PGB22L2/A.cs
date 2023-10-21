using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		// true: 裏
		var b = new bool[n];

		foreach (var (l, r) in qs)
		{
			for (int i = l - 1; i < r; i++)
			{
				b[i] ^= true;
			}
		}
		return b.Count(v => v);
	}
}
