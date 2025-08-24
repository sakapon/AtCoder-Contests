using System.Numerics;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[m], _ => Read3());

		// 数列 a の先頭が集合 s
		var dp = new long[1 << n];
		dp[0] = 1;

		for (uint s = 0; s < 1U << n; s++)
		{
			var c = BitOperations.PopCount(s) + 1;

			for (int i = 0; i < n; i++)
			{
				var ns = s | (1U << i);
				if (ns == s) continue;

				var ok = true;
				foreach (var (x, y, z) in ps)
				{
					if (c > x) continue;
					if (BitOperations.PopCount(ns & ((1U << y) - 1)) > z) { ok = false; break; }
				}

				if (!ok) continue;
				dp[ns] += dp[s];
			}
		}
		return dp[^1];
	}
}
