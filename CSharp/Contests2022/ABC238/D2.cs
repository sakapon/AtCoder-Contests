using System;
using System.Linq;

class D2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve() ? "Yes" : "No")));
	static bool Solve()
	{
		var (a, s) = Read2L();

		var f = 0L;

		for (int i = 0; i < 60; i++)
		{
			var d = 1L << i;
			if ((a & d) != 0) f += d;
		}

		s -= f << 1;
		return s >= 0 && (s & f) == 0;
	}
}
