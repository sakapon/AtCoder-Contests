using System;

namespace CoderLib8.Numerics
{
	// 列挙時に配列の内容が変更されます (同一の配列を参照)。
	public static class Combinatorics
	{
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

		public static void Combination<T>(T[] values, int r, Action<T[]> action)
		{
			var n = values.Length;
			var p = new T[r];

			if (r > 0) Dfs(0, 0);
			else action(p);

			void Dfs(int i, int j0)
			{
				var i2 = i + 1;
				for (int j = j0; j < n; ++j)
				{
					p[i] = values[j];

					if (i2 < r) Dfs(i2, j + 1);
					else action(p);
				}
			}
		}

		public static void Power<T>(T[] values, int r, Action<T[]> action)
		{
			var n = values.Length;
			var p = new T[r];

			if (r > 0) Dfs(0);
			else action(p);

			void Dfs(int i)
			{
				var i2 = i + 1;
				for (int j = 0; j < n; ++j)
				{
					p[i] = values[j];

					if (i2 < r) Dfs(i2);
					else action(p);
				}
			}
		}
	}
}
