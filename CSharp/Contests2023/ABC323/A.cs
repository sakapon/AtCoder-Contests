using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		return s.Where((c, i) => i % 2 == 1).All(c => c == '0');
	}
}
