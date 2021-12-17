using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var b = ReadL();

		var ab = a.Zip(b, (x, y) => x + y).ToArray();
		var rsq = new StaticRSQ1(ab);

		var c = new long[2 * n];
		var t = 0L;

		for (int i = 1; i <= n; i++)
		{
			t += rsq.GetSum(0, i);
			c[i] = t;
		}

		for (int i = 1; i < n; i++)
		{
			t -= n * ab[i - 1];
			t += rsq.GetSum(i, n);
			c[n + i] = t;
		}

		return string.Join(" ", c);
	}
}

public class StaticRSQ1
{
	int n;
	long[] s;
	public long[] Raw => s;
	public StaticRSQ1(long[] a)
	{
		n = a.Length;
		s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
	}

	// [l, r)
	// 範囲外のインデックスも可。
	public long GetSum(int l, int r)
	{
		if (r < 0 || n < l) return 0;
		if (l < 0) l = 0;
		if (n < r) r = n;
		return s[r] - s[l];
	}
}
