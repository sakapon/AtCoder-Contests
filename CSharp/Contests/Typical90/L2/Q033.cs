using System;

class Q033
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();

		if (h == 1) return w;
		if (w == 1) return h;

		h = (h + 1) / 2;
		w = (w + 1) / 2;
		return h * w;
	}
}
