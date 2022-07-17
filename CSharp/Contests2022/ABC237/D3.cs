using System;
using System.Collections.Generic;

class D3
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var l = new LinkedList<int>();
		l.AddFirst(n);

		for (int i = n - 1; i >= 0; i--)
			if (s[i] == 'L')
				l.AddLast(i);
			else
				l.AddFirst(i);

		return string.Join(" ", l);
	}
}
