using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		// 次の休日が i 日後であるときの生産量
		var d = new long[n + 1];
		for (int i = 2; i <= n; i++)
		{
			d[i] = d[i - 1] + a[(i - 2) / 2];
		}

		// 初日は休日
		var dp = new long[n + 1];

		for (int i = 0; i < n; i++)
		{
			for (int j = 1; j <= n; j++)
			{
				if (i + j > n) break;
				ChFirstMax(ref dp[i + j], dp[i] + d[j]);
			}
		}
		return dp[n];
	}

	public static void ChFirstMax<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) < 0) o1 = o2; }
}
