using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int a, int b, int c) Read3() { var a = Read(); return (a[0] - 1, a[1] - 1, a[2] - 1); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read3());

		var M = 0;

		Power(new[] { false, true }, n, p =>
		{
			foreach (var (a, b, c) in es)
			{
				if (p[a] && p[b] && p[c]) return;
			}

			var u = new bool[n];
			foreach (var (a, b, c) in es)
			{
				if (p[a] && p[b]) u[c] = true;
				if (p[a] && p[c]) u[b] = true;
				if (p[b] && p[c]) u[a] = true;
			}

			M = Math.Max(M, u.Count(x => x));
		});
		Console.WriteLine(M);
	}

	static void Power<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2);
				else action(p);
			}
		}
	}
}
