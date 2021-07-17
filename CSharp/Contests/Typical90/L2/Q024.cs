using System;
using System.Linq;

class Q024
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, k) = Read2();
		var a = Read();
		var b = Read();

		var d = a.Zip(b, (x, y) => Math.Abs(x - y)).Sum();

		if (k < d) return false;
		return (d - k) % 2 == 0;
	}
}
