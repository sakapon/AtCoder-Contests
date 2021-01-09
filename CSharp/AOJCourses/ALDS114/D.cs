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

		var sa = ManberMyers(s);

		Func<string, bool> match = p =>
		{
			var order = First(0, n + 1, o => string.CompareOrdinal(s, sa[o], p, 0, p.Length) >= 0);
			return order <= n && sa[order] + p.Length <= n && string.CompareOrdinal(s, sa[order], p, 0, p.Length) == 0;
		};
		Console.WriteLine(string.Join("\n", ps.Select(p => match(p) ? 1 : 0)));
	}

	static int[] ManberMyers(string s)
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
		if (s.All(c => c == s[0])) return sa;

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
		return sa;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
