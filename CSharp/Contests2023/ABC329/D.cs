using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		var r = new List<int>();
		var v = new int[n + 1];
		var set = new SortedSet<(int v, int i)>();

		foreach (var i in a)
		{
			set.Remove((v[i], i));
			v[i]--;
			set.Add((v[i], i));

			r.Add(set.Min.i);
		}

		return string.Join("\n", r);
	}
}
