using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = Enumerable.Range(1, 3 * n)
			.OrderBy(i => a[i - 1])
			.Skip(n)
			.Take(n)
			.OrderBy(i => i);
		return string.Join("\n", r);
	}
}
