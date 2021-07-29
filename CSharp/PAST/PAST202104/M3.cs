using System;
using System.Collections.Generic;
using System.Linq;

class M3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());

		var d = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());
		var sum = d.Values.Sum(c => c * (c - 1) / 2);

		void Add(int x, int count)
		{
			var c = d.GetValueOrDefault(x);
			sum -= c * (c - 1) / 2;
			c += count;
			sum += c * (c - 1) / 2;
			d[x] = c;
		}

		// WA
		var lset = new SortedSet<int>();
		var rset = new SortedSet<int>();

		var ls = new int[n + 1];
		var rs = new int[n];

		for (var (l, r) = (0, 1); r <= n; r++)
		{
			if (r == n || a[r] != a[r - 1])
			{
				lset.Add(l);
				rset.Add(r);
				ls[r] = l;
				rs[l] = r;
				l = r;
			}
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var (L, R, X) = Read3();
			L--;

			var ll = ls[rset.GetViewBetween(L + 1, R).Min];
			var lrange = lset.GetViewBetween(ll, R - 1).ToArray();
			var rl = lrange[^1];
			var rr = rs[rl];
			var lx = a[ll];
			var rx = a[rl];

			// 関連する区間を全て除きます。
			foreach (var l in lrange)
			{
				Add(a[l], -(rs[l] - l));

				lset.Remove(l);
				rset.Remove(rs[l]);
			}

			Add(lx, L - ll);
			Add(X, R - L);
			Add(rx, rr - R);

			a[L] = X;
			if (R < rr) a[R] = rx;

			if (ll < L) lset.Add(ll);
			lset.Add(L);
			if (R < rr) lset.Add(R);

			if (ll < L) rset.Add(L);
			rset.Add(R);
			if (R < rr) rset.Add(rr);

			rs[ll] = L;
			rs[L] = R;
			if (R < rr) rs[R] = rr;

			ls[rr] = R;
			ls[R] = L;
			if (ll < L) ls[L] = ll;

			Console.WriteLine(sum);
		}
		Console.Out.Flush();
	}
}
