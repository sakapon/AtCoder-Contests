using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (s, t, x) = Read3();

		if (s > t) t += 24;
		if (s > x) x += 24;
		return s <= x && x < t;
	}
}
