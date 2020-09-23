using System;
using System.Linq;

class I
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var (sa, d) = SuffixArray(s);

		var r = 0L;
		// 前の文字列と一致した部分より後ろの文字数を加算します。
		for (int i = 0; i < n; i++)
		{
			var t = d[i + 1];
			var j0 = sa[i] + t;
			var j1 = sa[i + 1] + t;
			while (j0 < n && j1 < n && s[j0] == s[j1]) { t++; j0++; j1++; }
			r += n - sa[i + 1] - t;
		}
		Console.WriteLine(r);
	}

	static (int[], int[]) SuffixArray(string s)
	{
		var n = s.Length;

		// order -> index
		var sa = Enumerable.Range(0, n + 1).ToArray();
		// index -> order
		var rank = new int[n + 1];
		var tr = new int[n + 1];
		tr[n] = rank[n] = -1;
		for (int i = 0; i < n; i++) rank[i] = s[i];

		// 前の文字列と等しいことが保証されている文字数
		// order -> count
		var d = new int[n + 1];

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

			for (int i = 0; i < n; i++)
			{
				// 2k 文字まで等しいかどうか
				var eq = equals(sa[i], sa[i + 1]);
				tr[sa[i + 1]] = tr[sa[i]] + (eq ? 0 : 1);
				if (eq) d[i + 1] = k << 1;
			}
			tr.CopyTo(rank, 0);
		}
		return (sa, d);
	}
}
