using System;
using System.Linq;

class Q060
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var l1 = LisMax(a);
		Array.Reverse(a);
		var l2 = LisMax(a);
		Array.Reverse(l2);

		return Enumerable.Range(0, n).Max(i => l1[i] + l2[i] - 1);
	}

	// index i で最大となるときの個数
	static int[] LisMax(int[] a)
	{
		var n = a.Length;
		var r = new int[n];

		// 部分列の i 番目となりうる最小値 (0-indexed) (LIS)。
		var l = Array.ConvertAll(new bool[n + 1], _ => int.MaxValue);

		for (int i = 0; i < n; ++i)
		{
			var li = Min(0, n, x => l[x] >= a[i]);
			l[li] = a[i];
			r[i] = li + 1;
		}
		return r;
	}

	static int Min(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
