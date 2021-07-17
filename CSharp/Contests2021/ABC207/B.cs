using System;

class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b, c, d) = Read4L();

		var e = c * d - b;
		if (e <= 0) return -1;
		return (a + e - 1) / e;
	}
}
