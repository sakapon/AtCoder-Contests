using System;
using System.Linq;

class Q032
{
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ => Read());
		var m = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = new bool[n, n];
		foreach (var e in es)
		{
			map[e[0] - 1, e[1] - 1] = true;
			map[e[1] - 1, e[0] - 1] = true;
		}

		var r = max;
		var rn = Enumerable.Range(0, n).ToArray();

		Permutation(rn, n, p =>
		{
			for (int i = 1; i < n; i++)
			{
				if (map[p[i - 1], p[i]]) return;
			}

			var sum = 0;
			for (int i = 0; i < n; i++)
				sum += a[p[i]][i];

			if (r > sum) r = sum;
		});

		return r == max ? -1 : r;
	}

	static void Permutation<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];
		var u = new bool[n];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				if (u[j]) continue;
				p[i] = values[j];
				u[j] = true;

				if (i2 < r) Dfs(i2);
				else action(p);

				u[j] = false;
			}
		}
	}
}
