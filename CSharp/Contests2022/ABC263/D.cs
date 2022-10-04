using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, l, r) = Read3();
		var a = ReadL();

		// 置換による効果
		var dl = new long[n + 1];
		var dr = new long[n + 1];

		for (int i = 0; i < n; i++)
		{
			dl[i + 1] = dl[i] + l - a[i];
		}
		for (int i = n - 1; i >= 0; i--)
		{
			dr[i] = dr[i + 1] + r - a[i];
		}

		var minr = new long[n + 1];
		for (int i = n - 1; i >= 0; i--)
		{
			minr[i] = Math.Min(minr[i + 1], dr[i]);
		}

		var min = Enumerable.Range(0, n + 1).Min(i => dl[i] + minr[i]);
		return a.Sum() + min;
	}
}
