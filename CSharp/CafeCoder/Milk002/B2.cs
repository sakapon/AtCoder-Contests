using System;
using System.Linq;

class B2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var m = s.Length % 3;

		var q = s.SelectMany((c, i) => i > 0 && i % 3 == m ? new[] { ',', c } : new[] { c });
		return string.Join("", q);
	}
}
