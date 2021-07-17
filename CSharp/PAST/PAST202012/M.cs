using System;
using System.Linq;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, l) = ((int, long))Read2L();
		var a = Read();
		var s = new CumSum(a);

		var r = Last(a.Min(), l, x =>
		{
			var raq = 0;
			var lazy = new int[n + 1];
			lazy[0]++;
			lazy[1]--;

			for (int i = 0; i <= n; i++)
			{
				raq += lazy[i];
				if (i == n) return raq > 0;
				if (raq <= 0) continue;

				var left = First(i + 1, n + 1, j => s.s[j] >= s.s[i] + x);
				var right = Last(i, n, j => s.s[j] <= s.s[i] + l);

				if (left <= n && n <= right) return true;
				if (left <= n) lazy[left]++;
				if (right + 1 <= n) lazy[right + 1]--;
			}
			return false;
		});
		Console.WriteLine(r);
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}

class CumSum
{
	public long[] s;
	public CumSum(int[] a)
	{
		s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
	}
	public long Sum(int l_in, int r_ex) => s[r_ex] - s[l_in];
}
