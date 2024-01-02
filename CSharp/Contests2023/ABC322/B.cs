using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Console.ReadLine();
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		if (t.StartsWith(s))
		{
			if (t.EndsWith(s))
				return 0;
			else
				return 1;
		}
		else
		{
			if (t.EndsWith(s))
				return 2;
			else
				return 3;
		}
	}
}
