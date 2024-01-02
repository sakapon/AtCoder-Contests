using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = long.Parse(Console.ReadLine());

		while (n % 2 == 0) n /= 2;
		while (n % 3 == 0) n /= 3;
		return n == 1;
	}
}
