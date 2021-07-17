using System;
using System.Collections;
using System.Linq;

class Q057
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Array.ConvertAll(new bool[n], _ => { Console.ReadLine(); return Read(); });
		var s = new BitArray(Array.ConvertAll(Read(), x => x == 1));

		var b = Array.ConvertAll(new bool[n], _ => new BitArray(m));
		for (int i = 0; i < n; i++)
		{
			foreach (var j in a[i])
			{
				b[i][j - 1] = true;
			}
		}

		void SwapColumns(int x, int y)
		{
			for (int i = 0; i < n; i++)
				(b[i][x], b[i][y]) = (b[i][y], b[i][x]);
			(s[x], s[y]) = (s[y], s[x]);
		}

		var rn = Enumerable.Range(0, n).ToArray();
		var rm = Enumerable.Range(0, m).ToArray();
		var nm = Math.Min(n, m);
		var rj = m;

		for (int j = 0; j < nm; j++)
		{
			if (n < m && rn.Skip(j).All(i => !b[i][j]))
			{
				while (j < --rj)
				{
					if (!rn.Skip(j).All(i => !b[i][rj]))
					{
						SwapColumns(j, rj);
						break;
					}
				}
			}

			for (int i = n - 1; i >= 0; i--)
			{
				if (i == j || !b[i][j]) continue;

				if (b[j][j])
				{
					b[i].Xor(b[j]);
				}
				else if (i > j)
				{
					(b[i], b[j]) = (b[j], b[i]);
				}
			}
		}

		for (int i = 0; i < nm; i++)
		{
			if (s[i])
			{
				s.Xor(b[i]);
			}
		}

		if (rm.Any(j => s[j])) return 0;

		var c = b.Count(ba => rm.All(j => !ba[j]));
		return MPow(2, c);
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
}
