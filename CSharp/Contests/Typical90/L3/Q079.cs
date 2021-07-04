using System;

class Q079
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => ReadL());
		var b = Array.ConvertAll(new bool[h], _ => ReadL());

		var r = 0L;

		for (int i = 0; i < h - 1; i++)
		{
			for (int j = 0; j < w - 1; j++)
			{
				var d = b[i][j] - a[i][j];
				r += Math.Abs(d);
				a[i][j + 1] += d;
				a[i + 1][j] += d;
				a[i + 1][j + 1] += d;
			}
		}

		for (int i = 0; i < h; i++)
		{
			if (a[i][^1] != b[i][^1])
			{
				return "No";
			}
		}
		for (int j = 0; j < w; j++)
		{
			if (a[^1][j] != b[^1][j])
			{
				return "No";
			}
		}
		return "Yes\n" + r;
	}
}
