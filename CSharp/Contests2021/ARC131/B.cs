using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine().ToCharArray());

		const string chars = "12345";
		var u = new bool[128];

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (c[i][j] != '.') continue;

				Array.Clear(u, 0, 128);
				if (i > 0) u[c[i - 1][j]] = true;
				if (i + 1 < h && c[i + 1][j] != '.') u[c[i + 1][j]] = true;
				if (j > 0) u[c[i][j - 1]] = true;
				if (j + 1 < w && c[i][j + 1] != '.') u[c[i][j + 1]] = true;

				foreach (var d in chars)
				{
					if (u[d]) continue;
					c[i][j] = d;
					break;
				}
			}
		}

		return string.Join("\n", c.Select(r => new string(r)));
	}
}
