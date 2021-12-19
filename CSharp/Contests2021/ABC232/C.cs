using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var abs = Array.ConvertAll(new bool[m], _ => Read2());
		var cds = Array.ConvertAll(new bool[m], _ => Read2());

		var r = false;
		Permutation(Enumerable.Range(1, n).ToArray(), n, p =>
		{
			if (r) return;

			var abset = abs.Select(t =>
			{
				var (a, b) = t;
				(a, b) = (p[a - 1], p[b - 1]);
				return (Math.Min(a, b), Math.Max(a, b));
			}).ToHashSet();

			if (abset.SetEquals(cds)) r = true;
		});
		return r;
	}

	public static void Permutation<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];
		var u = new bool[n];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				if (u[j]) continue;
				p[i] = values[j];
				u[j] = true;

				if (i2 < r) Dfs(i2);
				else action(p);

				u[j] = false;
			}
		}
	}
}
