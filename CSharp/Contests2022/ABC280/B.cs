using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = ReadL();

		var a = Enumerable.Range(0, n - 1).Select(i => s[i + 1] - s[i]).Prepend(s[0]);
		return string.Join(" ", a);
	}
}
