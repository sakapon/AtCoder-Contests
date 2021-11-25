using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (x, y) = Read2();

		var dx = Enumerable.Range(1, x).Count(i => x % i == 0);
		var dy = Enumerable.Range(1, y).Count(i => y % i == 0);
		return dx == dy ? "Z" : dx > dy ? "X" : "Y";
	}
}
