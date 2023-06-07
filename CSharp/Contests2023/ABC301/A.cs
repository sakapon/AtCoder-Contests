using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var tc = s.Count(c => c == 'T');

		if (n % 2 == 0 && tc == n / 2) return s[^1] == 'T' ? 'A' : 'T';
		return tc <= n / 2 ? 'A' : 'T';
	}
}
