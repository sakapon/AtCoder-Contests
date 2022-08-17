using System;

class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "black" : "white");
	static bool Solve()
	{
		var (r, c) = Read2();
		return Math.Max(Math.Abs(r - 8), Math.Abs(c - 8)) % 2 == 1;
	}
}
