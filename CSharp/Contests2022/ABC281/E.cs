using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var a = ReadL();

		var r = new long[n - m + 1];

		var b = a[0..m];
		Array.Sort(b);

		var s = b[0..k].Sum();
		var set = new WBMultiSet<long>();
		set.Initialize(b, true);

		for (int i = 0; i < n - m + 1; i++)
		{
			r[i] = s;

			if (i == n - m) continue;
			var v1 = a[i];
			var v2 = a[m + i];

			var i2 = set.GetFirstIndex(x => x >= v2);
			if (i2 < k)
			{
				s -= set.GetAt(k - 1).Item;
				s += v2;
			}
			set.Add(v2);

			var i1 = set.GetFirstIndex(v1);
			if (i1 < k)
			{
				s -= v1;
				s += set.GetAt(k).Item;
			}
			set.Remove(v1);
		}
		return string.Join(" ", r);
	}
}
