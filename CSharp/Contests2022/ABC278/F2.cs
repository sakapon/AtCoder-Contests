using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static void Main() => Console.WriteLine(Solve() ? "First" : "Second");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (i == j) continue;
				if (ss[i][^1] == ss[j][0])
				{
					map[i].Add(j);
				}
			}
		}

		// x: 使った単語の集合、i: 最後に使った単語 (これから使う場合)
		var dp = NewArray2<bool>(1 << n, n);
		Array.Fill(dp[^1], true);
		for (int x = (1 << n) - 2; x > 0; x--)
		{
			for (int i = 0; i < n; i++)
			{
				if ((x & (1 << i)) == 0) continue;

				dp[x][i] = true;
				foreach (var j in map[i])
				{
					var nx = x | (1 << j);
					if (nx != x && dp[nx][j])
					{
						dp[x][i] = false;
						break;
					}
				}
			}
		}

		return Enumerable.Range(0, n).Any(i => dp[1 << i][i]);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
