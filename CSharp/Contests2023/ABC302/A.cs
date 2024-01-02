using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b) = Read2L();
		return (a + b - 1) / b;
	}
}
