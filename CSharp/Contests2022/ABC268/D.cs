using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());
		var tset = Array.ConvertAll(new bool[m], _ => Console.ReadLine()).ToHashSet();

		if (n == 1)
		{
			var x = ss[0];
			if (x.Length < 3 || tset.Contains(x)) return -1;
			return x;
		}

		var rn = Enumerable.Range(0, n).ToArray();
		var rn_2 = Enumerable.Range(0, n - 2).ToArray();

		// rem 文字余ります。
		var rem = 16 - (ss.Sum(s => s.Length) + n - 1);

		Combination(Enumerable.Range(0, rem + n - 1).ToArray(), n - 1, c =>
		{
			// _ の長さ
			var ls = rn_2.Select(i => c[i + 1] - c[i]).Prepend(c[0] + 1).ToArray();

			Permutation(rn, n, p =>
			{
				var sb = new StringBuilder();
				for (int i = 0; i < n; i++)
				{
					sb.Append(ss[p[i]]);
					if (i < n - 1) sb.Append('_', ls[i]);
				}
				var x = sb.ToString();
				if (x.Length < 3 || tset.Contains(x)) return;
				Console.WriteLine(x);
				Environment.Exit(0);
			});
		});

		return -1;
	}

	public static void Permutation<T>(T[] values, int r, Action<T[]> action)
	{
		var p = new T[r];
		var u = new bool[values.Length];

		Action<int> Dfs = null;
		Dfs = i =>
		{
			for (int j = 0; j < values.Length; ++j)
			{
				if (u[j]) continue;
				p[i] = values[j];
				u[j] = true;
				if (i + 1 < r) Dfs(i + 1); else action(p);
				u[j] = false;
			}
		};
		if (r > 0) Dfs(0); else action(p);
	}

	public static void Combination<T>(T[] values, int r, Action<T[]> action)
	{
		var p = new T[r];

		Action<int, int> Dfs = null;
		Dfs = (i, j0) =>
		{
			for (int j = j0; j < values.Length; ++j)
			{
				p[i] = values[j];
				if (i + 1 < r) Dfs(i + 1, j + 1); else action(p);
			}
		};
		if (r > 0) Dfs(0, 0); else action(p);
	}
}
