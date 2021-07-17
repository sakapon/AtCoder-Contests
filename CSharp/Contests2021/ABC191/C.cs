using System;
using System.Collections.Generic;
using System.Linq;

static class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var c1 = Enumerable.Range(0, h - 1).Sum(i => Enumerable.Range(0, w).GroupCountsBySeq(j => s[i][j] != s[i + 1][j]).Count(g => g.Key));
		var c2 = Enumerable.Range(0, w - 1).Sum(j => Enumerable.Range(0, h).GroupCountsBySeq(i => s[i][j] != s[i][j + 1]).Count(g => g.Key));
		Console.WriteLine(c1 + c2);
	}

	static IEnumerable<KeyValuePair<TK, int>> GroupCountsBySeq<TS, TK>(this IEnumerable<TS> source, Func<TS, TK> toKey)
	{
		var c = EqualityComparer<TK>.Default;
		TK k = default(TK), kt;
		var count = 0;

		foreach (var o in source)
		{
			if (!c.Equals(k, kt = toKey(o)))
			{
				if (count > 0) yield return new KeyValuePair<TK, int>(k, count);
				k = kt;
				count = 0;
			}
			++count;
		}
		if (count > 0) yield return new KeyValuePair<TK, int>(k, count);
	}
}
