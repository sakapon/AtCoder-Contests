using System;
using System.Linq;

namespace CoderLib6.Strings
{
	static class SuffixArray
	{
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

		// orthodox implementation
		[Obsolete]
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
	}
}
