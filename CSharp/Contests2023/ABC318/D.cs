using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n - 1], _ => Read());

		var rn = Enumerable.Range(0, n).ToArray();
		var map = new long[n, n];
		for (int i = 0; i < n - 1; i++)
			for (int j = 0; j < n - 1 - i; j++)
				map[i, j + i + 1] = ps[i][j];

		var u = new bool[n];
		var r = 0L;
		var t = 0L;
		if (n % 2 == 0)
		{
			DFS(0);
		}
		else
		{
			for (int v = 0; v < n; v++)
			{
				u[v] = true;
				DFS(0);
				u[v] = false;
			}
		}
		return r;

		void DFS(int i)
		{
			if (i == n / 2)
			{
				Chmax(ref r, t);
				return;
			}

			var v = rn.First(x => !u[x]);
			u[v] = true;
			for (int nv = v + 1; nv < n; nv++)
			{
				if (u[nv]) continue;
				u[nv] = true;
				t += map[v, nv];
				DFS(i + 1);
				t -= map[v, nv];
				u[nv] = false;
			}
			u[v] = false;
		}
	}

	public static long Chmax(ref long x, long v) => x < v ? x = v : x;
}
