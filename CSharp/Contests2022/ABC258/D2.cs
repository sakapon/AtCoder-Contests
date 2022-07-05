using System;
using System.Linq;

class D2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x) = Read2L();
		var abs = Array.ConvertAll(new bool[n], _ => ReadL());

		long s = 0, i = 0;
		return abs[..(int)Math.Min(x, n)].Min(ab => (s += ab[0] + ab[1]) + ab[1] * (x - ++i));
	}
}
