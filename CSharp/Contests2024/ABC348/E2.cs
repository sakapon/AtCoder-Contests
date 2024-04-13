class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var c = ReadL().Prepend(0).ToArray();

		var map = ToMap(n + 1, es, true);
		var degs = Array.ConvertAll(map, es => es.Length);
		var u = new bool[n + 1];
		var r = new long[n + 1];

		var rn = Enumerable.Range(1, n).ToArray();
		var q = new Queue<int>(rn.Where(v => degs[v] == 1));
		var rv = 0;

		while (q.Count > 0)
		{
			rv = q.Dequeue();
			u[rv] = true;

			foreach (var nv in map[rv])
			{
				if (--degs[nv] == 1) q.Enqueue(nv);

				if (u[nv]) continue;
				c[nv] += c[rv];
				r[nv] += r[rv] + c[rv];
			}
		}

		DFS2(rv, -1, 0, 0);
		return r[1..].Min();

		void DFS2(int v, int pv, long s1, long s2)
		{
			var t = r[v];
			r[v] += s1 + s2;

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				DFS2(nv, v, s1 + c[v] - c[nv], s1 + s2 + t - r[nv] - c[nv]);
			}
		}
	}

	public static int[][] ToMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (twoWay) map[e[1]].Add(e[0]);
		}
		return Array.ConvertAll(map, l => l.ToArray());
	}
}
