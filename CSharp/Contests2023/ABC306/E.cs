using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k, qc) = Read3();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var a = new int[n];
		var set = new WBMultiSet<int>(ComparerHelper<int>.Create(true));
		set.Initialize(a, true);
		var s = 0L;
		var r = new List<long>();

		if (k == n)
		{
			foreach (var q in qs)
			{
				var x = q.Item1 - 1;
				var y = q.Item2;

				s += y - a[x];
				a[x] = y;
				r.Add(s);
			}
		}
		else
		{
			foreach (var q in qs)
			{
				var x = q.Item1 - 1;
				var y = q.Item2;

				var node_k_ = set.GetAt(k - 1);
				if (y > node_k_.Item)
					s += y - node_k_.Item;
				set.Add(y);

				var node_k = set.GetAt(k);
				if (a[x] > node_k.Item)
					s -= a[x] - node_k.Item;
				set.Remove(a[x]);

				a[x] = y;
				r.Add(s);
			}
		}
		return string.Join("\n", r);
	}
}
