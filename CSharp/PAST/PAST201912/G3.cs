using System;
using System.Linq;

class G3
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n - 1].Select((_, i) => new int[i + 1].Concat(Console.ReadLine().Split().Select(int.Parse)).ToArray()).ToArray();
		var p2 = Enumerable.Range(0, n + 1).Select(i => 1 << i).ToArray();

		var dp = new int[3, p2[n]];
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				for (int f = 0; f < p2[n]; f++)
					if ((f & p2[i]) > 0 && (f & p2[j]) > 0)
						dp[0, f] += a[i][j];

		for (int i = 0, i2 = 1; i < 2; i++, i2++)
			for (int f = 0; f < p2[n]; f++)
			{
				dp[i2, f] = dp[0, f];
				for (int g = f; g > 0; g = f & (g - 1))
					dp[i2, f] = Math.Max(dp[i2, f], dp[i, f - g] + dp[0, g]);
			}
		Console.WriteLine(dp[2, p2[n] - 1]);
	}
}
