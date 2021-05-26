using System;
using System.Collections;
using System.Collections.Generic;

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

		// n^r 通り
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

		// Test: https://atcoder.jp/contests/abc184/tasks/abc184_f
		// 2^n 通り
		// true を返すことでキャンセル可能
		// アドホックのコードと遜色ないほど高速
		public static void AllBoolCombination(int n, Func<bool[], bool> action)
		{
			if (n > 30) throw new InvalidOperationException();
			var pn = 1 << n;
			var b = new bool[n];

			for (int x = 0; x < pn; ++x)
			{
				for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;
				if (action(b)) break;
			}
		}

		// 2^n 通り
		// true を返すことでキャンセル可能
		// 少し低速だが問題ない
		[Obsolete]
		public static void AllBoolCombination(int n, Func<BitArray, bool> action)
		{
			if (n > 30) throw new InvalidOperationException();
			var pn = 1 << n;

			for (int x = 0; x < pn; ++x)
				if (action(new BitArray(new[] { x }))) break;
		}

		// 2^n 通り
		// true を返すことでキャンセル可能
		// 集計処理が簡単になるが、かなり低速
		public static void AllCombination<T>(T[] values, Func<T[], bool> action)
		{
			var n = values.Length;
			if (n > 30) throw new InvalidOperationException();
			var pn = 1 << n;

			var rn = new int[n];
			for (int i = 0; i < n; ++i) rn[i] = i;

			for (int x = 0; x < pn; ++x)
			{
				var indexes = Array.FindAll(rn, i => (x & (1 << i)) != 0);
				if (action(Array.ConvertAll(indexes, i => values[i]))) break;
			}
		}

		// n >= 20 (10^6) で低速となる場合はこのテンプレートを利用します。
		static long[] CombinationSums(int[] a)
		{
			var n = a.Length;
			var l = new List<long>();

			for (int x = 0; x < 1 << n; ++x)
			{
				var sum = 0L;
				for (int i = 0; i < n; ++i)
				{
					if ((x & (1 << i)) != 0)
					{
						sum += a[i];
					}
				}
				l.Add(sum);
			}
			// Without distinct or sort
			return l.ToArray();
		}

		// Test: https://atcoder.jp/contests/typical90/tasks/typical90_as
		public static void AllSubsets(int n, int s, Func<int, bool> action)
		{
			for (int x = 0; ; x = (x - s) & s)
			{
				if (action(x)) break;
				if (x == s) break;
			}
		}
		// 逆順
		//public static void AllSubsets(int n, int s, Func<int, bool> action)
		//{
		//	for (int x = s; ; x = (x - 1) & s)
		//	{
		//		if (action(x)) break;
		//		if (x == 0) break;
		//	}
		//}

		public static void AllSubsets(int n, int s, Func<bool[], bool> action)
		{
			var b = new bool[n];
			for (int x = 0; ; x = (x - s) & s)
			{
				for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;
				if (action(b)) break;
				if (x == s) break;
			}
		}

		public static void AllSupersets(int n, int s, Func<int, bool> action)
		{
			for (int x = s; x < 1 << n; x = (x + 1) | s)
			{
				if (action(x)) break;
			}
		}

		public static void AllSupersets(int n, int s, Func<bool[], bool> action)
		{
			var b = new bool[n];
			for (int x = s; x < 1 << n; x = (x + 1) | s)
			{
				for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;
				if (action(b)) break;
			}
		}
	}
}
