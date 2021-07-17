using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var sum = a.Sum();

		var dp = new bool[sum + 1];
		dp[0] = true;

		for (int i = 0; i < n; i++)
		{
			for (int j = sum; j >= 0; j--)
			{
				if (!dp[j]) continue;
				dp[j + a[i]] = true;
			}
		}
		return Enumerable.Range(0, sum).Where(x => dp[x]).Min(x => Math.Max(x, sum - x));
	}
}
