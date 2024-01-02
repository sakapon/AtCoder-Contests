using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var r = Enumerable.Range(1, n).OrderBy(i => -s[i - 1].Count(c => c == 'o'));
		return string.Join(" ", r);
	}
}
