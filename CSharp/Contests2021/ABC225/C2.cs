using System;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var b = Array.ConvertAll(new bool[n], _ => Read());

		var bl = b[0][0] % 7;
		var br = b[0][^1] % 7;
		if (bl == 0) bl = 7;
		if (br == 0) br = 7;
		if (bl > br) return false;

		for (int i = 0; i < n; i++)
			for (int j = 0; j < m; j++)
				if (b[i][j] - b[0][0] != 7 * i + j)
					return false;
		return true;
	}
}
