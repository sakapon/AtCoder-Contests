using CoderLib8.Values;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s0 = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var n2 = n * n;
		var rn = Enumerable.Range(0, n * n).ToArray();
		var s = s0.SelectMany(c => c).ToArray();
		var ps = rn.Where(v => s[v] == 'P').ToArray();

		var g = new Grid2<char>(n, n, s);

		IEnumerable<int> GetNexts(int v)
		{
			var (p1, p2) = (v / n2, v % n2);
			var (i1, j1) = (p1 / n, p1 % n);
			var (i2, j2) = (p2 / n, p2 % n);

			var (di, dj) = (-1, 0);
			var (np1, np2) = (p1, p2);

			(np1, np2) = (GetNext(i1, j1, di, dj), GetNext(i2, j2, di, dj));
			if (np1 != p1 || np2 != p2) yield return n2 * np1 + np2;
			(di, dj) = (dj, -di);
			(np1, np2) = (GetNext(i1, j1, di, dj), GetNext(i2, j2, di, dj));
			if (np1 != p1 || np2 != p2) yield return n2 * np1 + np2;
			(di, dj) = (dj, -di);
			(np1, np2) = (GetNext(i1, j1, di, dj), GetNext(i2, j2, di, dj));
			if (np1 != p1 || np2 != p2) yield return n2 * np1 + np2;
			(di, dj) = (dj, -di);
			(np1, np2) = (GetNext(i1, j1, di, dj), GetNext(i2, j2, di, dj));
			if (np1 != p1 || np2 != p2) yield return n2 * np1 + np2;
			(di, dj) = (dj, -di);
		}

		int GetNext(int i, int j, int di, int dj)
		{
			var ni = i + di;
			var nj = j + dj;
			if (!(0 <= ni && ni < n) || !(0 <= nj && nj < n) || g[ni, nj] == '#')
			{
				ni = i;
				nj = j;
			}
			return n * ni + nj;
		}

		var cs = ShortestByBFS(n2 * n2, v => GetNexts(v).ToArray(), n2 * ps[0] + ps[1]);
		var r = rn.Min(v => cs[n2 * v + v]);
		if (r == long.MaxValue) return -1;
		return r;
	}

	public static long[] ShortestByBFS(int n, Func<int, int[]> nexts, int sv, int ev = -1, long maxCost = long.MaxValue)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			if (v == ev) return costs;
			var nc = costs[v] + 1;
			if (nc > maxCost) return costs;

			foreach (var nv in nexts(v))
			{
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}
