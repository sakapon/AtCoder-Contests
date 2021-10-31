using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var t = Console.ReadLine().Distinct().Count();
		return t == 1 ? 1 : t == 2 ? 3 : 6;
	}
}
