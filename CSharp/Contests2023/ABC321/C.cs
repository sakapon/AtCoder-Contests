using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = int.Parse(Console.ReadLine());

		var r = Enumerable.Range(1, 1023)
			.Select(x => ToElements(x).Select(i => (char)('0' + i)).ToArray())
			.Select(cs => long.Parse(new string(cs)))
			.OrderBy(x => x)
			.ToArray();
		return r[k];
	}

	static IEnumerable<int> ToElements(int x)
	{
		for (int i = 10; --i >= 0;)
			if ((x & (1 << i)) != 0) yield return i;
	}
}
