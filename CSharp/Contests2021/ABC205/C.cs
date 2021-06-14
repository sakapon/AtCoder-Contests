using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b, c) = Read3();

		if (c % 2 == 0)
		{
			a = Math.Abs(a);
			b = Math.Abs(b);
		}
		return a == b ? '=' : a < b ? '<' : '>';
	}
}
