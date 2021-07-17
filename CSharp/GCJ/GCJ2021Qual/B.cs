using System;
using System.Linq;

class B
{
	const int max = 1 << 30;
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select((_, i) => $"Case #{i + 1}: {Solve()}")));
	static object Solve()
	{
		var h = Console.ReadLine().Split();

		var x = int.Parse(h[0]);
		var y = int.Parse(h[1]);
		var s = h[2];
		var n = s.Length;

		var dp = NewArray2(n, 2, max);
		if (s[0] != 'J') dp[0][0] = 0;
		if (s[0] != 'C') dp[0][1] = 0;

		for (int i = 1; i < n; i++)
		{
			if (dp[i - 1][0] < max)
			{
				if (s[i] != 'J') dp[i][0] = Math.Min(dp[i][0], dp[i - 1][0]);
				if (s[i] != 'C') dp[i][1] = Math.Min(dp[i][1], dp[i - 1][0] + x);
			}
			if (dp[i - 1][1] < max)
			{
				if (s[i] != 'J') dp[i][0] = Math.Min(dp[i][0], dp[i - 1][1] + y);
				if (s[i] != 'C') dp[i][1] = Math.Min(dp[i][1], dp[i - 1][1]);
			}
		}
		return dp.Last().Min();
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default(T)) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
