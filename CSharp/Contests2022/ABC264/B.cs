using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "black" : "white");
	static bool Solve()
	{
		var (r, c) = Read2();

		var n = 15;
		var u = new bool[n, n];

		for (int k = 0; k <= n / 2; k++)
		{
			var v = k % 2 == 0;

			for (int i = k; i < n - k; i++)
			{
				for (int j = k; j < n - k; j++)
				{
					u[i, j] = v;
				}
			}
		}
		return u[r - 1, c - 1];
	}
}
