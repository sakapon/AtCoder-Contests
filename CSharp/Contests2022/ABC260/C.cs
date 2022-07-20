using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x, y) = Read3();

		var (r, b) = (1L, 0L);

		while (n-- > 1)
		{
			b += r * x;

			r += b;
			b *= y;
		}
		return b;
	}
}
