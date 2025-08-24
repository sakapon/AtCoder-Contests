using CoderLib8.Values;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();

		var g = new Grid2<char>(h, w, '.');

		var (i, j) = (0, 0);
		var (di, dj) = (-1, 0);

		for (int k = 0; k < n; k++)
		{
			if (g[i, j] == '.')
			{
				g[i, j] = '#';
				(di, dj) = (dj, -di);
			}
			else
			{
				g[i, j] = '.';
				(di, dj) = (-dj, di);

			}
			i = (i + di + h) % h;
			j = (j + dj + w) % w;
		}

		return string.Join("\n", g.Select(r => string.Join("", r)));
	}
}
