using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Combinatorics
{
	// n 個の球を k 個の箱に入れる方法を列挙します。
	// 球を区別します。
	// 箱を区別します。
	public static class GroupingLabeledHelper
	{
		#region 0 個以上
		// k^n 通り

		public static void Assign0(int n, int k, Func<List<int>[], bool> action)
		{
			var b = Array.ConvertAll(new bool[k], _ => new List<int>());
			DFS(0);

			bool DFS(int v)
			{
				if (v == n) return action(b);

				for (int i = 0; i < k; ++i)
				{
					b[i].Add(v);
					if (DFS(v + 1)) return true;
					b[i].RemoveAt(b[i].Count - 1);
				}
				return false;
			}
		}

		// 辞書順
		public static void Assign0ForBalls(int n, int k, Func<int[], bool> action)
		{
			var p = new int[n];
			DFS(0);

			bool DFS(int v)
			{
				if (v == n) return action(p);

				for (int i = 0; i < k; ++i)
				{
					p[v] = i;
					if (DFS(v + 1)) return true;
				}
				return false;
			}
		}

		public static void Assign0ForBalls<T>(int n, T[] a, Func<T[], bool> action)
		{
			var k = a.Length;
			var p = new T[n];
			DFS(0);

			bool DFS(int v)
			{
				if (v == n) return action(p);

				for (int i = 0; i < k; ++i)
				{
					p[v] = a[i];
					if (DFS(v + 1)) return true;
				}
				return false;
			}
		}

		#endregion

		#region 1 個以上

		public static void Assign1(int n, int k, Func<List<int>[], bool> action)
		{
			if (n < k) return;
			var b = Array.ConvertAll(new bool[k], _ => new List<int>());
			var c0 = k;
			DFS(0);

			bool DFS(int v)
			{
				if (v == n) return action(b);

				var f = n - v == c0;
				for (int i = 0; i < k; ++i)
				{
					if (f && b[i].Count > 0) continue;
					if (b[i].Count == 0) --c0;
					b[i].Add(v);
					if (DFS(v + 1)) return true;
					b[i].RemoveAt(b[i].Count - 1);
					if (b[i].Count == 0) ++c0;
				}
				return false;
			}
		}

		// 辞書順
		public static void Assign1ForBalls(int n, int k, Func<int[], bool> action)
		{
			if (n < k) return;
			var p = new int[n];
			var c = new int[k];
			var c0 = k;
			DFS(0);

			bool DFS(int v)
			{
				if (v == n) return action(p);

				var f = n - v == c0;
				for (int i = 0; i < k; ++i)
				{
					if (f && c[i] > 0) continue;
					if (c[i]++ == 0) --c0;
					p[v] = i;
					if (DFS(v + 1)) return true;
					if (--c[i] == 0) ++c0;
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
