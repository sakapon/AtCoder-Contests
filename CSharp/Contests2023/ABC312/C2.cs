using System;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var b = Read();

		var c = a.Concat(b.Select(x => x + 1)).ToArray();
		Array.Sort(c);
		return c[m - 1];
	}
}
