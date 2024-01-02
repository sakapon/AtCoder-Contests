using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var r = Console.ReadLine();
		var c = Console.ReadLine();

		var rn = Enumerable.Range(0, n).ToArray();
		var r3 = new[] { 0, 1, 2 };
		const string ABC = "ABC";

		var l = new List<int[]>();

		var p = new int[n];
		for (int i = 0; i < n; ++i) p[i] = i;
		do
		{
			l.Add((int[])p.Clone());
		}
		while (NextPermutation(p));

		// 同じ文字を書き込むパターン
		var b = l.ToArray();

		var b_inv = Array.ConvertAll(b, p =>
		{
			var q = new int[n];
			for (int i = 0; i < n; i++) q[p[i]] = i;
			return q;
		});

		var bs = new int[3][];
		var bs_inv = new int[3][];

		for (int i = 0; i < b.Length; i++)
		{
			bs[0] = b[i];
			bs_inv[0] = b_inv[i];

			for (int j = 0; j < b.Length; j++)
			{
				bs[1] = b[j];
				bs_inv[1] = b_inv[j];
				if (HasOverlap(bs[0], bs[1])) continue;

				for (int k = 0; k < b.Length; k++)
				{
					bs[2] = b[k];
					bs_inv[2] = b_inv[k];
					if (HasOverlap(bs[0], bs[2])) continue;
					if (HasOverlap(bs[1], bs[2])) continue;

					var r2 = Array.ConvertAll(rn, ni => ABC[r3.MinBy(si => bs[si][ni], 1 << 30).arg]);
					var c2 = Array.ConvertAll(rn, ni => ABC[r3.MinBy(si => bs_inv[si][ni], 1 << 30).arg]);

					if (new string(r2) == r && new string(c2) == c)
					{
						var cs = NewArray2(n, n, '.');
						for (int si = 0; si < 3; si++)
						{
							for (int ni = 0; ni < n; ni++)
							{
								cs[ni][bs[si][ni]] = ABC[si];
							}
						}
						return "Yes\n" + string.Join("\n", cs.Select(c => new string(c)));
					}
				}
			}
		}

		return "No";

		bool HasOverlap(int[] x, int[] y)
		{
			for (int i = 0; i < n; i++)
			{
				if (x[i] == y[i]) return true;
			}
			return false;
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	public static bool NextPermutation(int[] p)
	{
		var n = p.Length;

		// p[i] < p[i + 1] を満たす最大の i
		var i = n - 2;
		while (i >= 0 && p[i] >= p[i + 1]) --i;
		if (i < 0) return false;

		// p[i] < p[j] を満たす最大の j
		var j = i + 1;
		while (j + 1 < n && p[i] < p[j + 1]) ++j;

		(p[i], p[j]) = (p[j], p[i]);
		Array.Reverse(p, i + 1, n - i - 1);
		return true;
	}
}

public static class Enumerable2
{
	public static (T arg, TKey min) MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> toKey, TKey ik, IComparer<TKey> c = null)
	{
		c ??= Comparer<TKey>.Default;
		T r = default;
		foreach (var v in source)
		{
			var k = toKey(v);
			if (c.Compare(ik, k) > 0) (r, ik) = (v, k);
		}
		return (r, ik);
	}
}
