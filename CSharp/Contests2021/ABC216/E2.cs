using System;
using System.Linq;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = ReadL();

		// 満足度が x 以上となるアトラクションの回数
		long Count(int x) => a.Sum(v => v >= x ? v - x + 1 : 0);

		// k 回以内となる最小の a の値
		var minA = First(1, int.MaxValue, x => Count(x) <= k);

		var r = a.Sum(v => v >= minA ? (v + minA) * (v - minA + 1) / 2 : 0);
		r += (minA - 1) * (k - Count(minA));
		return r;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
