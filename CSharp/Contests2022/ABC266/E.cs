using System;
using System.Linq;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var r = 0D;
		while (n-- > 0) r = Enumerable.Range(1, 6).Average(x => Math.Max(x, r));
		return r;
	}
}
