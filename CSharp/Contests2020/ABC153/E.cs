using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var z = read();
		int h = z[0], n = z[1];
		var ms = new int[n].Select(_ => read()).ToArray();

		var dp = new int[h + 1];
		for (int i = 1; i <= h; i++)
		{
			int min = int.MaxValue, j;
			foreach (var m in ms)
			{
				if ((j = i - m[0]) < 0) j = 0;
				min = Math.Min(min, dp[j] + m[1]);
			}
			dp[i] = min;
		}
		Console.WriteLine(dp[h]);
	}
}
