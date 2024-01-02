using System;
using System.Collections.Generic;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var l = 0;
		var q = new Stack<char>();

		foreach (char c in s)
		{
			if (c == ')' && l > 0)
			{
				l--;
				while (q.Pop() != '(') ;
			}
			else
			{
				if (c == '(') l++;
				q.Push(c);
			}
		}

		var r = q.ToArray();
		Array.Reverse(r);
		return new string(r);
	}
}
