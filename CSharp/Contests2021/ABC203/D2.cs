using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = Array.ConvertAll(new bool[n], _ => Read());

		return Last(0, a.Max(r => r.Max()), m =>
		{
			var raq = new StaticRAQ2(n, n);

			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					raq.Add(i, j, i + k, j + k, a[i][j] >= m ? 1 : -1);
				}
			}

			var all = raq.GetAll0();

			for (int i = k - 1; i < n; i++)
			{
				for (int j = k - 1; j < n; j++)
				{
					if (all[i, j] <= 0) return false;
				}
			}
			return true;
		});
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
