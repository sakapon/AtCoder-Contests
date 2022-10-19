using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, d) = Read3L();
		var a = ReadL();

		Array.Sort(a);
		var un = a.GroupBy(x => x % d).Sum(g => Unavailable(g.Key, g.ToArray())) % M;
		var all = GetAll();
		return (all - un + M) % M;

		long GetAll()
		{
			var all = 0L;
			if (d <= 1 << 20)
			{
				// 余りごとに加算
				for (int rem = 0; rem < d; rem++)
				{
					// 範囲内の個数
					var k = (n + d - rem) / d;
					if (rem == 0) k--;
					all += Sum1(k);
				}
			}
			else
			{
				// 選んだ個数ごとに加算
				var max = n / d + 1;
				for (int c = 1; c <= max; c++)
				{
					// 占有する幅
					var w = (c - 1) * d + 1;
					if (n < w) continue;
					all += n - w + 1;
				}
			}
			return all % M;
		}

		long Unavailable(long rem, long[] a)
		{
			// 範囲内の個数
			var k = (n + d - rem) / d;
			if (rem == 0) k--;
			var all = Sum1(k);

			for (int i = 0; i < a.Length; i++)
			{
				a[i] = (a[i] - 1) / d;
			}

			a = a.Prepend(-1).Append(k).ToArray();
			for (int i = 1; i < a.Length; i++)
			{
				var x = a[i] - a[i - 1] - 1;
				all -= Sum1(x);
				all = (all + M) % M;
			}
			return all % M;
		}

		static long Sum1(long k)
		{
			k %= M;
			return k * (k + 1) % M * MHalf % M;
		}
	}

	const long M = 1000000007;
	const long MHalf = (M + 1) / 2;
}
