using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, t, m) = Read3();
		var ps = Array.ConvertAll(new bool[m], _ => Read());

		var fs = Array.ConvertAll(ps, a => (1 << a[0] - 1) | (1 << a[1] - 1));
		var r = 0;
		Assign1(n, t, p =>
		{
			foreach (var x in p)
				foreach (var f in fs)
					if ((x & f) == f) return false;
			r++;
			return false;
		});
		return r;
	}

	// 区別する n 個の球を、区別しない k 個の箱に入れる
	// 集合を bit で表現
	public static void Assign1(int n, int k, Func<int[], bool> action)
	{
		if (n < k) return;
		var b = new int[k];
		DFS(0, 0);

		// i0: 最初の空の箱の番号
		bool DFS(int v, int i0)
		{
			if (v == n) return action(b);

			for (int i = k - i0 < n - v ? 0 : i0; i < k; ++i)
			{
				b[i] |= 1 << v;
				if (DFS(v + 1, i < i0 ? i0 : i0 + 1)) return true;
				b[i] &= ~(1 << v);
				if (i == i0) break;
			}
			return false;
		}
	}
}
