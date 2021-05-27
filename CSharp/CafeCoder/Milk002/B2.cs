using System;
using System.Linq;

class B2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var q = s.Reverse().SelectMany((c, i) => i > 0 && i % 3 == 0 ? new[] { ',', c } : new[] { c }).Reverse();
		return string.Join("", q);
	}
}
