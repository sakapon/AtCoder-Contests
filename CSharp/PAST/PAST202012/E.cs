using System;
using System.Collections.Generic;
using Bang.Graphs.Spp;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var s = GraphConsole.ReadGrid(h);
		var t = GraphConsole.ReadGrid(h);

		GridHelper.EncloseGrid(ref h, ref w, ref s, delta: 10);

		if (Matches()) return true;
		t = GridHelper.RotateLeft(t);
		if (Matches()) return true;
		t = GridHelper.RotateLeft(t);
		if (Matches()) return true;
		t = GridHelper.RotateLeft(t);
		if (Matches()) return true;

		return false;

		bool Matches()
		{
			var (th, tw) = (t.Length, t[0].Length);
			for (int di = 0; di < h - th + 1; di++)
				for (int dj = 0; dj < w - tw + 1; dj++)
				{
					if (Matches2(di, dj)) return true;
				}
			return false;
		}

		bool Matches2(int di, int dj)
		{
			var (th, tw) = (t.Length, t[0].Length);
			for (int i = 0; i < th; i++)
				for (int j = 0; j < tw; j++)
				{
					if (t[i][j] == '#' && s[i + di][j + dj] == '#')
						return false;
				}
			return true;
		}
	}
}
