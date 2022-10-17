using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		// 縦長
		var (w, h) = Read2();

		if (h % 2 == 1 && w % 2 == 1) return 0;

		// 0: 横 開
		// 1: 横 閉
		// 2: 縦 開
		// 3: 縦 閉
		var states = new List<int[]>();
		Power(new[] { 0, 1, 2, 3 }, w, p => states.Add((int[])p.Clone()));
		var avail1 = states.Select(Avail1).ToArray();

		var p4w = states.Count;
		var map = Array.ConvertAll(avail1, _ => new List<int>());

		for (int i = 0; i < p4w; i++)
		{
			for (int j = 0; j < p4w; j++)
			{
				if (avail1[i] && avail1[j] && Avail2(states[i], states[j]))
				{
					map[i].Add(j);
				}
			}
		}

		var dp = Enumerable.Range(0, p4w).Select(i => avail1[i] && states[i].All(x => x != 3) ? 1L : 0L).ToArray();
		var dt = new long[p4w];

		for (int i = 1; i < h; i++)
		{
			for (int j = 0; j < p4w; j++)
			{
				foreach (var nj in map[j])
				{
					dt[nj] += dp[j];
					dt[nj] %= M;
				}
			}

			(dp, dt) = (dt, dp);
			Array.Clear(dt, 0, dt.Length);
		}

		return Enumerable.Range(0, p4w).Where(i => avail1[i] && states[i].All(x => x != 2)).Sum(i => dp[i]) % M;

		bool Avail1(int[] s)
		{
			for (int i = 0; i < w; i++)
			{
				if (s[i] == 0)
				{
					if (i + 1 >= w || s[i + 1] != 1) return false;
				}
				else if (s[i] == 1)
				{
					if (i - 1 < 0 || s[i - 1] != 0) return false;
				}
			}
			return true;
		}

		bool Avail2(int[] s1, int[] s2)
		{
			for (int i = 0; i < w; i++)
			{
				if (s1[i] == 2)
				{
					if (s2[i] != 3) return false;
				}
				else if (s2[i] == 3)
				{
					if (s1[i] != 2) return false;
				}
			}
			return true;
		}
	}

	public static void Power<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2);
				else action(p);
			}
		}
	}
}
