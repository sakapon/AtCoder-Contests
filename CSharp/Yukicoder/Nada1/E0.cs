using System;

class E0
{
	static void Main()
	{
		for (int n = 3, m = 1 << 30; m > 2; n++)
		{
			m = SearchForN(n);
			Console.WriteLine($"{n} {m}");
		}
	}

	static int SearchForN(int n)
	{
		var m = 3;
		while (Try(n, m)) m++;
		return m - 1;
	}

	static readonly bool[] tf = new[] { false, true };
	static bool Try(int n, int m)
	{
		var ok = false;
		Power(tf, n * m, p =>
		{
			if (ok) return;

			var a = NewArray2<bool>(n, m);
			for (int i = 0; i < n; i++)
				for (int j = 0; j < m; j++)
					a[i][j] = p[i * m + j];

			for (int x1 = 0; x1 < n; x1++)
				for (int x2 = x1 + 1; x2 < n; x2++)
					for (int y1 = 0; y1 < m; y1++)
						for (int y2 = y1 + 1; y2 < m; y2++)
						{
							var f = a[x1][y1];
							if (a[x1][y2] == f && a[x2][y1] == f && a[x2][y2] == f) return;
						}
			ok = true;
		});
		return ok;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

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
