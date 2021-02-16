using System;
using System.Linq;

class T2
{
	const long M = 1000000007;
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var dp = new long[n];
		dp[0] = 1;

		for (int i = 1; i < n; i++)
		{
			var cum = new CumSum(dp);
			var t = new long[n];
			if (s[i - 1] == '>')
				for (int j = 0; j <= i; j++)
					t[j] = cum.Sum(j, i);
			else
				for (int j = 0; j <= i; j++)
					t[j] = cum.Sum(0, j);
			dp = Array.ConvertAll(t, x => x % M);
		}
		Console.WriteLine(dp.Sum() % M);
	}
}

class CumSum
{
	long[] s;
	public CumSum(long[] a)
	{
		s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
	}
	public long Sum(int l_in, int r_ex) => s[r_ex] - s[l_in];
}
