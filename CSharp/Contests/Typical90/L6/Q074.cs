using System;
using System.Linq;

class Q074
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		return s.Select((c, i) => (c - 'a') * (1L << i)).Sum();
	}
}
