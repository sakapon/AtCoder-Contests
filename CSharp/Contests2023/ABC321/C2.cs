using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = int.Parse(Console.ReadLine());

		var l = new List<string> { "" };
		for (int i = 0; i < 10; i++)
		{
			var si = i.ToString();
			var c = l.Count;
			for (int j = 0; j < c; j++)
			{
				l.Add(si + l[j]);
			}
		}
		return l.Skip(1).OrderBy(long.Parse).ElementAt(k);
	}
}
