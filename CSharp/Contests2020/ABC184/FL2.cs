using System;
using System.Collections.Generic;
using System.Linq;

class FL2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, t) = Read2();
		var a = Read();

		var n2 = n / 2;

		var s1 = BruteForceHelper.CreateAllSums(a[..n2]);
		var s2 = BruteForceHelper.CreateAllSums(a[n2..]);

		Array.Sort(s1);
		Array.Sort(s2);
		Array.Reverse(s2);

		Console.WriteLine(TwoPointers(s1, s2, (x, y) => x + y <= t).Max(_ => _.v1 + _.v2));
	}

	static IEnumerable<(T1 v1, T2 v2)> TwoPointers<T1, T2>(T1[] a1, T2[] a2, Func<T1, T2, bool> predicate)
	{
		for (int i = 0, j = 0; i < a1.Length && j < a2.Length; ++i)
			for (; j < a2.Length; ++j)
				if (predicate(a1[i], a2[j])) { yield return (a1[i], a2[j]); break; }
	}
}

public static class BruteForceHelper
{
	public static long[] CreateAllSums(int[] a)
	{
		var n = a.Length;
		var r = new long[1 << n];
		for (int i = 0, pi = 1; i < n; ++i, pi <<= 1)
			for (int x = 0; x < pi; ++x)
				r[pi | x] = r[x] + a[i];
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
}
