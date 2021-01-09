using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		const int c = 300;
		const long M = 1000000007;

		var lazy = NewArray2<long>(c + 1, c);
		var dp = NewArray1<long>(n + 1);
		dp[1] = 1;

		for (int i = 1; i <= n; i++)
		{
			for (int x = 1; x <= c; x++)
			{
				dp[i] += lazy[x][i % x];
			}
			dp[i] %= M;
			if (i == n) continue;

			var v = dp[i];
			var ai = a[i - 1];

			if (ai > 1)
			{
				dp[i + 1] += v;
				dp[i + 1] %= M;
			}

			if (ai <= c)
			{
				lazy[ai][i % ai] += v;
				lazy[ai][i % ai] %= M;
			}
			else
			{
				for (int j = i + ai; j <= n; j += ai)
				{
					dp[j] += v;
					dp[j] %= M;
				}
			}
		}
		Console.WriteLine(dp[n]);
	}

	static T[] NewArray1<T>(int n, T v = default) => Array.ConvertAll(new bool[n], _ => v);
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
