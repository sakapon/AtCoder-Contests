using Oomph.Data.UF09Lib.UFs.v301;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var (n, m, k) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read3());

		var r = new List<int>();
		var es1 = new Queue<int>();

		var eCount1 = es.Sum(e => e.Item3);
		var uf = new UnionFind(n + 1);
		var uf2 = new UnionFind(n + 1);

		foreach (var (u, v, c) in es)
		{
			if (c == 1) continue;

			uf.Union(u, v);
		}

		for (int ei = 1; ei <= m; ei++)
		{
			var (u, v, c) = es[ei - 1];
			if (c == 0) continue;

			if (uf.Union(u, v))
			{
				r.Add(ei);
				uf2.Union(u, v);
			}
			else
			{
				es1.Enqueue(ei);
			}
		}
		if (r.Count > k) return -1;

		while (r.Count < k)
		{
			if (es1.Count == 0) return -1;
			var ei = es1.Dequeue();
			var (u, v, c) = es[ei - 1];

			if (uf2.Union(u, v))
			{
				r.Add(ei);
			}
		}

		for (int ei = 1; ei <= m; ei++)
		{
			var (u, v, c) = es[ei - 1];
			if (c == 1) continue;

			if (uf2.Union(u, v))
			{
				r.Add(ei);
			}
		}

		return string.Join(" ", r);
	}
}
