using System;
using System.Linq;

class B2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());
		return 2 * n * (n + 1) - ReadL().Sum();
	}
}
