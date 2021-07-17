using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Q051B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k, p) = ((int, int, long))Read3L();
		var a = ReadL();

		var n1 = n / 2;
		var n2 = n - n1;

		var s1 = BruteForceHelper.CreateAllSumsForCount(a[..n1]);
		var s2 = BruteForceHelper.CreateAllSumsForCount(a[n1..]);

		foreach (var g in s2)
			Array.Reverse(g);

		var r = 0L;

		for (int i = 0; i <= k && i <= n1; i++)
		{
			var j = k - i;
			if (j < 0 || n2 < j) continue;

			var g1 = s1[i];
			var g2 = s2[j];

			r += TwoPointers(g1.Length, g2.Length, (x, y) => g1[x] + g2[y] <= p).Sum(_ => (long)g2.Length - _.j);
		}

		return r;
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}
}

public static class BruteForceHelper
{
	// O(2^n)
	public static long[][] CreateAllSumsForCount(long[] a)
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

				ls[BitOperations.PopCount((uint)nx)].Add(nv);
			}

		// この関数ではソートします。
		var r = Array.ConvertAll(ls, l => l.ToArray());
		foreach (var g in r) Array.Sort(g);
		return r;
	}

	// この bit DP では、配るよりも貰うほうが速いです。
	public static long[][] CreateAllSumsForCount_BitDP(long[] a)
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

					ls[BitOperations.PopCount((uint)x)].Add(nv);
					break;
				}

		// この関数ではソートします。
		var r = Array.ConvertAll(ls, l => l.ToArray());
		foreach (var g in r) Array.Sort(g);
		return r;
	}

	public static long[][] CreateAllSumsForCount_List(long[] a)
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
}
