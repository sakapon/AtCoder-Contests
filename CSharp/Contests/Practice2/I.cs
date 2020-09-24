using System;
using System.Linq;

class I
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var (sa, lcp) = ManberMyers_Lcp(s);

		// 前の文字列と一致した部分より後ろの文字数を加算します。
		var r = 0L;
		for (int i = 0; i < n; i++)
			r += n - sa[i + 1] - lcp[i];
		Console.WriteLine(r);
	}

	static (int[], int[]) ManberMyers_Lcp(string s)
	{
		var n = s.Length;

		// order -> index
		var sa = new int[n + 1];
		// index -> order
		var rank = new int[n + 1];
		for (int i = 0; i < n; ++i)
		{
			sa[i] = n - i;
			rank[i] = s[i];
		}
		if (s.All(c => c == s[0])) return (sa, Enumerable.Range(0, n).ToArray());

		// Empty のランクを 0 とします。
		// rank_k(i) と rank_k(i+k) から rank_2k(i) を作ります。
		var k = 1;
		Converter<int, long> toKey = i => rank[i] * 100000000L + rank[Math.Min(i + k, n)];
		var keys = Array.ConvertAll(sa, toKey);

		// k == 1
		{
			Array.Sort(keys, sa);

			for (int i = 1; i <= n; ++i)
				rank[sa[i]] = keys[i] == keys[i - 1] ? rank[sa[i - 1]] : i;
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
				for (int i = start; i <= j; ++i)
					keys[i] = toKey(sa[i]);
				Array.Sort(keys, sa, start, count);

				// rank の一部が早く更新されるため、while の回数が少なくなる可能性があります。
				for (int i = start + 1; i <= j; ++i)
				{
					var eq = keys[i] == keys[i - 1];
					rank[sa[i]] = eq ? rank[sa[i - 1]] : i;
					if (eq) next = true;
				}

				j = start;
			}
		}

		// order -> count
		var lcp = new int[n];
		for (int i = 0; i < n; ++i)
		{
			var o = rank[i] - 1;
			for (int j = sa[o] + lcp[o], i2 = i + lcp[o]; j < n && i2 < n && s[j] == s[i2]; ++j, ++i2) ++lcp[o];
			if (lcp[o] > 1) lcp[rank[i + 1] - 1] = lcp[o] - 1;
		}
		return (sa, lcp);
	}
}
