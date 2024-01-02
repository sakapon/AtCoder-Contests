using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Console.ReadLine();
		var s = Console.ReadLine();

		var r = s.IndexOf("ABC");
		if (r != -1) r++;
		return r;
	}
}
