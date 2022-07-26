using System;

class A2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (l1, r1, l2, r2) = Read4();

		l1 = Math.Max(l1, l2);
		r1 = Math.Min(r1, r2);
		return Math.Max(r1 - l1, 0);
	}
}
