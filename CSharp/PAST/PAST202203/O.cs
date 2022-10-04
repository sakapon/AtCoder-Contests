using System;
using System.Collections.Generic;
using System.Linq;

class O
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMapList(n + 1, es, false);
		var u = new int[n + 1];
		var l = new List<(int, int)>();

		for (int v = 1; v <= n; v++)
		{
			if (u[v] != 0) continue;
			var (c1, c2) = (0, 0);
			var bip = true;
			DFS(v, 1);
			if (bip) l.Add((c1, c2));

			void DFS(int v, int rem)
			{
				if (u[v] != 0)
				{
					if (u[v] != rem) bip = false;
					return;
				}
				u[v] = rem;
				if (rem == 1) c1++;
				else c2++;

				foreach (var nv in map[v])
				{
					DFS(nv, rem == 1 ? 2 : 1);
				}
			}
		}

		var n1 = (n + 2) / 3;
		var n2 = (n + 1) / 3;
		var dp = new bool[n1 + 1, n2 + 1];
		dp[0, 0] = true;

		foreach (var (c1, c2) in l)
		{
			for (int i = n1; i >= 0; i--)
			{
				for (int j = n2; j >= 0; j--)
				{
					if (!dp[i, j]) continue;
					if (i + c1 <= n1 && j + c2 <= n2) dp[i + c1, j + c2] = true;
					if (i + c2 <= n1 && j + c1 <= n2) dp[i + c2, j + c1] = true;
				}
			}
		}
		return dp[n1, n2];
	}

	public static List<int>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
