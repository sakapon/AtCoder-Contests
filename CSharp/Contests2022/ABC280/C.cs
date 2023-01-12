using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		return Enumerable.Range(0, t.Length).First(i => i == s.Length || s[i] != t[i]) + 1;
	}
}
