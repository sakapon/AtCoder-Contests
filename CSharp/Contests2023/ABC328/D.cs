using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var q = new ArrayDeque<char>();

		foreach (var c in s)
		{
			if (c == 'C' && q.Count >= 2 && q[0] == 'B' && q[1] == 'A')
			{
				q.PopFirst();
				q.PopFirst();
			}
			else
			{
				q.AddFirst(c);
			}
		}

		var cs = q.ToArray();
		Array.Reverse(cs);
		return new string(cs);
	}
}
