using System;

class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b, k) = Read3L();

		for (int i = 0; ; i++)
		{
			if (a >= b) return i;
			a *= k;
		}
	}
}
