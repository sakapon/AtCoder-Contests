using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	const long M = 1000000007;
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var map = TallyIndexes(s);

		var dp = new long[n];
		for (int k = 0; k < 26; k++)
		{
			var q = map[k];
			if (q.Count > 0)
			{
				dp[q.Peek()] = 1;
				if (q.Peek() == 0) q.Dequeue();
			}
		}

		for (int i = 0; i < n - 1; i++)
		{
			for (int k = 0; k < 26; k++)
			{
				var q = map[k];
				if (q.Count == 0) continue;
				if (q.Peek() == i + 1) q.Dequeue();
				if (q.Count == 0) continue;

				dp[q.Peek()] += dp[i];
				dp[q.Peek()] %= M;
			}
		}

		return dp.Sum() % M;
	}

	static Queue<int>[] TallyIndexes(string s)
	{
		var d = Array.ConvertAll(new bool[26], _ => new Queue<int>());
		for (int i = 0; i < s.Length; ++i) d[s[i] - 'a'].Enqueue(i);
		return d;
	}
}
