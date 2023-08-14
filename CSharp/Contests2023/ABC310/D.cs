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
		Partition(n, t, p =>
		{
			foreach (var x in p)
				foreach (var f in fs)
					if ((x & f) == f) return;
			r++;
		});
		return r;
	}

	// 区別できる n 個の球を、区別しない k 個の箱に入れる
	// 集合を bit で表現
	public static void Partition(int n, int r, Action<int[]> action)
	{
		var p = new int[r];
		DFS(0);

		void DFS(int v)
		{
			var f = 1 << v;

			var t = v + r - n;
			if (t >= 0 && p[t] == 0)
			{
				p[t] |= f;
				if (v == n - 1) action(p);
				else DFS(v + 1);
				p[t] &= ~f;
				return;
			}

			var end = false;
			for (int i = 0; !end && i < r; i++)
			{
				if (p[i] == 0) end = true;
				p[i] |= f;
				if (v == n - 1) action(p);
				else DFS(v + 1);
				p[i] &= ~f;
			}
		}
	}
}
