using System;
using System.Collections.Generic;

// Mo。TLE。
class FM
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var c = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var moq = GetMoQueries(n, qs);

		// 各色の個数
		var ccs = new int[n + 1];
		var tc = 0;
		var (tl, tr) = (1, 0);
		var tcs = new int[qc];

		void Incr(int i)
		{
			if (ccs[c[i - 1]]++ == 0) tc++;
		}
		void Decr(int i)
		{
			if (--ccs[c[i - 1]] == 0) tc--;
		}

		foreach (var qi in moq)
		{
			var (l, r) = qs[qi];

			while (tr < r) Incr(++tr);
			while (l < tl) Incr(--tl);
			while (r < tr) Decr(tr--);
			while (tl < l) Decr(tl++);

			tcs[qi] = tc;
		}
		return string.Join("\n", tcs);
	}

	// クエリ ID を訪問順に返します。
	public static int[] GetMoQueries(int n, (int l, int r)[] qs)
	{
		var qc = qs.Length;

		// ブロック数
		var bc = (int)Math.Sqrt(qc);
		// ブロック長
		var bl = n / bc + 1;

		var qi_map = Array.ConvertAll(new bool[bc], _ => new List<int>());
		var r_map = Array.ConvertAll(new bool[bc], _ => new List<int>());
		for (int qi = 0; qi < qc; qi++)
		{
			var (l, r) = qs[qi];
			l /= bl;
			qi_map[l].Add(qi);
			r_map[l].Add(r);
		}

		var path = new List<int>();
		for (int bi = 0; bi < bc; bi++)
		{
			var qis = qi_map[bi].ToArray();
			var rs = r_map[bi].ToArray();
			Array.Sort(rs, qis);
			path.AddRange(qis);
		}
		return path.ToArray();
	}
}
