using System;
using System.Linq;

class C2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var qs = new int[h[2]].Select(_ => Read()).ToArray();

		var r = 0;
		var rn = Enumerable.Range(0, n + m - 1).ToArray();

		Combination(rn, n, p =>
		{
			var a = p.Select((i, j) => i - j).ToArray();
			r = Math.Max(r, qs.Where(q => a[q[1] - 1] - a[q[0] - 1] == q[2]).Sum(q => q[3]));
		});
		Console.WriteLine(r);
	}

	static void Combination<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0, 0);
		else action(p);

		void Dfs(int i, int j0)
		{
			var i2 = i + 1;
			for (int j = j0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2, j + 1);
				else action(p);
			}
		}
	}
}
