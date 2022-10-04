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
		var (n, m) = Read2();
		var a = ReadL();

		var rs = new IntCumRangeSum(a);
		var t = Enumerable.Range(1, m).Sum(i => i * a[i - 1]);
		var r = t;

		for (int i = m; i < n; i++)
		{
			t += m * a[i];
			t -= rs[i - m, i];
			r = Math.Max(r, t);
		}
		return r;
	}
}

public class IntCumRangeSum
{
	readonly int n;
	readonly long[] s;

	public IntCumRangeSum(long[] counts)
	{
		n = counts.Length;
		s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + counts[i];
	}

	public int ItemsCount => n;
	public long Sum => s[n];
	public long[] CumSum => s;

	public long this[int i] => s[i + 1] - s[i];
	public long this[int l, int r] => s[r] - s[l];
}
