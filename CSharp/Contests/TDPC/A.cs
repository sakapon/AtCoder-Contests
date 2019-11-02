using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var dp = new bool[100 * n + 1];
		dp[0] = true;
		for (int i = 0; i < n; i++)
			for (int j = 100 * i; j >= 0; j--)
				if (dp[j]) dp[j + p[i]] = true;
		Console.WriteLine(dp.Count(x => x));
	}
}
