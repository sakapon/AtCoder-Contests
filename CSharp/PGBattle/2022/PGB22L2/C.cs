using System;
using System.Collections.Generic;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = 0L;
		var q = new Stack<int>();

		foreach (var x in a)
		{
			if (x == 1)
			{
				q.Push(1);
			}
			else
			{
				while (q.Count > 0 && x != q.Peek() + 1)
				{
					q.Pop();
				}

				if (q.Count > 0)
				{
					q.Pop();
					q.Push(x);
				}
				else
				{
					r += x;
				}
			}
		}

		return r;
	}
}
