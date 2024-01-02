class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());
		var b = Array.ConvertAll(new bool[h], _ => Read());

		var sv = ToString(a);
		var ev = ToString(b);

		return BFS(s =>
		{
			var m = Parse(s);
			var nexts = new List<string>();
			for (int i = 0; i < m.Length - 1; i++)
			{
				SwapYoko(m, i);
				nexts.Add(ToString(m));
				SwapYoko(m, i);
			}
			for (int j = 0; j < m[0].Length - 1; j++)
			{
				SwapTate(m, j);
				nexts.Add(ToString(m));
				SwapTate(m, j);
			}
			return nexts.ToArray();
		}, sv, ev);
	}

	static string ToString(int[][] a)
	{
		return string.Join("\n", a.Select(c => string.Join(" ", c)));
	}
	static int[][] Parse(string s)
	{
		return s.Split('\n').Select(t => Array.ConvertAll(t.Split(), int.Parse)).ToArray();
	}

	static void SwapYoko(int[][] a, int i)
	{
		(a[i], a[i + 1]) = (a[i + 1], a[i]);
	}
	static void SwapTate(int[][] a, int j)
	{
		for (int i = 0; i < a.Length; i++)
		{
			(a[i][j], a[i][j + 1]) = (a[i][j + 1], a[i][j]);
		}
	}

	public static int[] GetColumn(int[][] m, int c) => Array.ConvertAll(m, r => r[c]);
	public static int[][] Transpose(int[][] m) => m[0].Select((x, c) => GetColumn(m, c)).ToArray();

	public static int BFS(Func<string, string[]> nexts, string sv, string ev)
	{
		var costs = new Dictionary<string, int>();
		var q = new Queue<string>();
		costs[sv] = 0;
		if (sv == ev) return costs[sv];
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			foreach (var nv in nexts(v))
			{
				if (costs.ContainsKey(nv)) continue;
				costs[nv] = nc;
				if (nv == ev) return costs[nv];
				q.Enqueue(nv);
			}
		}
		return -1;
	}
}
