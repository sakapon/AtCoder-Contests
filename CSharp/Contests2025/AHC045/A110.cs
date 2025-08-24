class A110
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }

	static DateTime startTime;
	public static double CurrentTime => (DateTime.Now - startTime).TotalMilliseconds;

	static int n, m, Q, L, W;
	static int[] gcs;
	static int[][] ps0;

	static int[] rn0;
	static (double, double)[] ps;
	static int[,] d;

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

		d = new int[n, n];
		for (int i = 0; i < n; i++)
		{
			var (xi, yi) = ps[i];
			for (int j = 0; j < n; j++)
			{
				var (xj, yj) = ps[j];
				xj -= xi; yj -= yi;
				d[i, j] = (int)Math.Sqrt(xj * xj + yj * yj);
			}
		}

		var groups = new Group[m];
		var ids0 = rn0.ToList();
		Shuffle(ids0);
		var u = new bool[n];
		var pq = new PriorityQueue<(int, int), int>();

		foreach (var gi in Enumerable.Range(0, m).OrderBy(j => -gcs[j]))
		{
			var g = new Group();
			groups[gi] = g;

			var ids = new List<int>();
			var edges = new List<(int, int)>();

			var c = gcs[gi];
			while (c-- > 0)
			{
				var id = GetCityId();
				ids0.Remove(id);
				u[id] = true;
				ids.Add(id);

				foreach (var id2 in ids0)
					pq.Enqueue((id, id2), d[id, id2]);

				int GetCityId()
				{
					if (pq.Count == 0)
					{
						return ids0[^1];
					}
					else
					{
						while (true)
						{
							var (v1, v2) = pq.Dequeue();
							if (u[v2]) continue;
							edges.Add((v1, v2));
							return v2;
						}
					}
				}
			}

			g.CityIds = ids.ToArray();
			g.Edges = edges.ToArray();
			pq.Clear();
		}

		if (!Array.TrueForAll(u, b => b)) throw new InvalidOperationException();

#if !DEBUG
		for (int gi = 0; gi < m; gi++)
		{
			if (3 <= gcs[gi] && gcs[gi] <= L)
				groups[gi].Edges = Query(groups[gi].CityIds);
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
