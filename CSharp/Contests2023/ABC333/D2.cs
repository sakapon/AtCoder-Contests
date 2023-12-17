class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

		var rs = Enumerable.Range(0, n + 1).ToArray();
		var gs = Array.ConvertAll(rs, i => new List<int> { i });

		foreach (var (u, v) in es)
		{
			if (u == 1) continue;

			var l1 = gs[rs[u]];
			var l2 = gs[rs[v]];
			if (l1.Count < l2.Count) (l1, l2) = (l2, l1);

			foreach (var x in l2)
			{
				l1.Add(x);
				rs[x] = l1[0];
				gs[x] = l1;
			}
		}

		return n - gs.Max(l => l.Count);
	}
}
