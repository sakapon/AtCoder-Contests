using System;
using System.Collections.Generic;

class C
{
	static void Main()
	{
		var l = new LinkedList<int>();
		var node = l.AddLast(1 << 30);

		for (int k = int.Parse(Console.ReadLine()); k > 0; k--)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				node = l.AddBefore(node, q[1]);
			}
			else if (q[0] == 1)
			{
				if (q[1] >= 0)
					for (int i = 0; i < q[1]; i++)
						node = node.Next;
				else
					for (int i = 0; i > q[1]; i--)
						node = node.Previous;
			}
			else
			{
				node = node.Next;
				l.Remove(node.Previous);
			}
		}

		l.RemoveLast();
		Console.WriteLine(string.Join("\n", l));
	}
}
