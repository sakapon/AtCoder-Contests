using System;
using System.Linq;

class T
{
	const long M = 1000000007;
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var dp = new long[n];
		dp[0] = 1;

		for (int i = 0; i < n - 1; i++)
		{
			var raq = new StaticRAQ(n);
			if (s[i] == '>')
				for (int j = 0; j <= i; j++)
					raq.Add(0, j + 1, dp[j]);
			else
				for (int j = 0; j <= i; j++)
					raq.Add(j + 1, i + 2, dp[j]);
			dp = Array.ConvertAll(raq.GetAll(), x => x % M);
		}
		Console.WriteLine(dp.Sum() % M);
	}
}

class StaticRAQ
{
	long[] d;
	public StaticRAQ(int n) { d = new long[n]; }

	// O(1)
	// 範囲外のインデックスも可。
	public void Add(int l_in, int r_ex, long v)
	{
		d[Math.Max(0, l_in)] += v;
		if (r_ex < d.Length) d[r_ex] -= v;
	}

	// O(n)
	public long[] GetAll()
	{
		var a = new long[d.Length];
		a[0] = d[0];
		for (int i = 1; i < d.Length; ++i) a[i] = a[i - 1] + d[i];
		return a;
	}
}
