using System;
using System.Collections.Generic;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var r = 0;
		var q = new Stack<int>();
		var fox = "fox";

		foreach (var c in s)
		{
			if (q.TryPeek(out var t) && fox[t] == c)
			{
				q.Pop();
				if (t == 2) r++;
				else q.Push(t + 1);
			}
			else if (fox[0] == c)
			{
				q.Push(1);
			}
			else
			{
				q.Clear();
			}
		}
		Console.WriteLine(n - 3 * r);
	}
}
