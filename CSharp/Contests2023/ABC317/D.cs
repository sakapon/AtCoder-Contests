using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		var sum = ps.Sum(p => p.Item3);
		var dp = new long[sum + 1];
		Array.Fill(dp, 1L << 60);
		dp[0] = 0;

		foreach (var (x, y, z) in ps)
		{
			var d = x > y ? 0 : (y - x + 1) / 2;

			for (int j = sum; j >= 0; j--)
			{
				var nj = j + z;
				if (nj > sum) continue;
				Chmin(ref dp[nj], dp[j] + d);
			}
		}

		return dp[((sum + 1) / 2)..].Min();
	}

	public static long Chmin(ref long x, long v) => x > v ? x = v : x;
}
