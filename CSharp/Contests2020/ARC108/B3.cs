using System;
using System.Collections.Generic;
using System.Linq;

class B3
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var q = new Stack<char>();
		foreach (var c in s)
		{
			if (c == 'x' && q.Take(2).SequenceEqual("of"))
			{
				q.Pop();
				q.Pop();
			}
			else
			{
				q.Push(c);
			}
		}
		Console.WriteLine(q.Count);
	}
}
