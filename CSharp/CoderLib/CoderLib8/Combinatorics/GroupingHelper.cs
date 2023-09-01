using System;
using System.Collections.Generic;

namespace CoderLib8.Combinatorics
{
	// n 個の球を k 個の箱に入れる方法を列挙します。
	// 球を区別します。
	// 箱を区別しません。
	public static class GroupingHelper
	{
		#region 0 個以上

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
					if (b[i].Count == 0) break;
				}
				return false;
			}
		}

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

		public static void Assign1(int n, int k, Func<List<int>[], bool> action)
		{
			var b = Array.ConvertAll(new bool[k], _ => new List<int>());
			DFS(0, 0);

			// i0: 最初の空の箱の番号
			bool DFS(int v, int i0)
			{
				if (n - v < k - i0) return false;
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

		public static void Assign1ForBalls(int n, int k, Func<int[], bool> action)
		{
			var p = new int[n];
			DFS(0, 0);

			// i0: 最初の空の箱の番号
			bool DFS(int v, int i0)
			{
				if (n - v < k - i0) return false;
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
	}
}
