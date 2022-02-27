using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		for (int i = 0; i < n; i++)
			for (int j = 0; j <= n - 6; j++)
				if (Has4_Yoko(i, j)) return true;

		for (int i = 0; i <= n - 6; i++)
			for (int j = 0; j < n; j++)
				if (Has4_Tate(i, j)) return true;

		for (int i = 0; i <= n - 6; i++)
			for (int j = 0; j <= n - 6; j++)
				if (Has4_Naname1(i, j)) return true;

		for (int i = 0; i <= n - 6; i++)
			for (int j = 5; j < n; j++)
				if (Has4_Naname2(i, j)) return true;

		return false;

		bool Has4_Yoko(int i, int j)
		{
			var c = 0;
			for (int k = 0; k < 6; k++)
				if (s[i][j + k] == '#') c++;
			return c >= 4;
		}

		bool Has4_Tate(int i, int j)
		{
			var c = 0;
			for (int k = 0; k < 6; k++)
				if (s[i + k][j] == '#') c++;
			return c >= 4;
		}

		bool Has4_Naname1(int i, int j)
		{
			var c = 0;
			for (int k = 0; k < 6; k++)
				if (s[i + k][j + k] == '#') c++;
			return c >= 4;
		}

		bool Has4_Naname2(int i, int j)
		{
			var c = 0;
			for (int k = 0; k < 6; k++)
				if (s[i + k][j - k] == '#') c++;
			return c >= 4;
		}
	}
}
