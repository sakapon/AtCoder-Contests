using System;
using System.Linq;

class N
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var s = new long[n, n];
		for (int l = 0; l < n; l++)
			for (int r = l; r < n; r++)
				s[l, r] = (r > 0 ? s[l, r - 1] : 0) + a[r];
		var dp = new long[n, n];
		for (int i = 1; i < n; i++)
			for (int l = 0; l + i < n; l++)
				dp[l, l + i] = s[l, l + i] + Enumerable.Range(1, i).Min(j => dp[l, l + j - 1] + dp[l + j, l + i]);
		Console.WriteLine(dp[0, n - 1]);
	}
}
