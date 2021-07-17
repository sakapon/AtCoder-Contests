using System;
using System.Text;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (A, B, K) = ((int, int, long))Read3L();

		var ncr = GetNcr();

		var sb = new StringBuilder();
		Rec(A, B, K);
		return sb.ToString();

		void Rec(int a, int b, long k)
		{
			if (a == 0)
			{
				sb.Append('b', b);
				return;
			}
			if (b == 0)
			{
				sb.Append('a', a);
				return;
			}

			var c = ncr[a + b - 1, a - 1];
			if (k <= c)
			{
				sb.Append('a');
				Rec(a - 1, b, k);
			}
			else
			{
				sb.Append('b');
				Rec(a, b - 1, k - c);
			}
		}
	}

	static long[,] GetNcr()
	{
		var n = 66;
		var dp = new long[n + 1, n + 1];
		for (int i = 0; i <= n; i++)
		{
			dp[i, 0] = dp[i, i] = 1;
			for (int j = 1; j < i; j++)
				dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j];
		}
		return dp;
	}
}
