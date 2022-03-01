using System;

class C2
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		for (int i = 0; i < n; i++)
			for (int j = 5; j < n; j++)
				if (Has4(i, j, 0, -1)) return true;

		for (int i = 5; i < n; i++)
			for (int j = 0; j < n; j++)
				if (Has4(i, j, -1, 0)) return true;

		for (int i = 5; i < n; i++)
			for (int j = 5; j < n; j++)
				if (Has4(i, j, -1, -1)) return true;

		for (int i = 0; i <= n - 6; i++)
			for (int j = 5; j < n; j++)
				if (Has4(i, j, 1, -1)) return true;

		return false;

		bool Has4(int i, int j, int di, int dj)
		{
			var c = 0;
			for (int k = 0; k < 6; k++, i += di, j += dj)
				if (s[i][j] == '#') c++;
			return c >= 4;
		}
	}
}
