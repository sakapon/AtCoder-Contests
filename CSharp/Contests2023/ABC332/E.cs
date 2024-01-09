using CoderLib8.Collections;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, d) = Read2();
		var w = ReadL();

		var dp = new SeqArray2<long>(1 << n, d + 1, (1L << 60) * 3);

		for (int x = 1; x < 1 << n; x++)
		{
			var s = ((uint)x).ToElements(n).Sum(i => w[i]);
			dp[x, 1] = s * s;

			AllSubsets(d, x, y =>
			{
				var z = x & ~y;
				for (int k = 2; k <= d; k++)
				{
					dp[x, k] = Math.Min(dp[x, k], dp[y, 1] + dp[z, k - 1]);
				}
				return false;
			});
		}

		var sum = w.Sum();
		return (double)(dp.a[^1] * d - sum * sum) / (d * d);
	}

	public static void AllSubsets(int n, int s, Func<int, bool> action)
	{
		for (int x = 0; ; x = (x - s) & s)
		{
			if (action(x)) break;
			if (x == s) break;
		}
	}
}
