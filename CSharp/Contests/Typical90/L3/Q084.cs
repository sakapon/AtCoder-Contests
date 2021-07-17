using System;
using System.Linq;

class Q084
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var c = new long[n];

		for (int i = 0, t = -1; i < n; i++)
			if (s[i] == 'o') t = i;
			else c[i] = t + 1;

		for (int i = 0, t = -1; i < n; i++)
			if (s[i] == 'x') t = i;
			else c[i] = t + 1;

		return c.Sum();
	}
}
