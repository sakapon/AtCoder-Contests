using System;
using System.Collections.Generic;
using System.Linq;

class O
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var abs = Array.ConvertAll(new bool[n], _ => Read2());

		var a = Array.ConvertAll(abs, p => p.a);
		var b = Array.ConvertAll(abs, p => p.b);
		var s = CumSumL(a);

		// B_i を購入するまでのコストの最小値
		var dp = new long[n + 1];
		// 購入前の所持金 (単調増加)
		var q = new long[n + 1];

		for (int i = 0; i < n; i++)
		{
			q[i + 1] = Math.Max(q[i], s[i + 1] - dp[i]);

			if (i > 0 && b[i - 1] > b[i])
			{
				b[i] = b[i - 1];
			}

			var j = First(0, i + 2, x => q[x] >= b[i]) - 1;
			if (j > i) return -1;
			dp[i + 1] = dp[j] + b[i];
		}

		return s[n] - dp[n];
	}

	public static long[] CumSumL(int[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
