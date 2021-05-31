using System;

class Q050L
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, l) = Read2();

		var dp = new MemoDP1<long>(n + 1, -1, (t, i) =>
		{
			var v = t[i - 1];
			if (i - l >= 0) v += t[i - l];
			return v % M;
		});

		dp[0] = 1;
		return dp[n];
	}
}
