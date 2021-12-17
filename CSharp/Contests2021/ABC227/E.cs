using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		var k = int.Parse(Console.ReadLine());

		var A = s.Count(c => c == 'K');
		var B = s.Count(c => c == 'E');
		var C = s.Count(c => c == 'Y');

		var sMap = new Dictionary<(int, int, int), string>();
		sMap[(0, 0, 0)] = s;

		for (int a = 0; a <= A; a++)
		{
			for (int b = 0; b <= B; b++)
			{
				for (int c = 0; c <= C; c++)
				{
					var v = (a, b, c);
					if (v == (0, 0, 0)) continue;

					var pv = a > 0 ? (a - 1, b, c) : b > 0 ? (a, b - 1, c) : (a, b, c - 1);
					var pc = a > 0 ? 'K' : b > 0 ? 'E' : 'Y';

					var t = sMap[pv];
					var i = t.IndexOf(pc);
					sMap[v] = t.Remove(i, 1);
				}
			}
		}

		var chars = "KEY".ToCharArray();
		var nextMap = sMap.ToDictionary(p => p.Key, p => Array.ConvertAll(chars, p.Value.IndexOf));

		var dp = sMap.ToDictionary(p => p.Key, p => new long[n * n]);
		dp[(0, 0, 0)][0] = 1;

		for (int a = 0; a <= A; a++)
		{
			for (int b = 0; b <= B; b++)
			{
				for (int c = 0; c <= C; c++)
				{
					var v = (a, b, c);
					var next = nextMap[v];
					var comb = dp[v];

					for (int i = 0; i < comb.Length; i++)
					{
						if (comb[i] == 0) continue;

						if (next[0] != -1) dp[(a + 1, b, c)][i + next[0]] += comb[i];
						if (next[1] != -1) dp[(a, b + 1, c)][i + next[1]] += comb[i];
						if (next[2] != -1) dp[(a, b, c + 1)][i + next[2]] += comb[i];
					}
				}
			}
		}

		return dp[(A, B, C)].Take(k + 1).Sum();
	}
}
