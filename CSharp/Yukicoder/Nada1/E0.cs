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
		var m = 2;
		while (Try(n, ++m)) ;
		return m - 1;
	}

	static readonly bool[][] ok = NewArray2<bool>(9, 9);
	static bool Try(int n, int m)
	{
		if (n > m) return ok[m][n];

		var c = n * m;
		var a = NewArray2<bool>(n, m);

		for (long x = 0; x < 1L << c; x++)
		{
			for (int i = 0; i < n; i++)
				for (int j = 0; j < m; j++)
					a[i][j] = (x & (1 << i * m + j)) != 0;

			if (Check())
			{
				Console.WriteLine($"{n} {m}: OK");
				return ok[n][m] = true;
			}
		}
		return false;

		bool Check()
		{
			for (int x1 = 0; x1 < n; x1++)
				for (int x2 = x1 + 1; x2 < n; x2++)
					for (int y1 = 0; y1 < m; y1++)
						for (int y2 = y1 + 1; y2 < m; y2++)
						{
							var f = a[x1][y1];
							if (a[x1][y2] == f && a[x2][y1] == f && a[x2][y2] == f) return false;
						}
			return true;
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
