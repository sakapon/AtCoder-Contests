﻿using System;
using System.Collections.Generic;

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

				for (int i = 0; i < k; i++)
				{
					b[i].Add(v);
					if (DFS(v + 1)) return true;
					b[i].RemoveAt(b[i].Count - 1);
				}
				return false;
			}
		}

		public static void AssignForBalls0(int n, int k, Func<int[], bool> action)
		{
			var p = new int[n];
			DFS(0);

			bool DFS(int v)
			{
				if (v == n) return action(p);

				for (int i = 0; i < k; i++)
				{
					p[v] = i;
					if (DFS(v + 1)) return true;
				}
				return false;
			}
		}

		#endregion

		#region 1 個以上

		public static void Assign1(int n, int k, Func<List<int>[], bool> action)
		{
			var b = Array.ConvertAll(new bool[k], _ => new List<int>());
			var c0 = k;
			DFS(0);

			bool DFS(int v)
			{
				if (n - v < c0) return false;
				if (v == n) return action(b);

				for (int i = 0; i < k; i++)
				{
					if (b[i].Count == 0) --c0;
					b[i].Add(v);
					if (DFS(v + 1)) return true;
					b[i].RemoveAt(b[i].Count - 1);
					if (b[i].Count == 0) ++c0;
				}
				return false;
			}
		}

		#endregion
	}
}
