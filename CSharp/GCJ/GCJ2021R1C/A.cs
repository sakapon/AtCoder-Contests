using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select((_, i) => $"Case #{i + 1}: {Solve()}")));
	static object Solve()
	{
		var (n, k) = Read2();
		var a = Read().OrderBy(x => x).ToList();

		a.Insert(0, 0);
		a.Add(k + 1);

		var ds = Enumerable.Range(0, n + 1)
			.Select(i => new Dist { d = a[i + 1] - a[i] - 1, isEdge = i == 0 || i == n })
			.Where(_ => _.d > 0)
			.ToArray();

		return (double)GetCount(ds) / k;
	}

	static int GetCount(Dist[] ds)
	{
		if (ds.Length == 0) return 0;
		if (ds.Length == 1) return ds[0].d;

		// Choose 1 range.
		var r1 = ds.Max(_ => _.d);

		// Choose 2 ranges.
		var r2 = ds.Select(_ => _.isEdge ? _.d : (_.d + 1) / 2).OrderBy(x => -x).Take(2).Sum();

		return Math.Max(r1, r2);
	}

	struct Dist
	{
		public int d;
		public bool isEdge;
	}
}
