using System;
using System.Collections.Generic;
using System.Linq;

class D3
{
	static void Main()
	{
		long M = 1000000007;
		var s = Console.ReadLine();
		var n = s.Length;
		var q = int.Parse(Console.ReadLine());
		var ps = new int[q].Select(_ => Console.ReadLine());

		var sa = ManberMyers(s);
		var rh = new RH(s, M);

		Func<int, string, RH, int> compare = (i, p, prh) =>
		{
			var preCount = Last(0, Math.Min(n - i, p.Length), x => rh.Hash(i, x) == prh.Hash(0, x));
			if (i + preCount == n ^ preCount == p.Length) return preCount == p.Length ? 1 : -1;
			if (preCount == p.Length) return 0;
			return s[i + preCount] - p[preCount];
		};

		Func<string, bool> match = p =>
		{
			var prh = new RH(p, M);
			var order = First(0, n + 1, o => compare(sa[o], p, prh) >= 0);
			return order <= n && sa[order] + p.Length <= n && rh.Hash(sa[order], p.Length) == prh.Hash(0, p.Length);
		};
		Console.WriteLine(string.Join("\n", ps.Select(p => match(p) ? 1 : 0)));
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

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
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
	string s;
	int n;
	long p;
	long[] pow, pre;

	public RH(string _s, long _p)
	{
		s = _s;
		n = s.Length;
		p = _p;

		pow = new long[n + 1];
		pow[0] = 1;
		pre = new long[n + 1];

		for (int i = 0; i < n; ++i)
		{
			pow[i + 1] = pow[i] * p;
			pre[i + 1] = pre[i] * p + s[i];
		}
	}

	public long Hash(int start, int count) => pre[start + count] - pre[start] * pow[count];

	public static long Hash(string s, long p) => Hash(s, 0, s.Length, p);
	public static long Hash(string s, int start, int count, long p)
	{
		var h = 0L;
		for (int i = 0; i < count; ++i) h = h * p + s[start + i];
		return h;
	}
}
