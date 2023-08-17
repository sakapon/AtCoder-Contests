using System;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, t, m) = Read3();
		var ps = Array.ConvertAll(new bool[m], _ => Read2());

		var r = 0;
		Partition(n, t, p =>
		{
			foreach (var (a, b) in ps)
				if (p[a - 1] == p[b - 1]) return;
			r++;
		});
		return r;
	}

	// 区別する n 個の球を、区別しない r 個の箱に入れる
	public static void Partition(int n, int r, Action<int[]> action)
	{
		// 各球に対して、入る箱の番号
		var p = new int[n];
		DFS(0, 0);

		// e: 最初の空の箱の番号
		void DFS(int v, int e)
		{
			var i = v + r - n;
			if (i != e) i = 0;

			for (; i < r; ++i)
			{
				p[v] = i;
				if (v == n - 1) action(p);
				else DFS(v + 1, i == e ? e + 1 : e);
				if (i == e) break;
			}
		}
	}
}
