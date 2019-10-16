using System;
using System.Linq;

class K
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var a = read();
		var k = h[1];

		var dp = new bool[k + 1];
		for (int i = 1; i <= k; i++)
			dp[i] = a.Any(x => x <= i && !dp[i - x]);
		Console.WriteLine(dp[k] ? "First" : "Second");
	}
}
