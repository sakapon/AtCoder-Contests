class A121
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }

	static DateTime startTime;
	public static double CurrentTime => (DateTime.Now - startTime).TotalMilliseconds;

	static int n, m, Q, L, W;
	static int[] gcs;
	static int[][] ps0;

	static int[] rn0;
	static (double x, double y)[] ps;
	static int[,] dMatrix;
	static PriorityQueue<(int, int), int> pq = new PriorityQueue<(int, int), int>();

	class Group
	{
		public int[] CityIds;
		public (int, int)[] Edges;
	}

	static void Main()
	{
		var z = Read();
		(n, m, Q, L, W) = (z[0], z[1], z[2], z[3], z[4]);
		gcs = Read();
		ps0 = Array.ConvertAll(new bool[n], _ => Read());

		startTime = DateTime.Now;

		rn0 = Enumerable.Range(0, n).ToArray();
		ps = Array.ConvertAll(ps0, p => ((p[0] + p[1]) / 2.0, (p[2] + p[3]) / 2.0));

		dMatrix = new int[n, n];
		for (int i = 0; i < n; i++)
		{
			var (xi, yi) = ps[i];
			for (int j = 0; j < n; j++)
			{
				var (xj, yj) = ps[j];
				xj -= xi; yj -= yi;
				dMatrix[i, j] = (int)Math.Sqrt(xj * xj + yj * yj);
			}
		}

		var groups = new Group[m];
		var u = new bool[n];
		var pq = new PriorityQueue<(int, int), int>();

		var waitingIds = rn0.ToList();
		var (px_sum, py_sum) = (ps.Sum(p => p.x), ps.Sum(p => p.y));
		var (px_avg, py_avg) = (px_sum / n, py_sum / n);

		double GetDistance2FromCenter(int id)
		{
			var (xi, yi) = ps[id];
			xi -= px_avg; yi -= py_avg;
			return xi * xi + yi * yi;
		}

		foreach (var gi in Enumerable.Range(0, m).OrderBy(j => gcs[j]))
		{
			var id0 = waitingIds.MaxBy(GetDistance2FromCenter);
			var (d_sum, g) = CreateGroup(waitingIds, u, id0, gcs[gi]);
			groups[gi] = g;

			foreach (var id in g.CityIds)
			{
				px_sum -= ps[id].x; py_sum -= ps[id].y;
			}
			(px_avg, py_avg) = (px_sum / waitingIds.Count, py_sum / waitingIds.Count);
		}

		if (waitingIds.Count > 0) throw new InvalidOperationException();
		if (!Array.TrueForAll(u, b => b)) throw new InvalidOperationException();

#if !DEBUG
		for (int gi = 0; gi < m; gi++)
		{
			if (3 <= gcs[gi] && gcs[gi] <= L)
				groups[gi].Edges = Query(groups[gi].CityIds);
			else if (gcs[gi] > L)
			{
				var qr = Query(groups[gi].CityIds[..L]);
				Array.Copy(qr, groups[gi].Edges, qr.Length);
			}
		}
#endif

#if DEBUG
		Console.WriteLine($"{(int)CurrentTime} ms");

		var outFileName = $"{DateTime.Now:yyyyMMdd-HHmmss}.txt";
		using var outWriter = File.CreateText(outFileName);
		Console.SetOut(outWriter);
#endif
		Console.WriteLine("!");
		foreach (var g in groups)
		{
			Console.WriteLine(string.Join(" ", g.CityIds));
			foreach (var (v1, v2) in g.Edges)
				Console.WriteLine($"{v1} {v2}");
		}
	}

	static (int d_sum, Group) CreateGroup(List<int> waitingIds, bool[] u, int id0, int count)
	{
		var d_sum = 0;
		var ids = new List<int>();
		var edges = new List<(int, int)>();

		while (count-- > 0)
		{
			var id = GetCityId();
			waitingIds.Remove(id);
			u[id] = true;
			ids.Add(id);

			foreach (var id2 in waitingIds)
				pq.Enqueue((id, id2), dMatrix[id, id2]);

			int GetCityId()
			{
				if (pq.Count == 0) return id0;

				while (true)
				{
					pq.TryDequeue(out var p, out var d);
					var (v1, v2) = p;
					if (u[v2]) continue;
					d_sum += d;
					edges.Add((v1, v2));
					return v2;
				}
			}
		}

		pq.Clear();
		var g = new Group
		{
			CityIds = ids.ToArray(),
			Edges = edges.ToArray()
		};
		return (d_sum, g);
	}

	static (int, int)[] Query(int[] gis)
	{
		var l = gis.Length;
		Console.WriteLine($"? {l} " + string.Join(" ", gis));
		return Array.ConvertAll(new bool[l - 1], _ => Read2());
	}

	static readonly Random random = new Random();

	static void Shuffle<T>(IList<T> a) => Shuffle(a, 0, a.Count);
	static void Shuffle<T>(IList<T> a, int start, int count)
	{
		for (int i = count - 1; i > 0; --i)
		{
			var j = random.Next(i + 1);
			(a[start + i], a[start + j]) = (a[start + j], a[start + i]);
		}
	}
}
