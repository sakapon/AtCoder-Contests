using System;

class C2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b, c) = Read3L();

		if (c % 2 == 0)
		{
			a *= a;
			b *= b;
		}
		return a == b ? '=' : a < b ? '<' : '>';
	}
}
