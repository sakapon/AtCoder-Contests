using System;
using System.Collections.Generic;

class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Array.ConvertAll(new bool[4], _ => Console.ReadLine());

		var set = new HashSet<string> { "H", "2B", "3B", "HR" };
		return set.SetEquals(s);
	}
}
