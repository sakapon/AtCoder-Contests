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

		var dp = new double[n + 1, n];
		for (int i = n - 1; i > 0; i--)
			for (int j = 0; j < n; j++)
			{
				var es = map[i].Select(x => dp[x, j]).ToArray();
				dp[i, j] = i == j && es.Length > 1 ?
					(es.Sum() - es.Max()) / (es.Length - 1) + 1 :
					es.Sum() / es.Length + 1;
			}
		Console.WriteLine(Enumerable.Range(1, n - 1).Min(j => dp[1, j]));
	}
}
