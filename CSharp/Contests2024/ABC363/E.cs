class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, y) = Read3();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		int ToVertexId(int i, int j) => w * i + j;
		(int i, int j) FromVertexId(int v) => (v / w, v % w);

		var n = h * w;
		var s = n;
		var u = new bool[n];
		var q = new SortedSet<(int l, int v)>();

		for (int j = 0; j < w; j++)
		{
			q.Add((a[0][j], ToVertexId(0, j)));
			q.Add((a[h - 1][j], ToVertexId(h - 1, j)));
		}
		for (int i = 0; i < h; i++)
		{
			q.Add((a[i][0], ToVertexId(i, 0)));
			q.Add((a[i][w - 1], ToVertexId(i, w - 1)));
		}

		int[] GetAllNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int>();
			if (j > 0) l.Add(v - 1);
			if (j + 1 < w) l.Add(v + 1);
			if (i > 0) l.Add(v - w);
			if (i + 1 < h) l.Add(v + w);
			return l.ToArray();
		}

		var r = new int[y];
		for (int k = 1; k <= y; k++)
		{
			while (q.Count > 0 && q.Min.l <= k)
			{
				var (l, v) = q.Min;
				q.Remove((l, v));
				u[v] = true;
				s--;

				foreach (var nv in GetAllNexts(v))
				{
					if (u[nv]) continue;
					var (ni, nj) = FromVertexId(nv);
					q.Add((a[ni][nj], nv));
				}
			}
			r[k - 1] = s;
		}
		return string.Join("\n", r);
	}
}
