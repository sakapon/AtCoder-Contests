using System;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, p) = Read2();

		// j 本の辺を取り除く方法の数
		// dp0: 連結
		// dp1: 非連結
		var dp0 = new long[n + 2];
		var dp1 = new long[n + 2];
		var t0 = new long[n + 2];
		var t1 = new long[n + 2];
		dp0[0] = 1;
		dp1[1] = 1;

		for (int i = 1; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				t0[j] += dp0[j] + dp1[j];
				t0[j + 1] += dp0[j] * 3;
				t1[j + 1] += dp1[j];
				t1[j + 2] += dp0[j] * 2;

				t0[j] %= p;
				t1[j] %= p;
			}

			(dp0, t0) = (t0, dp0);
			(dp1, t1) = (t1, dp1);

			Array.Clear(t0, 0, t0.Length);
			Array.Clear(t1, 0, t1.Length);
		}

		return string.Join(" ", dp0[1..n]);
	}
}
