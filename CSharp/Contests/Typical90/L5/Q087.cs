using System;
using System.Linq;

class Q087
{
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, p, k) = Read3();
		var a = Array.ConvertAll(new bool[n], _ => ReadL());

		var l = First(1, max, x => Count(x) <= k);
		var r = First(1, max, x => Count(x) < k);

		if (l == max) return 0;
		if (r == max) return "Infinity";
		return r - l;

		int Count(int x)
		{
			var d = Array.ConvertAll(a, b => b.ToArray());
			for (int i = 0; i < n; i++)
				for (int j = 0; j < n; j++)
					if (d[i][j] == -1) d[i][j] = x;
			WarshallFloyd(n, d);

			var c = 0;
			for (int i = 0; i < n; i++)
				for (int j = i + 1; j < n; j++)
					if (d[i][j] <= p) c++;
			return c;
		}
	}

	static bool WarshallFloyd(int n, long[][] cs)
	{
		for (int k = 0; k < n; ++k)
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < n; ++j)
				{
					if (cs[i][k] == long.MaxValue || cs[k][j] == long.MaxValue) continue;
					var nc = cs[i][k] + cs[k][j];
					if (cs[i][j] <= nc) continue;
					cs[i][j] = nc;
				}
		for (int i = 0; i < n; ++i) if (cs[i][i] < 0) return false;
		return true;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
