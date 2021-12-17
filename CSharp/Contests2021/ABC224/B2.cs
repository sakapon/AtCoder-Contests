using System;

class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		for (int i1 = 0; i1 < h; i1++)
		{
			for (int j1 = 0; j1 < w; j1++)
			{
				for (int i2 = i1 + 1; i2 < h; i2++)
				{
					for (int j2 = j1 + 1; j2 < w; j2++)
					{
						if (a[i1][j1] + a[i2][j2] > a[i2][j1] + a[i1][j2]) return false;
					}
				}
			}
		}
		return true;
	}
}
