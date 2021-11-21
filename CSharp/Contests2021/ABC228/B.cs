using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x) = Read2();
		var a = Read();

		var u = new bool[n + 1];
		for (; !u[x]; x = a[x - 1])
			u[x] = true;
		return u.Count(b => b);
	}
}
