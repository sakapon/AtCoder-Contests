using System;
using System.Collections.Generic;
using System.Linq;

class I2
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var sa = ManberMyers(s);
		var rh = new RH(s);

		var r = 0L;
		// 前の文字列と一致した部分より後ろの文字数を加算します。
		for (int i = 0; i < n; i++)
			r += n - sa[i + 1] - Last(0, Math.Min(n - sa[i], n - sa[i + 1]), x => rh.Hash(sa[i], x) == rh.Hash(sa[i + 1], x));
		Console.WriteLine(r);
	}

	static int[] ManberMyers(string s)
	{
		var n = s.Length;

		// order -> index
		var sa = Enumerable.Range(0, n + 1).ToArray();
		// index -> order
		// Empty のランクを 0 とします。
		var rank = new int[n + 1];
		var tr = new int[n + 1];
		for (int i = 0; i < n; i++) rank[i] = s[i];

		// rank_k(i) と rank_k(i+k) から rank_2k(i) を作ります。
		var k = 1;
		Comparison<int> compare = (i, j) =>
		{
			var d = rank[i] - rank[j];
			if (d != 0) return d;
			i = Math.Min(i + k, n);
			j = Math.Min(j + k, n);
			return rank[i] - rank[j];
		};
		Func<int, int, bool> equals = (i, j) => rank[i] == rank[j] && rank[Math.Min(i + k, n)] == rank[Math.Min(j + k, n)];

		var comparer = Comparer<int>.Create(compare);

		// k == 1
		{
			Array.Sort(sa, compare);

			for (int i = 1; i <= n; ++i)
				tr[sa[i]] = equals(sa[i], sa[i - 1]) ? tr[sa[i - 1]] : i;
			tr.CopyTo(rank, 0);
		}

		var next = true;
		while (next && (k <<= 1) < n)
		{
			next = false;
			for (int j = n; j >= 0; --j)
			{
				var start = rank[sa[j]];
				if (start == j) continue;
				var count = j - start + 1;

				// ソートが完了していない部分のみソートします。
				Array.Sort(sa, start, count, comparer);

				for (int i = start + 1; i <= j; ++i)
				{
					var eq = equals(sa[i], sa[i - 1]);
					tr[sa[i]] = eq ? tr[sa[i - 1]] : i;
					if (eq) next = true;
				}
				// rank の一部が早く更新されるため、while の回数が少なくなる可能性があります。
				for (int i = start + 1; i <= j; ++i)
					rank[sa[i]] = tr[sa[i]];

				j = start;
			}
		}
		return sa;
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}

class RH
{
	const long B = 10007;
	const long M = 1000000007;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;

	string s;
	int n;
	long b;
	long[] pow, pre;

	public RH(string _s, long _b = B)
	{
		s = _s;
		n = s.Length;
		b = _b;

		pow = new long[n + 1];
		pow[0] = 1;
		pre = new long[n + 1];

		for (int i = 0; i < n; ++i)
		{
			pow[i + 1] = pow[i] * b % M;
			pre[i + 1] = (pre[i] * b + s[i]) % M;
		}
	}

	public long Hash(int start, int count) => MInt(pre[start + count] - pre[start] * pow[count]);
	public long Hash2(int minIn, int maxEx) => MInt(pre[maxEx] - pre[minIn] * pow[maxEx - minIn]);

	public static long Hash(string s, long b = B) => Hash(s, 0, s.Length, b);
	public static long Hash(string s, int start, int count, long b = B)
	{
		var h = 0L;
		for (int i = 0; i < count; ++i) h = (h * b + s[start + i]) % M;
		return h;
	}
}
