using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var q1 = new Queue<int>();
		var q2 = new Stack<int>();

		for (int i = 0; i < n; i++)
			if (s[i] == 'L')
				q2.Push(i);
			else
				q1.Enqueue(i);
		q1.Enqueue(n);

		return string.Join(" ", q1.Concat(q2));
	}
}
