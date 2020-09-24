using System;
using System.Linq;

class D2
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		var q = int.Parse(Console.ReadLine());
		var ps = new int[q].Select(_ => Console.ReadLine());

		var sa = ManberMyers0(s);

		Func<string, bool> match = p =>
		{
			var order = First(0, n + 1, o => StringCompare(s, sa[o], p, 0, p.Length) >= 0);
			return order <= n && sa[order] + p.Length <= n && StringEquals(s, sa[order], p, 0, p.Length);
		};
		Console.WriteLine(string.Join("\n", ps.Select(p => match(p) ? 1 : 0)));
	}

	// string.CompareOrdinal の代用です。
	// ただし、速くはないようです。
	static int StringCompare(string s1, int i1, string s2, int i2, int length)
	{
		for (int j = 0; j < length; ++j)
		{
			int j1 = i1 + j, j2 = i2 + j;
			if (j1 == s1.Length ^ j2 == s2.Length) return j1 == s1.Length ? -1 : 1;
			if (j1 == s1.Length) return 0;
			var r = s1[j1].CompareTo(s2[j2]);
			if (r != 0) return r;
		}
		return 0;
	}

	// string.CompareOrdinal (== 0) の代用です。
	static bool StringEquals(string s1, int i1, string s2, int i2, int length)
	{
		for (int j = 0; j < length; ++j)
		{
			int j1 = i1 + j, j2 = i2 + j;
			if (j1 == s1.Length ^ j2 == s2.Length) return false;
			if (j1 == s1.Length) return true;
			if (s1[j1] != s2[j2]) return false;
		}
		return true;
	}

	static int[] ManberMyers0(string s)
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
			var r = rank[i].CompareTo(rank[j]);
			if (r != 0) return r;
			return rank[Math.Min(i + k, n)].CompareTo(rank[Math.Min(j + k, n)]);
		};
		Func<int, int, bool> equals = (i, j) => rank[i] == rank[j] && rank[Math.Min(i + k, n)] == rank[Math.Min(j + k, n)];

		for (; k < n; k <<= 1)
		{
			Array.Sort(sa, compare);

			for (int i = 1; i <= n; ++i)
				tr[sa[i]] = equals(sa[i], sa[i - 1]) ? tr[sa[i - 1]] : i;
			tr.CopyTo(rank, 0);
		}
		return sa;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
