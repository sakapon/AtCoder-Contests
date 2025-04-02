class A110
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }

	const int ThreadsCount = 1 << 4;
	const int AnnealingRate = 10000;
	const int Timeout = 1800;

	static DateTime startTime;
	public static double CurrentTime => (DateTime.Now - startTime).TotalMilliseconds;
	public static int Loops;

	static int n, m, Q, L, W;
	static int[] gcs;
	static int[][] ps0;

	static int[] rn0;
	static (double, double)[] ps;
	static int[,] d;

	class Group
	{
		public List<int> CityIds = new List<int>();
		public List<(int, int)> Edges = new List<(int, int)>();
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
				var (dx, dy) = (xi - xj, yi - yj);
				d[i, j] = (int)Math.Sqrt(dx * dx + dy * dy);
			}
		}

		var groups = new Group[m];
		var ids0 = rn0.ToList();
		Shuffle(ids0);
		var u = new bool[n];
		foreach (var gi in Enumerable.Range(0, m).OrderBy(j => -gcs[j]))
		{
			var g = new Group();
			groups[gi] = g;

			var pq = new PriorityQueue<(int, int), int>();

			var c = gcs[gi];
			while (c-- > 0)
			{
				if (pq.Count == 0)
				{
					var id = ids0[^1];
					ids0.RemoveAt(ids0.Count - 1);
					u[id] = true;
					g.CityIds.Add(id);

					foreach (var id2 in ids0)
						pq.Enqueue((id, id2), d[id, id2]);
				}
				else
				{
					var id = -1;
					while (true)
					{
						var (v1, v2) = pq.Dequeue();
						if (!u[v2])
						{
							g.Edges.Add((v1, v2));
							id = v2;
							break;
						}
					}

					ids0.Remove(id);
					u[id] = true;
					g.CityIds.Add(id);

					foreach (var id2 in ids0)
						pq.Enqueue((id, id2), d[id, id2]);
				}
			}
		}

#if !DEBUG
		for (int gi = 0; gi < m; gi++)
		{
			if (3 <= gcs[gi] && gcs[gi] <= L)
				groups[gi].Edges = Query(groups[gi].CityIds.ToArray()).ToList();
		}
#endif

#if DEBUG
		//Console.WriteLine(score);
		//Console.WriteLine(Loops);
		Console.WriteLine($"{(int)CurrentTime} ms");

		var outFileName = $"{DateTime.Now:yyyyMMdd-HHmmss}.txt";
		using var outWriter = File.CreateText(outFileName);
		Console.SetOut(outWriter);
#endif
		Console.WriteLine("!");
		for (int gi = 0; gi < m; gi++)
		{
			Console.WriteLine(string.Join(" ", groups[gi].CityIds));
			foreach (var (v1, v2) in groups[gi].Edges)
			{
				Console.WriteLine($"{v1} {v2}");
			}
		}
	}

	// Annealing
	static (double, int[]) SolveOne()
	{
		var rn = (int[])rn0.Clone();
		Shuffle(rn);

		var gmap = new int[n];
		for (int gi = 0, i = -1; gi < m; gi++)
			for (int j = 0; j < gcs[gi]; j++)
				gmap[rn[++i]] = gi;

		var score = GetScore(gmap);

		for (double t; (t = CurrentTime) < Timeout; ++Loops)
		{
			var (i, j) = NextInt2();

			(gmap[i], gmap[j]) = (gmap[j], gmap[i]);
			var newScore = GetScore(gmap);

			if (IsValidForScore(score, newScore, t))
				score = newScore;
			else
				(gmap[i], gmap[j]) = (gmap[j], gmap[i]);
		}

		return (score, gmap);
	}

	static (int, int)[] Query(int[] gis)
	{
		var l = gis.Length;
		Console.WriteLine($"? {l} " + string.Join(" ", gis));
		return Array.ConvertAll(new bool[l - 1], _ => Read2());
	}

	static double GetScore(int[] gmap)
	{
		var prev = new int[m];
		Array.Fill(prev, -1);

		var d_sum = 0;
		for (int i = 0; i < n; ++i)
		{
			var gi = gmap[i];
			if (prev[gi] != -1)
				d_sum += d[prev[gi], i];
			prev[gi] = i;
		}
		return 1000000000.0 / d_sum;
	}

	static readonly Random random = new Random();

	static bool IsValidForScore(double oldScore, double newScore, double t) => IsValidForDelta(oldScore, newScore - oldScore, t);
	static bool IsValidForDelta(double oldScore, double delta, double t) => delta >= 0 || random.NextDouble() < Math.Exp(AnnealingRate * delta / oldScore * Timeout / (Timeout - t));

	static (int, int) NextInt2()
	{
		var n1 = random.Next(n);
		while (true)
		{
			var n2 = random.Next(n);
			if (n1 > n2) (n1, n2) = (n2, n1);
			if (n2 - n1 == 0) continue;
			return (n1, n2);
		}
	}

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
