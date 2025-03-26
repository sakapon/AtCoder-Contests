using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class A46_Single
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var o = new A46_Single();
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

	A46_Single()
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
			var (i, j) = NextInt2();
			Array.Reverse(t, i + 1, j - i);
			if (!Maximize(t))
				Array.Reverse(t, i + 1, j - i);
		}
	}

	double GetScore(int[] sol)
	{
		var s = 0.0;
		for (int i = 0; i < n; ++i)
			s += d[sol[i], sol[i + 1]];
		return 1000000 / s;
	}

	bool Maximize(int[] sol)
	{
		var s = GetScore(sol);
		if (score >= s) return false;
		score = s;
		result = (int[])sol.Clone();
		return true;
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
}
