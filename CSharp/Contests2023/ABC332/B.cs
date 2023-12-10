class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (k, g, m) = Read3();

		int x = 0, y = 0;
		while (k-- > 0)
		{
			if (x == g)
			{
				x = 0;
			}
			else if (y == 0)
			{
				y = m;
			}
			else
			{
				var t = Math.Min(g - x, y);
				x += t;
				y -= t;
			}
		}
		return $"{x} {y}";
	}
}
