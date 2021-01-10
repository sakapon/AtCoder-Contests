using System;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (x, y) = Read2L();

		if ((x, y) == (0, 0)) return "1 1";
		if (x < y) return $"{x + 2 * y} {y}";
		if (x > y) return $"{x} {2 * x + y}";
		return -1;
	}
}
