using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read3());

		var map = NewArray2(n, n, -1);
		foreach (var (a, b, c) in es)
		{
			map[a - 1][b - 1] = c;
			map[b - 1][a - 1] = c;
		}

		var r = 0;
		var u = new bool[n];

		for (int sv = 0; sv < n; sv++)
		{
			DFS(sv, 0);
		}
		return r;

		void DFS(int v, int d)
		{
			Chmax(ref r, d);
			u[v] = true;

			for (int nv = 0; nv < n; nv++)
			{
				if (u[nv]) continue;
				if (map[v][nv] == -1) continue;

				DFS(nv, d + map[v][nv]);
			}
			u[v] = false;
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
	public static int Chmax(ref int x, int v) => x < v ? x = v : x;
}
