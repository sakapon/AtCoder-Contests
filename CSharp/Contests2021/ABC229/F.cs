using System;
using System.Linq;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var b = ReadL();

		var m = Math.Max(GetMax(false), GetMax(true));
		return a.Sum() + b.Sum() - m;

		long GetMax(bool same01)
		{
			var dp0 = new long[n + 1];
			var dp1 = new long[n + 1];

			if (same01)
			{
				dp1[1] = long.MinValue;
			}
			else
			{
				dp0[1] = long.MinValue;
				dp1[1] = a[0];
			}

			for (int i = 2; i <= n; i++)
			{
				dp0[i] = Math.Max(dp0[i - 1], dp1[i - 1] + b[i - 2]);
				dp1[i] = Math.Max(dp0[i - 1] + b[i - 2], dp1[i - 1]);
				dp1[i] += a[i - 1];
			}

			if (same01)
			{
				dp1[n] += b[^1];
			}
			else
			{
				dp0[n] += b[^1];
			}

			return Math.Max(dp0[n], dp1[n]);
		}
	}
}
