using System;
using System.Linq;

class F
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var n = h[0];
		var map = new int[h[1]].Select(_ => read()).ToLookup(r => r[0], r => r[1]).ToDictionary(g => g.Key, g => g.ToArray());

		var m = double.MaxValue;
		for (int k = 1; k < n; k++)
		{
			var dp = new double[n + 1];
			for (int i = n - 1; i > 0; i--)
			{
				if (i == k && map[i].Length > 1)
				{
					var sum = 0.0;
					var max = 0.0;
					foreach (var j in map[i])
					{
						sum += dp[j];
						max = Math.Max(max, dp[j]);
					}
					dp[i] = (sum - max) / (map[i].Length - 1) + 1;
				}
				else
				{
					var sum = 0.0;
					foreach (var j in map[i])
					{
						sum += dp[j];
					}
					dp[i] = sum / map[i].Length + 1;
				}
			}
			m = Math.Min(m, dp[1]);
		}
		Console.WriteLine(m);
	}
}
