using System;
using System.Collections.Generic;
using System.Linq;

class B2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = ReadL();
		int n = (int)h[0], k = (int)h[1];
		long l = h[2], r = h[3];
		var a = ReadL();

		var n1 = n / 2;
		var n2 = n - n1;

		var gs1 = BruteForceHelper.CreateAllSumsForCount(a.Take(n1).ToArray());
		var gs2 = BruteForceHelper.CreateAllSumsForCount(a.Skip(n1).ToArray());

		var c = 0L;
		for (int i1 = 0; i1 <= n1; i1++)
		{
			var i2 = k - i1;
			if (i2 < 0 || n2 < i2) continue;

			var s1 = gs1[i1];
			var s2 = gs2[i2];
			Array.Sort(s2);

			foreach (var x1 in s1)
			{
				var li = First(0, s2.Length, si => x1 + s2[si] >= l);
				var ri = First(0, s2.Length, si => x1 + s2[si] > r);
				c += ri - li;
			}
		}
		Console.WriteLine(c);
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
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

				// BitOperations.PopCount に変更してください。
				//ls[BitOperations.PopCount((uint)nx)].Add(nv);
				ls[PopCount((ulong)nx)].Add(nv);
			}

		var r = Array.ConvertAll(ls, l => l.ToArray());
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

					// BitOperations.PopCount に変更してください。
					//ls[BitOperations.PopCount((uint)x)].Add(nv);
					ls[PopCount((ulong)x)].Add(nv);
					break;
				}

		var r = Array.ConvertAll(ls, l => l.ToArray());
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

		var r = Array.ConvertAll(ls, l => l.ToArray());
		return r;
	}

	const ulong m1 = 0x5555555555555555;
	const ulong m2 = 0x3333333333333333;
	const ulong m4 = 0x0F0F0F0F0F0F0F0F;
	const ulong m8 = 0x00FF00FF00FF00FF;
	const ulong m16 = 0x0000FFFF0000FFFF;
	const ulong m32 = 0x00000000FFFFFFFF;

	public static int PopCount(ulong x)
	{
		x = (x & m1) + ((x >> 1) & m1);
		x = (x & m2) + ((x >> 2) & m2);
		x = (x & m4) + ((x >> 4) & m4);
		x = (x & m8) + ((x >> 8) & m8);
		x = (x & m16) + ((x >> 16) & m16);
		x = (x & m32) + ((x >> 32) & m32);
		return (int)x;
	}
}
