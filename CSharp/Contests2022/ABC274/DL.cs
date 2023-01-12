using System;
using System.Collections.Generic;
using System.Linq;

class DL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, x, y) = Read3();
		var a = Read();

		return Check(a[0], a.Where((v, i) => i % 2 == 0).Skip(1).ToArray(), x)
			&& Check(0, a.Where((v, i) => i % 2 == 1).ToArray(), y);
	}

	const int size = 10000;
	static bool Check(int a0, int[] a, int z)
	{
		var n = a.Length;
		var dp = OffsetArray<bool>.Create(-size, size);
		var dt = OffsetArray<bool>.Create(-size, size);
		dp[a0] = true;

		for (int i = 0; i < n; i++)
		{
			for (int j = -size; j < size; j++)
			{
				if (!dp[j]) continue;
				dt[j + a[i]] = true;
				dt[j - a[i]] = true;
			}

			(dp, dt) = (dt, dp);
			dt.Clear();
		}
		return dp[z];
	}
}
