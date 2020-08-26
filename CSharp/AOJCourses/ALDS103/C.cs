using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var qs = new int[n].Select(_ => Console.ReadLine().Split());

		var l = new LinkedList<int>();

		foreach (var q in qs)
		{
			switch (q[0])
			{
				case "insert":
					l.AddFirst(int.Parse(q[1]));
					break;
				case "delete":
					l.Remove(int.Parse(q[1]));
					break;
				case "deleteFirst":
					l.RemoveFirst();
					break;
				case "deleteLast":
					l.RemoveLast();
					break;
				default:
					break;
			}
		}
		Console.WriteLine(string.Join(" ", l));
	}
}
