using System;
using System.Linq;

class Q020L
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (a, b, c) = Read3L();
		return a < Enumerable.Repeat(c, (int)b).Aggregate((x, y) => x * y);
	}
}
