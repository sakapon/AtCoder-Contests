using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var dp = new int[2 * n + 2];
		for (int i = 1; i <= n; i++)
		{
			var j = a[i - 1];
			dp[2 * i] = dp[j] + 1;
			dp[2 * i + 1] = dp[j] + 1;
		}
		return string.Join("\n", dp[1..]);
	}
}
