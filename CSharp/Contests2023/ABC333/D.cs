class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = ToMap(n + 1, es, false);
		var subs = new int[n + 1];

		DFS(1, -1);
		return n - map[1].Max(v => subs[v]);

		void DFS(int v, int pv)
		{
			subs[v]++;

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				DFS(nv, v);
				subs[v] += subs[nv];
			}
		}
	}

	static List<int>[] ToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
