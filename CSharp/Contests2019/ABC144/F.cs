using System;
using System.Linq;

class F
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var n = h[0];
		var map = new int[h[1]].Select(_ => read()).ToLookup(r => r[0], r => r[1]);

		var dp = new double[n + 1];
		for (int i = n - 1; i > 0; i--)
		{
			var es = map[i].Select(x => dp[x]).ToArray();
			dp[i] = (es.Length == 1 ? es[0] : (es.Sum() - es.Max()) / (es.Length - 1)) + 1;
		}
		Console.WriteLine(dp[1]);
	}
}
