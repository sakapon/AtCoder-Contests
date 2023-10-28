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
		var bs = new int[3][];

		for (int i = 0; i < b.Length; i++)
		{
			bs[0] = b[i];

			for (int j = 0; j < b.Length; j++)
			{
				bs[1] = b[j];
				if (HasOverlap(bs[0], bs[1])) continue;

				for (int k = 0; k < b.Length; k++)
				{
					bs[2] = b[k];
					if (HasOverlap(bs[0], bs[2])) continue;
					if (HasOverlap(bs[1], bs[2])) continue;

					var r2 = new (int c, int m)[n];
					var c2 = new (int c, int m)[n];
					Array.Fill(r2, ('.', n));
					Array.Fill(c2, ('.', n));

					for (int t = 0; t < 3; t++)
					{
						for (int ti = 0; ti < n; ti++)
						{
							if (r2[ti].m > bs[t][ti]) r2[ti] = (t, bs[t][ti]);
							if (c2[bs[t][ti]].m > ti) c2[bs[t][ti]] = (t, ti);
						}
					}

					if (new string(r2.Select(p => (char)('A' + p.c)).ToArray()) == r && new string(c2.Select(p => (char)('A' + p.c)).ToArray()) == c)
					{
						var cs = NewArray2(n, n, '.');
						for (int t = 0; t < 3; t++)
						{
							for (int ti = 0; ti < n; ti++)
							{
								cs[ti][bs[t][ti]] = (char)('A' + t);
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
