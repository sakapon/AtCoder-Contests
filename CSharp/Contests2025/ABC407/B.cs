class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (x, y) = Read2();

		var c = 0;
		for (int i = 1; i <= 6; i++)
			for (int j = 1; j <= 6; j++)
				if (i + j >= x || Math.Abs(i - j) >= y) c++;
		return c / 36.0;
	}
}
