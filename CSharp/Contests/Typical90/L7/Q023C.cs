using System;
using System.Collections.Generic;
using System.Linq;

class Q023C
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		h++;
		c = c.Append(new string('.', w)).ToArray();

		var rows = GetRows();
		var n = rows.Length;

		var nexts = Array.ConvertAll(new bool[n], _ => new List<int>());
		nexts[0].Add(0);
		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				if (AreAdjacentRows(rows[i], rows[j]))
				{
					nexts[i].Add(j);
					nexts[j].Add(i);
				}
			}
		}

		var dp = new long[n];
		dp[0] = 1;
		for (int i = 0; i < h; i++)
			dp = NextDP(dp, c[i]);
		return dp[0];

		int[] GetRows()
		{
			var r = new List<int>();
			for (int x = 0; x < 1 << w; x++)
				if (IsRow(x)) r.Add(x);
			return r.ToArray();
		}

		// 行の状態が実現可能かどうか
		bool IsRow(int x)
		{
			for (int i = 1; i < w; ++i)
				if ((x & (1 << i)) != 0 && (x & (1 << (i - 1))) != 0) return false;
			return true;
		}

		// 行の状態がマップ上で実現可能かどうか
		bool IsRowForMap(int x, string s)
		{
			for (int i = 0; i < w; ++i)
				if ((x & (1 << i)) != 0 && s[i] == '#') return false;
			return true;
		}

		bool AreAdjacentRows(int x, int y)
		{
			for (int i = 0; i < w; ++i)
				if ((x & (1 << i)) != 0 && (y & (1 << i)) != 0) return false;

			for (int i = 1; i < w; ++i)
			{
				if ((x & (1 << i)) != 0 && (y & (1 << (i - 1))) != 0) return false;
				if ((y & (1 << i)) != 0 && (x & (1 << (i - 1))) != 0) return false;
			}
			return true;
		}

		long[] NextDP(long[] dp, string s)
		{
			var t = new long[n];
			for (int i = 0; i < n; i++)
			{
				foreach (var j in nexts[i])
				{
					if (IsRowForMap(rows[j], s))
					{
						t[j] += dp[i];
					}
				}
			}

			for (int i = 0; i < n; i++)
				t[i] %= M;
			return t;
		}
	}
}
