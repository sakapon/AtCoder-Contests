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

		var k = 0;
		var l = new LinkedList<char>(s);
		var node = l.First;

		while (true)
		{
			if (node.Value == ')' && k > 0)
			{
				k--;
				while (true)
				{
					var c = node.Value;
					var t = node.Previous;
					l.Remove(node);
					node = t;
					if (c == '(') break;
				}
			}
			else
			{
				if (node.Value == '(') k++;
			}

			if ((node = node == null ? l.First : node.Next) == null) break;
		}

		var r = l.ToArray();
		return new string(r);
	}
}
