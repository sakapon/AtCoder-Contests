using System;
using System.Linq;

class C2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var x = long.Parse(Console.ReadLine());

		var q = Math.DivRem(x, a.Sum(), out x);
		return n * q + Enumerable.Range(0, n).First(i => (x -= a[i]) < 0) + 1;
	}
}
