using System;
using System.Linq;

class E2
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		int n = h[0];
		var rs = new int[h[1]].Select(_ => read()).ToArray();
		var qs = new int[int.Parse(Console.ReadLine())].Select(_ => read()).ToArray();

		var d = new long[n + 1, n + 1];
		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= n; j++)
				d[i, j] = long.MaxValue;
		foreach (var r in rs)
		{
			d[r[0], r[1]] = r[2];
			d[r[1], r[0]] = r[2];
		}

		for (int k = 1; k <= n; k++)
			for (int i = 1; i <= n; i++)
				for (int j = 1; j <= n; j++)
					if (d[i, k] < long.MaxValue && d[k, j] < long.MaxValue)
						d[i, j] = Math.Min(d[i, j], d[i, k] + d[k, j]);

		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= n; j++)
				d[i, j] = d[i, j] <= h[2] ? 1 : long.MaxValue;

		for (int k = 1; k <= n; k++)
			for (int i = 1; i <= n; i++)
				for (int j = 1; j <= n; j++)
					if (d[i, k] < long.MaxValue && d[k, j] < long.MaxValue)
						d[i, j] = Math.Min(d[i, j], d[i, k] + d[k, j]);

		Console.WriteLine(string.Join("\n", qs.Select(x => d[x[0], x[1]]).Select(x => x < long.MaxValue ? x - 1 : -1)));
	}
}
