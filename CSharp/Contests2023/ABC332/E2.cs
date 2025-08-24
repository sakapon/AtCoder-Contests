using CoderLib8.Collections;

class E2
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
		}

		for (int x = 1; x < 1 << n; x++)
		{
			AllSupersets(n, x, z =>
			{
				var y = z & ~x;
				for (int k = 1; k < d; k++)
				{
					var nv = dp[x, k] + dp[y, 1];
					if (dp[z, k + 1] > nv) dp[z, k + 1] = nv;
				}
				return false;
			});
		}

		var sum = w.Sum();
		return (double)(dp.a[^1] * d - sum * sum) / (d * d);
	}

	public static void AllSupersets(int n, int s, Func<int, bool> action)
	{
		for (int x = s; x < 1 << n; x = (x + 1) | s)
		{
			if (action(x)) break;
		}
	}
}
