using System;
using System.Collections.Generic;
using System.Linq;

class P001
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int n = 1000;
		return Enumerable.Range(0, n).Where(x => x % 3 == 0 || x % 5 == 0).Sum();
	}
}
