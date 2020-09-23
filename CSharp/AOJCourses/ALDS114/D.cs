using System;
using System.Linq;

class D
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		var q = int.Parse(Console.ReadLine());
		var ps = new int[q].Select(_ => Console.ReadLine());

		var sa = SuffixArray(s);

		Func<string, bool> match = p =>
		{
			var order = First(0, n + 1, o => string.CompareOrdinal(s, sa[o], p, 0, p.Length) >= 0);
			return order <= n && string.CompareOrdinal(s, sa[order], p, 0, p.Length) == 0;
		};
		Console.WriteLine(string.Join("\n", ps.Select(p => match(p) ? 1 : 0)));
	}

	static int[] SuffixArray(string s)
	{
		var n = s.Length;

		// order -> index
		var sa = Enumerable.Range(0, n + 1).ToArray();
		// index -> order
		var rank = new int[n + 1];
		rank[n] = -1;
		for (int i = 0; i < n; i++) rank[i] = s[i];

		// rank_k(i) と rank_k(i+k) から rank_2k(i) を作ります。
		var k = 1;
		Comparison<int> c = (i, j) =>
		{
			var r = rank[i].CompareTo(rank[j]);
			if (r != 0) return r;
			return rank[Math.Min(i + k, n)].CompareTo(rank[Math.Min(j + k, n)]);
		};
		Func<int, int, bool> eq = (i, j) => rank[i] == rank[j] && rank[Math.Min(i + k, n)] == rank[Math.Min(j + k, n)];

		for (; k < n; k <<= 1)
		{
			Array.Sort(sa, c);

			// 生成する必要はないか
			var tr = (int[])rank.Clone();
			for (int i = 0; i < n; i++)
				tr[sa[i + 1]] = tr[sa[i]] + (eq(sa[i], sa[i + 1]) ? 0 : 1);
			rank = tr;
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
