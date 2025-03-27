using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class A46_Parallel
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var o = new A46_Parallel();
		Task.Run(() =>
		{
			Thread.Sleep(980);
			o.end = true;
		});
		Parallel.For(0, 100, o.Solve);

		var (score, r) = o.results.MaxBy(p => p.score);
		Console.WriteLine(string.Join("\n", r.Select(i => i + 1)));
		//Console.WriteLine(score);
		//Console.WriteLine(o.loops);
	}

	readonly int n;
	readonly double[,] d;

	bool end;
	int loops;
	readonly int[] path0;
	readonly (double score, int[])[] results = new (double, int[])[100];

	A46_Parallel()
	{
		n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

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

	void Solve(int k)
	{
		var score = 0.0;
		var path = (int[])path0.Clone();
		Shuffle(path);

		for (; !end; ++loops)
		{
			var (i, j) = NextInt2();
			Array.Reverse(path, i + 1, j - i);

			var s = GetScore(path);
			if (score < s)
				score = s;
			else
				Array.Reverse(path, i + 1, j - i);
		}

		results[k] = (score, path);
	}

	double GetScore(int[] sol)
	{
		var s = 0.0;
		for (int i = 0; i < n; ++i)
			s += d[sol[i], sol[i + 1]];
		return 1000000 / s;
	}

	static readonly Random random = new Random();
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

	void Shuffle<T>(T[] a)
	{
		for (int i = 1; i < n; ++i)
		{
			var j = random.Next(n - i);
			(a[j + 1], a[^(i + 1)]) = (a[^(i + 1)], a[j + 1]);
		}
	}
}
