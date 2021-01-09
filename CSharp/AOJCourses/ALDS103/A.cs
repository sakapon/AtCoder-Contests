using System;
using System.Collections.Generic;

class A
{
	static void Main()
	{
		var es = Console.ReadLine().Split();
		var s = new Stack<int>();

		foreach (var e in es)
		{
			switch (e)
			{
				case "+":
					s.Push(s.Pop() + s.Pop());
					break;
				case "-":
					s.Push(-s.Pop() + s.Pop());
					break;
				case "*":
					s.Push(s.Pop() * s.Pop());
					break;
				default:
					s.Push(int.Parse(e));
					break;
			}
		}
		Console.WriteLine(s.Pop());
	}
}
