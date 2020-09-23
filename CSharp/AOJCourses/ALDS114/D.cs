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
			return order <= n && sa[order] + p.Length <= n && string.CompareOrdinal(s, sa[order], p, 0, p.Length) == 0;
		};
		Console.WriteLine(string.Join("\n", ps.Select(p => match(p) ? 1 : 0)));
	}

	static int[] SuffixArray(string s)
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

		for (; k < n; k <<= 1)
		{
			Array.Sort(sa, compare);

			for (int i = 0; i < n; i++)
				tr[sa[i + 1]] = tr[sa[i]] + (equals(sa[i], sa[i + 1]) ? 0 : 1);
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
