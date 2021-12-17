using System;
using System.Collections.Generic;
using System.Linq;

class M2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var d = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());
		var sum = d.Values.Sum(c => c * (c - 1) / 2);

		void Add(int x, int count)
		{
			if (count == 0) return;
			var c = d.GetValueOrDefault(x);
			sum -= c * (c - 1) / 2;
			c += count;
			sum += c * (c - 1) / 2;
			d[x] = c;
		}

		var ruq = new STR<(int l, int r)>(n, (x, y) => x.l == -1 ? y : x, (-1, -1));

		for (var (l, r) = (0, 1); r <= n; r++)
		{
			if (r == n || a[r] != a[r - 1])
			{
				ruq.Set(l, r, (l, r));
				l = r;
			}
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var (L, R, X) = q;
			L--;

			var (Ll, Lr) = ruq.Get(L);
			var (Rl, Rr) = ruq.Get(R - 1);
			var Lx = a[Ll];
			var Rx = a[Rl];

			// 関連する区間を全て除きます。
			for (var (l, r) = (Ll, Lr); ; (l, r) = ruq.Get(r))
			{
				Add(a[l], -(r - l));
				if (r == Rr) break;
			}

			// 新たに 3 つの区間を追加します。
			Add(Lx, L - Ll);
			Add(X, R - L);
			Add(Rx, Rr - R);

			a[L] = X;
			if (R < Rr) a[R] = Rx;

			ruq.Set(Ll, L, (Ll, L));
			ruq.Set(L, R, (L, R));
			if (R < Rr) ruq.Set(R, Rr, (R, Rr));

			Console.WriteLine(sum);
		}
		Console.Out.Flush();
	}
}
