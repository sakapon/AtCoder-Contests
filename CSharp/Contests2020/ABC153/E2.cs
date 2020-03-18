using System;
using System.Linq;

class E2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var z = Read();
		var h = z[0];
		var ms = new int[z[1]].Select(_ => Read()).ToArray();

		var dp = Enumerable.Repeat(int.MaxValue, h + 1).ToArray();
		dp[h] = 0;
		for (int j, i = h; i > 0; i--)
			if (dp[i] < int.MaxValue)
				foreach (var m in ms)
				{
					if ((j = i - m[0]) < 0) j = 0;
					dp[j] = Math.Min(dp[j], dp[i] + m[1]);
				}
		Console.WriteLine(dp[0]);
	}
}
