class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int u, int v, int w) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var x = Read().Prepend(0).ToArray();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read3());

		var map = ToMap(n + 1, es, true);
		var r = 0L;
		DFS(1, -1);
		return r;

		void DFS(int v, int pv)
		{
			foreach (var ei in map[v])
			{
				var (nv, nv2, w) = es[ei];
				if (nv == v) nv = nv2;
				if (nv == pv) continue;
				DFS(nv, v);
				x[v] += x[nv];
				r += (long)Math.Abs(x[nv]) * w;
			}
		}
	}

	public static int[][] ToMap(int n, (int u, int v, int w)[] es, bool twoway)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		for (int ei = 0; ei < es.Length; ei++)
		{
			var (u, v, w) = es[ei];
			map[u].Add(ei);
			if (twoway) map[v].Add(ei);
		}
		return Array.ConvertAll(map, l => l.ToArray());
	}
}
