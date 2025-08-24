class A100
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

	static int[] rn0;
	static int[,] d;

	static void Main()
	{
		var z = Read();
		(n, m, Q, L, W) = (z[0], z[1], z[2], z[3], z[4]);
		gcs = Read();
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		startTime = DateTime.Now;

		rn0 = Enumerable.Range(0, n).ToArray();

		d = new int[n, n];
		for (int i = 0; i < n; i++)
		{
			var pi = ps[i];
			var (xi, yi) = ((pi[0] + pi[1]) / 2.0, (pi[2] + pi[3]) / 2.0);
			for (int j = 0; j < n; j++)
			{
				var pj = ps[j];
				var (xj, yj) = ((pj[0] + pj[1]) / 2.0, (pj[2] + pj[3]) / 2.0);
				var (dx, dy) = (xi - xj, yi - yj);
				d[i, j] = (int)Math.Sqrt(dx * dx + dy * dy);
			}
		}

		var results = new (double score, int[])[ThreadsCount];
		Parallel.For(0, ThreadsCount, i => results[i] = SolveOne());
		var (score, gmap) = results.MaxBy(p => p.score);

		var gs = rn0.GroupBy(i => gmap[i]).OrderBy(g => g.Key).Select(g => g.ToArray()).ToArray();
		var ges = gs.Select(g => Enumerable.Range(0, g.Length - 1).Select(j => (g[j], g[j + 1])).ToArray()).ToArray();

#if !DEBUG
		for (int gi = 0; gi < m; gi++)
		{
			if (3 <= gs[gi].Length && gs[gi].Length <= L)
				ges[gi] = Query(gs[gi]);
		}
#endif

#if DEBUG
		Console.WriteLine(score);
		Console.WriteLine(Loops);
		Console.WriteLine($"{(int)CurrentTime} ms");

		var outFileName = $"{DateTime.Now:yyyyMMdd-HHmmss}.txt";
		using var outWriter = File.CreateText(outFileName);
		Console.SetOut(outWriter);
#endif
		Console.WriteLine("!");
		for (int gi = 0; gi < m; gi++)
		{
			Console.WriteLine(string.Join(" ", gs[gi]));
			for (int j = 0; j < ges[gi].Length; j++)
			{
				var (u, v) = ges[gi][j];
				Console.WriteLine($"{u} {v}");
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

	static void Shuffle<T>(T[] a) => Shuffle(a, 0, a.Length);
	static void Shuffle<T>(T[] a, int start, int count)
	{
		for (int i = count - 1; i > 0; --i)
		{
			var j = random.Next(i + 1);
			(a[start + i], a[start + j]) = (a[start + j], a[start + i]);
		}
	}
}
