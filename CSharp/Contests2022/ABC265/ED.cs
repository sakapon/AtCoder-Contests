using System;
using System.Collections.Generic;
using System.Linq;

class ED
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();
		var set = Array.ConvertAll(new bool[m], _ => Read2L()).ToHashSet();

		var dp = new HashMap<(long, long), long>();
		var dt = new HashMap<(long, long), long>();
		dp[(0, 0)] = 1;

		long nx, ny;

		for (int i = 0; i < n; i++)
		{
			foreach (var (p, v) in dp)
			{
				var (x, y) = p;

				if (!set.Contains((nx, ny) = (x + a[0], y + a[1])))
				{
					dt[(nx, ny)] += v;
					dt[(nx, ny)] %= M;
				}
				if (!set.Contains((nx, ny) = (x + a[2], y + a[3])))
				{
					dt[(nx, ny)] += v;
					dt[(nx, ny)] %= M;
				}
				if (!set.Contains((nx, ny) = (x + a[4], y + a[5])))
				{
					dt[(nx, ny)] += v;
					dt[(nx, ny)] %= M;
				}
			}

			(dp, dt) = (dt, dp);
			dt.Clear();
		}

		return dp.Values.Sum() % M;
	}

	const long M = 998244353;
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
