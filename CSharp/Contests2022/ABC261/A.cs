using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (l1, r1, l2, r2) = Read4();

		var c = new int[100];
		for (int i = l1; i < r1; i++) c[i]++;
		for (int i = l2; i < r2; i++) c[i]++;
		return c.Count(x => x == 2);
	}
}
