class A46_Parallel
{
	const int ThreadsCount = 1 << 4;
	static void Main()
	{
		var results = new (double score, int[])[ThreadsCount];
		var o = new A46_Annealing2();
		Parallel.For(0, ThreadsCount, i => results[i] = o.Solve());
		var (score, r) = results.MaxBy(p => p.score);

		Console.WriteLine(string.Join("\n", r.Select(i => i + 1)));
#if DEBUG
		Console.WriteLine(score);
		Console.WriteLine(o.Loops);
		Console.WriteLine($"{(int)o.CurrentTime} ms");
#endif
	}
}

class A46_Annealing2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }

	const int TrialsCount = 300000;
	const int AnnealingRate = 500;
	const int Timeout = 990;
	readonly DateTime startTime;
	public double CurrentTime => (DateTime.Now - startTime).TotalMilliseconds;
	public int Loops;

	readonly int n;
	readonly double[,] d;
	readonly int[] path0;

	public A46_Annealing2()
	{
		n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		startTime = DateTime.Now;

		d = new double[n, n];
		for (int i = 0; i < n; i++)
		{
			var (xi, yi) = ps[i];
			for (int j = 0; j < n; j++)
			{
				var (xj, yj) = ps[j];
				var (dx, dy) = (xi - xj, yi - yj);
				d[i, j] = Math.Sqrt(dx * dx + dy * dy);
			}
		}

		path0 = Enumerable.Range(0, n + 1).ToArray();
		path0[^1] = 0;
	}

	public (double, int[]) Solve()
	{
		var path = (int[])path0.Clone();
		var maxScore = 0.0;
		var maxPath = path;

		while (CurrentTime < Timeout)
		{
			Shuffle(path, 1, n - 1);
			var score = GetScore(path);

			for (int c = 0; c < TrialsCount && CurrentTime < Timeout; ++c, ++Loops)
			{
				var (i, j) = NextInt2();
				Array.Reverse(path, i + 1, j - i);

				var s = GetScore(path);
				if (IsValid(score, s, c))
					score = s;
				else
					Array.Reverse(path, i + 1, j - i);
			}

			if (maxScore < score)
			{
				maxScore = score;
				maxPath = path.ToArray();
			}
		}

		return (maxScore, maxPath);
	}

	double GetScore(int[] sol)
	{
		var s = 0.0;
		for (int i = 0; i < n; ++i)
			s += d[sol[i], sol[i + 1]];
		return 1000000 / s;
	}

	static readonly Random random = new Random();

	static bool IsValid(double oldScore, double newScore, int c)
	{
		if (newScore >= oldScore) return true;
		return random.NextDouble() < Math.Exp(AnnealingRate * (newScore - oldScore) / oldScore * TrialsCount / (TrialsCount - c));
	}

	(int, int) NextInt2()
	{
		var n1 = random.Next(n);
		while (true)
		{
			var n2 = random.Next(n);
			if (n1 == n2) continue;
			return n1 < n2 ? (n1, n2) : (n2, n1);
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
