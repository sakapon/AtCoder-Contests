using System;

namespace CoderLib6.Numerics
{
	// 列挙時に配列の内容が変更されます (同一の配列を参照)。
	public static class Combinatorics
	{
		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/5/ITP2_5_C
		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/5/ITP2_5_D
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

		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/11/ITP2_11_D
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

		public static void Power<T>(T[] values, int r, Action<T[]> action)
		{
			var p = new T[r];

			Action<int> Dfs = null;
			Dfs = i =>
			{
				for (int j = 0; j < values.Length; ++j)
				{
					p[i] = values[j];
					if (i + 1 < r) Dfs(i + 1); else action(p);
				}
			};
			if (r > 0) Dfs(0); else action(p);
		}
	}
}
