using System;
using System.Linq;

class E2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());
		var rn = (long)Math.Sqrt(n);
		return 2 * Enumerable.Range(1, (int)rn).Sum(i => n / i) - rn * rn;
	}
}
