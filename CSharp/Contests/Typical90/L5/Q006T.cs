using System;
using System.Collections.Generic;
using CoderLib6.DataTrees;

class Q006T
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = Console.ReadLine();

		var r = new List<char>();

		var t = -1;
		int ti;
		var set = AvlTree<int>.Create(i => s[i] * 100000L + i);
		for (int i = 0; i < n - k; i++)
		{
			set.Add(i);
		}

		for (int i = n - k; i < n; i++)
		{
			set.Add(i);
			while ((ti = set.GetMin()) < t) set.Remove(ti);

			set.Remove(ti);
			t = ti;
			r.Add(s[t]);
		}

		return string.Join("", r);
	}
}
