using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var s = Console.ReadLine();
		var t = Console.ReadLine();
		var k = int.Parse(Console.ReadLine());

		var mc = new MCombinationMod2(400000);

		while (k-- > 0)
		{
			var (x, y) = Read2();
			x--;
			y--;

			if (x == 0)
			{
				Console.WriteLine(t[y]);
				continue;
			}
			if (y == 0)
			{
				Console.WriteLine(s[x]);
				continue;
			}

			var r = 0L;
			for (int i = 1; i <= x; i++)
			{
				if (s[i] == '1')
				{
					r += mc.MNcr(x + y - 1 - i, y - 1);
				}
			}
			for (int j = 1; j <= y; j++)
			{
				if (t[j] == '1')
				{
					r += mc.MNcr(x + y - 1 - j, x - 1);
				}
			}
			Console.WriteLine(r % 2);
		}
	}
}

public class MCombinationMod2
{
	// 階乗に含まれる素因数 2 の数
	static long[] MFactorials(int n)
	{
		var f = new long[n + 1];
		for (int p = 2; p <= n; p <<= 1)
			for (int i = p; i <= n; i += p) ++f[i];
		for (int i = 1; i <= n; ++i) f[i] += f[i - 1];
		return f;
	}

	// nCr を O(1) で求めるため、階乗を O(n) で求めておきます。
	readonly long[] f;
	public MCombinationMod2(int nMax)
	{
		f = MFactorials(nMax);
	}

	// (n & r) == r でも可能
	public long MNcr(int n, int r) => f[n] == f[n - r] + f[r] ? 1 : 0;
}
