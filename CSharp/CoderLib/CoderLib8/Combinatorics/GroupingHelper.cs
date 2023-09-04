using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Combinatorics
{
	// 写像 12 相
	// n 個の球を k 個の箱に入れる方法を列挙します。
	// 球を区別します。
	// 箱を区別しません。

	//if (v == n - 1 ? action(b) : DFS(v + 1, i < i0 ? i0 : i0 + 1)) return true;
	// とすることで高速化できます。
	public static class GroupingHelper
	{
		#region 0 個以上

		public static void Assign0(int n, int k, Func<List<int>[], bool> action)
		{
			var b = Array.ConvertAll(new bool[k], _ => new List<int>());
			DFS(0, 0);

			// i0: 最初の空の箱の番号
			bool DFS(int v, int i0)
			{
				if (v == n) return action(b);

				for (int i = 0; i < k; ++i)
				{
					b[i].Add(v);
					if (DFS(v + 1, i < i0 ? i0 : i0 + 1)) return true;
					b[i].RemoveAt(b[i].Count - 1);
					if (i == i0) break;
				}
				return false;
			}
		}

		// 辞書順
		public static void Assign0ForBalls(int n, int k, Func<int[], bool> action)
		{
			var p = new int[n];
			DFS(0, 0);

			// i0: 最初の空の箱の番号
			bool DFS(int v, int i0)
			{
				if (v == n) return action(p);

				for (int i = 0; i < k; ++i)
				{
					p[v] = i;
					if (DFS(v + 1, i < i0 ? i0 : i0 + 1)) return true;
					if (i == i0) break;
				}
				return false;
			}
		}

		#endregion

		#region 1 個以上
		// Test: https://atcoder.jp/contests/abc310/tasks/abc310_d

		public static void Assign1(int n, int k, Func<List<int>[], bool> action)
		{
			if (n < k) return;
			var b = Array.ConvertAll(new bool[k], _ => new List<int>());
			DFS(0, 0);

			// i0: 最初の空の箱の番号
			bool DFS(int v, int i0)
			{
				if (v == n) return action(b);

				for (int i = k - i0 < n - v ? 0 : i0; i < k; ++i)
				{
					b[i].Add(v);
					if (DFS(v + 1, i < i0 ? i0 : i0 + 1)) return true;
					b[i].RemoveAt(b[i].Count - 1);
					if (i == i0) break;
				}
				return false;
			}
		}

		// 辞書順
		public static void Assign1ForBalls(int n, int k, Func<int[], bool> action)
		{
			if (n < k) return;
			var p = new int[n];
			DFS(0, 0);

			// i0: 最初の空の箱の番号
			bool DFS(int v, int i0)
			{
				if (v == n) return action(p);

				for (int i = k - i0 < n - v ? 0 : i0; i < k; ++i)
				{
					p[v] = i;
					if (DFS(v + 1, i < i0 ? i0 : i0 + 1)) return true;
					if (i == i0) break;
				}
				return false;
			}
		}

		// 集合を bit で表現
		public static void Assign1(int n, int k, Func<int[], bool> action)
		{
			if (n < k) return;
			var b = new int[k];
			DFS(0, 0);

			// i0: 最初の空の箱の番号
			bool DFS(int v, int i0)
			{
				if (v == n) return action(b);

				for (int i = k - i0 < n - v ? 0 : i0; i < k; ++i)
				{
					b[i] |= 1 << v;
					if (DFS(v + 1, i < i0 ? i0 : i0 + 1)) return true;
					b[i] &= ~(1 << v);
					if (i == i0) break;
				}
				return false;
			}
		}

		#endregion

		public static class Test
		{
			public static void Assign_Test()
			{
				var r = 0;
				Assign0(4, 3, gs =>
				{
					Console.WriteLine(string.Join(", ", gs.Select(g => string.Join(" ", g))));
					r++;
					return false;
				});
				Console.WriteLine(r);
			}

			public static void Assign_ForBalls_Test()
			{
				var r = 0;
				Assign0ForBalls(4, 3, p =>
				{
					Console.WriteLine(string.Join(" ", p));
					r++;
					return false;
				});
				Console.WriteLine(r);
			}
		}
	}
}
