using System;
using System.Collections.Generic;
using System.Linq;

class B0
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		// k = 3
		// true: removed
		var r = new bool[2000];

		for (int i = 2; i <= 1000; i++)
		{
			if (r[i]) continue;

			for (int j = 1; j < i; j++)
			{
				if (!r[j])
				{
					r[i + i - j] = true;
				}
			}
		}
		return string.Join(" ", Enumerable.Range(1, 1000).Where(i => !r[i]));
	}
}
