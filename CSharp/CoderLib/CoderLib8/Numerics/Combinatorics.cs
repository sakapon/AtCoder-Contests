using System;
using System.Collections;
using System.Collections.Generic;

namespace CoderLib8.Numerics
{
	// 列挙時に配列の内容が変更されます (同一の配列を参照)。
	public static class Combinatorics
	{
		// 値は何でもかまいません。重複可能。
		public static bool NextPermutation(int[] p)
		{
			var n = p.Length;

			// p[i] < p[i + 1] を満たす最大の i
			var i = n - 2;
			while (i >= 0 && p[i] >= p[i + 1]) i--;
			if (i == -1) return false;

			// p[i] < p[j] を満たす最大の j
			var j = i + 1;
			while (j + 1 < n && p[i] < p[j + 1]) j++;

			(p[i], p[j]) = (p[j], p[i]);
			Array.Reverse(p, i + 1, n - i - 1);
			return true;
		}

		// 値は何でもかまいません。重複可能。
		public static bool PrevPermutation(int[] p)
		{
			var n = p.Length;

			// p[i] > p[i + 1] を満たす最大の i
			var i = n - 2;
			while (i >= 0 && p[i] <= p[i + 1]) i--;
			if (i == -1) return false;

			// p[i] > p[j] を満たす最大の j
			var j = i + 1;
			while (j + 1 < n && p[i] > p[j + 1]) j++;

			(p[i], p[j]) = (p[j], p[i]);
			Array.Reverse(p, i + 1, n - i - 1);
			return true;
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

		// r が小さい場合、for の r 重ループを使います。
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
		// r が小さい場合、for の r 重ループを使います。
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

		// Test: https://atcoder.jp/contests/abc226/tasks/abc226_f
		// 分割数
		// n = 50 のとき、204226 通り
		public static void Partition(int n, Action<int[]> action)
		{
			Dfs(new[] { n });

			void Dfs(int[] p)
			{
				action(p);

				var v2 = p.Length == 1 ? 1 : p[^2];
				var v1 = p[^1] - v2;
				if (v2 > v1) return;

				var q = new int[p.Length + 1];
				Array.Copy(p, 0, q, 0, p.Length - 1);

				for (; v2 <= v1; ++v2, --v1)
				{
					q[^2] = v2;
					q[^1] = v1;
					Dfs(q);
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

		// Test: https://atcoder.jp/contests/typical90/tasks/typical90_cb
		// Test: https://atcoder.jp/contests/abc152/tasks/abc152_f
		// 包除原理
		// n 個の条件を 1 つも満たさない場合の数を求めます。
		// getCount により、2^n 通りの条件の組合せに対する場合の数が得られるとします。
		// getCount(0) は全ての場合の数を表します。
		// 剰余 (mod) は考慮されていません。
		public static long InclusionExclusion(int n, Func<bool[], long> getCount)
		{
			if (n > 30) throw new InvalidOperationException();
			var pn = 1 << n;
			var b = new bool[n];

			var r = 0L;
			for (uint x = 0; x < pn; ++x)
			{
				for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;

				// BitOperations.PopCount に変更してください。
				//var sign = BitOperations.PopCount(x) % 2 == 0 ? 1 : -1;
				var sign = Math2.PopCount(x) % 2 == 0 ? 1 : -1;
				r += sign * getCount(b);
			}
			return r;
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
