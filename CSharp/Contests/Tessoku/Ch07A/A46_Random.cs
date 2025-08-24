using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class A46_Random
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var o = new A46_Random();
		Task.Run(() => o.Solve());
		Thread.Sleep(990);

		Console.WriteLine(string.Join("\n", o.result.Select(i => i + 1)));
		//Console.WriteLine(o.score);
		//Console.WriteLine(o.k);
	}

	readonly int n;
	readonly double[,] d;

	int k;
	double score;
	int[] result;

	A46_Random()
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
	}

	object Solve()
	{
		var t = Enumerable.Range(0, n + 1).ToArray();
		t[^1] = 0;

		for (; ; ++k)
		{
			Shuffle(t);
			Maximize(t);
		}
	}

	double GetScore(int[] sol)
	{
		var s = 0.0;
		for (int i = 0; i < n; ++i)
			s += d[sol[i], sol[i + 1]];
		return 1000000 / s;
	}

	void Maximize(int[] sol)
	{
		var s = GetScore(sol);
		if (score >= s) return;
		score = s;
		result = (int[])sol.Clone();
	}

	static readonly Random random = new Random();
	void Shuffle<T>(T[] a)
	{
		for (int i = 1; i < n; ++i)
		{
			var j = random.Next(n - i);
			(a[j + 1], a[^(i + 1)]) = (a[^(i + 1)], a[j + 1]);
		}
	}
}
