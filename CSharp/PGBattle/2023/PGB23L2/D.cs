using System;
using System.Collections.Generic;
using T3 = System.ValueTuple<int, int, int>;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();

		// WA
		var p3 = new long[] { 1, 3, 6, 6 };

		static long nCr(int n, int r)
		{
			if (r == 0) return 1;
			if (r == 1) return n;
			if (r == 2) return n * (n - 1) / 2;
			return n * (n - 1) * (n - 2) / 6;
		}

		var dp = new HashMap<T3, long>();
		var dt = new HashMap<T3, long>();
		dp[(0, 0, n)] = 1;

		for (int t = 0; t < n; t++)
		{
			foreach (var (p, v) in dp)
			{
				for (int i = 0; i <= 3; i++)
				{
					if (i > p.Item1) break;
					for (int j = 0; j <= 3 - i; j++)
					{
						if (j > p.Item2) break;
						var k = 3 - i - j;
						if (k > p.Item3) continue;

						dt[(p.Item1 - i + j, p.Item2 - j + k, p.Item3 - k)] += v * nCr(p.Item1, i) % m * nCr(p.Item2, j) % m * p3[i + j];
						dt[(p.Item1 - i + j, p.Item2 - j + k, p.Item3 - k)] %= m;
					}
				}
			}

			(dp, dt) = (dt, dp);
			dt.Clear();
		}
		return dp[(0, 0, 0)];
	}
}

class HashMap<TK, TV> : Dictionary<TK, TV>
{
	TV _v0;
	public HashMap(TV v0 = default(TV)) { _v0 = v0; }

	public new TV this[TK key]
	{
		get { return ContainsKey(key) ? base[key] : _v0; }
		set { base[key] = value; }
	}
}
