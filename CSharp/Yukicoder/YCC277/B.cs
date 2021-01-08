using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, m, p) = Read3();
		var a = ReadL();

		var amax = a.Max();

		// p^j を含む a_i
		var pa = new long[32];
		// a_i / p^j
		var da = new long[32];

		foreach (var v in a)
		{
			var t = v;
			var j = 0;
			while (t % p == 0)
			{
				t /= p;
				j++;
			}
			pa[j] = Math.Max(pa[j], v);
			da[j] = Math.Max(da[j], t);
		}

		var dp = new long[200];
		dp[0] = 1;

		for (int i = 0; i < dp.Length - da.Length - 20; i++)
		{
			var x = dp[i];
			if (x * amax > m)
			{
				Console.WriteLine(i + 1);
				return;
			}

			for (int j = 0; j < da.Length; j++)
			{
				if (da[j] == 0) continue;
				dp[i + j + 1] = Math.Max(dp[i + j + 1], x * da[j]);
			}
		}
		Console.WriteLine(-1);
	}
}
