using System;
using System.Collections.Generic;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, l) = Read2();
		var a = Read();

		return string.Join(" ", SlideMin(a, l));
	}

	static IEnumerable<int> SlideMin(int[] a, int k)
	{
		var b = new int[a.Length];
		for (int i = 0, l = 0, r = -1; i < a.Length; ++i)
		{
			while (l <= r && a[b[r]] > a[i]) --r;
			b[++r] = i;
			if (b[l] == i - k) ++l;
			if (i >= k - 1) yield return a[b[l]];
		}
	}
}
