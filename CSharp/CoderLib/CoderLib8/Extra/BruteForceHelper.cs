using System;
using System.Collections.Generic;

namespace CoderLib8.Extra
{
	public static class BruteForceHelper
	{
		// Test: https://atcoder.jp/contests/abc184/tasks/abc184_f
		// O(2^n)
		public static long[] CreateAllSums(int[] a)
		{
			var n = a.Length;
			var r = new long[1 << n];
			for (int i = 0, pi = 1; i < n; ++i, pi <<= 1)
				for (int x = 0; x < pi; ++x)
					r[x | pi] = r[x] + a[i];
			return r;
		}

		// この bit DP では、配るよりも貰うほうが速いです。
		public static long[] CreateAllSums_BitDP(int[] a)
		{
			var n = a.Length;
			var dp = new long[1 << n];
			for (int x = 1; x < 1 << n; ++x)
				for (int i = 0; i < n; ++i)
					if ((x & (1 << i)) != 0)
					{
						dp[x] = dp[x ^ (1 << i)] + a[i];
						break;
					}
			return dp;
		}

		public static long[] CreateAllSums_List(int[] a)
		{
			var r = new List<long> { 0 };
			for (int i = 0; i < a.Length; ++i)
				for (int j = r.Count - 1; j >= 0; --j)
					r.Add(r[j] + a[i]);
			return r.ToArray();
		}

		// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/7/DPL/4/DPL_4_B
		// Test: https://atcoder.jp/contests/typical90/tasks/typical90_ay
		// O(2^n)
		public static long[][] CreateAllSumsForCount(int[] a)
		{
			var n = a.Length;
			var dp = new long[1 << n];
			var ls = Array.ConvertAll(new bool[n + 1], _ => new List<long>());
			ls[0].Add(0);

			for (int i = 0, pi = 1; i < n; ++i, pi <<= 1)
				for (int x = 0; x < pi; ++x)
				{
					var nx = x | pi;
					var nv = dp[x] + a[i];
					dp[nx] = nv;

					// BitOperations.PopCount に変更してください。
					//ls[BitOperations.PopCount((uint)nx)].Add(nv);
					ls[PopCount(nx)].Add(nv);
				}

			// この関数ではソートします。
			var r = Array.ConvertAll(ls, l => l.ToArray());
			foreach (var g in r) Array.Sort(g);
			return r;
		}

		// この bit DP では、配るよりも貰うほうが速いです。
		public static long[][] CreateAllSumsForCount_BitDP(int[] a)
		{
			var n = a.Length;
			var dp = new long[1 << n];
			var ls = Array.ConvertAll(new bool[n + 1], _ => new List<long>());
			ls[0].Add(0);

			for (int x = 1; x < 1 << n; ++x)
				for (int i = 0; i < n; ++i)
					if ((x & (1 << i)) != 0)
					{
						var nv = dp[x ^ (1 << i)] + a[i];
						dp[x] = nv;

						// BitOperations.PopCount に変更してください。
						//ls[BitOperations.PopCount((uint)x)].Add(nv);
						ls[PopCount(x)].Add(nv);
						break;
					}

			// この関数ではソートします。
			var r = Array.ConvertAll(ls, l => l.ToArray());
			foreach (var g in r) Array.Sort(g);
			return r;
		}

		public static long[][] CreateAllSumsForCount_List(int[] a)
		{
			var n = a.Length;
			var ls = Array.ConvertAll(new bool[n + 1], _ => new List<long>());
			ls[0].Add(0);

			for (int i = 0; i < n; ++i)
				for (int j = i; j >= 0; --j)
				{
					var nl = ls[j + 1];
					foreach (var v in ls[j])
						nl.Add(v + a[i]);
				}

			// この関数ではソートします。
			var r = Array.ConvertAll(ls, l => l.ToArray());
			foreach (var g in r) Array.Sort(g);
			return r;
		}

		// Obsolete
		static int PopCount(int x)
		{
			var r = 0;
			for (; x != 0; x >>= 1) if ((x & 1) != 0) ++r;
			return r;
		}
	}
}
