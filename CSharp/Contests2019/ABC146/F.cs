using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], m = h[1];
		var s = Console.ReadLine();

		var safe = new int[n + 1];
		var t = 0;
		for (int i = 1; i <= n; i++) safe[i] = s[i] == '0' ? t = i : t;

		var dp = new int[n + 1];
		for (int i = n - 1; i >= 0; i--)
		{
			if (s[i] == '1') { dp[i] = -1; continue; }
			var j = Math.Min(n, i + m);
			if (safe[j] == i) { Console.WriteLine(-1); return; }
			dp[i] = dp[safe[j]] + 1;
		}

		var l = new List<int>();
		var u = dp[0];
		for (int i = 0; i <= n; i++)
		{
			if (dp[i] == u)
			{
				l.Add(i);
				u--;
			}
		}
		Console.WriteLine(string.Join(" ", Enumerable.Range(0, l.Count - 1).Select(i => l[i + 1] - l[i])));
	}
}
