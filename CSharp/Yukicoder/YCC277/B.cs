using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, m, p) = Read3();
		var a = ReadL();

		var a_max = a.Max();

		// a_i / p^j
		// j + 1 回でこの値による乗算
		var da = new long[32];

		foreach (var v in a)
		{
			var t = v;
			var j = 0;
			while (t % p == 0)
			{
				t /= p;
				j++;
			}
			da[j] = Math.Max(da[j], t);
		}

		var dp = new long[1 << 10];
		dp[0] = 1;

		for (int i = 0; i < dp.Length - da.Length; i++)
		{
			var x = dp[i];
			if (x * a_max > m)
			{
				Console.WriteLine(i + 1);
				return;
			}

			for (int j = 0; j < da.Length; j++)
			{
				if (da[j] == 0) continue;
				dp[i + j + 1] = Math.Max(dp[i + j + 1], x * da[j]);
			}
		}
		Console.WriteLine(-1);
	}
}
