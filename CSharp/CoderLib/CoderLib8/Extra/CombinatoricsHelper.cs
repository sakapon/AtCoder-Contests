using System;
using System.Collections.Generic;

namespace CoderLib8.Extra
{
	public static class CombinatoricsHelper
	{
		// Test: https://atcoder.jp/contests/abc310/tasks/abc310_d
		// 区別する n 個の球を、区別しない r 個の箱に入れる
		public static void Partition(int n, int r, Action<List<int>[]> action)
		{
			var p = Array.ConvertAll(new bool[r], _ => new List<int>());
			DFS(0);

			void DFS(int v)
			{
				var t = v + r - n;
				if (t >= 0 && p[t].Count == 0)
				{
					p[t].Add(v);
					if (v == n - 1) action(p);
					else DFS(v + 1);
					p[t].RemoveAt(p[t].Count - 1);
					return;
				}

				var end = false;
				for (int i = 0; !end && i < r; i++)
				{
					if (p[i].Count == 0) end = true;
					p[i].Add(v);
					if (v == n - 1) action(p);
					else DFS(v + 1);
					p[i].RemoveAt(p[i].Count - 1);
				}
			}
		}

		// 区別する n 個の球を、区別しない r 個の箱に入れる
		// 集合を bit で表現
		public static void Partition(int n, int r, Action<int[]> action)
		{
			var p = new int[r];
			DFS(0);

			void DFS(int v)
			{
				var f = 1 << v;

				var t = v + r - n;
				if (t >= 0 && p[t] == 0)
				{
					p[t] |= f;
					if (v == n - 1) action(p);
					else DFS(v + 1);
					p[t] &= ~f;
					return;
				}

				var end = false;
				for (int i = 0; !end && i < r; i++)
				{
					if (p[i] == 0) end = true;
					p[i] |= f;
					if (v == n - 1) action(p);
					else DFS(v + 1);
					p[i] &= ~f;
				}
			}
		}

		// 2 つの箱から区別できるボールを 1 つずつ取り出す方法の数
		public static long Choose2Boxes(long[] a)
		{
			var (s0, s1) = (0L, 0L);
			for (int i = 1; i < a.Length; i++)
			{
				s1 += a[i - 1];
				s0 += a[i] * s1;
			}
			return s0;
		}

		// Test: https://atcoder.jp/contests/abc312/tasks/abc312_g
		// 3 つの箱から区別できるボールを 1 つずつ取り出す方法の数
		public static long Choose3Boxes(long[] a)
		{
			var (s0, s1, s2) = (0L, 0L, 0L);
			for (int i = 2; i < a.Length; i++)
			{
				s2 += a[i - 2];
				s1 += a[i - 1] * s2;
				s0 += a[i] * s1;
			}
			return s0;
		}
	}
}
