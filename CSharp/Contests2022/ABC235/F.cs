using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		var m = Read()[0];
		var c = Read();

		// 上から i 桁、数字の部分集合 j
		// s[..i] 未満の個数および和
		var cdp = new long[p10];
		var sdp = new long[p10];
		var cdt = new long[p10];
		var sdt = new long[p10];

		// s[..i] に対する部分集合
		var ef = 0;
		// s[..i] の値
		var ev = 0L;

		for (int i = 0; i < n; i++)
		{
			var d = s[i] - '0';

			for (int j = 0; j < p10; j++)
			{
				for (int k = 0; k < 10; k++)
				{
					var nj = j | (1 << k);
					if ((j, k) == (0, 0)) nj = 0;

					cdt[nj] += cdp[j];
					sdt[nj] += sdp[j] * 10 + cdp[j] * k;
					cdt[nj] %= M;
					sdt[nj] %= M;
				}
			}

			for (int k = 0; k < d; k++)
			{
				var nj = ef | (1 << k);
				if ((ef, k) == (0, 0)) nj = 0;

				cdt[nj] += 1;
				sdt[nj] += ev * 10 + k;
				cdt[nj] %= M;
				sdt[nj] %= M;
			}

			ef |= 1 << d;
			ev = ev * 10 + d;
			ev %= M;

			(cdp, cdt) = (cdt, cdp);
			(sdp, sdt) = (sdt, sdp);
			Array.Clear(cdt, 0, cdt.Length);
			Array.Clear(sdt, 0, sdt.Length);
		}

		var cf = c.Sum(x => 1 << x);
		var r = sdp.Where((_, x) => (x & cf) == cf).Sum();
		if ((ef & cf) == cf) r += ev;
		return r %= M;
	}

	const int p10 = 1 << 10;
	const long M = 998244353;
}
