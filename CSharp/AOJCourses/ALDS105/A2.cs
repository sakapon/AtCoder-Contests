using System;
using System.Linq;

class A2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var q = int.Parse(Console.ReadLine());
		var m = Read();

		var M = m.Max();
		var dp = new bool[M + 1];
		dp[0] = true;
		for (int i = 0; i < n; i++)
			for (int x = M; x >= 0; x--)
			{
				if (!dp[x]) continue;
				var sum = x + a[i];
				if (sum <= M) dp[sum] = true;
			}

		Console.WriteLine(string.Join("\n", m.Select(x => dp[x] ? "yes" : "no")));
	}
}
