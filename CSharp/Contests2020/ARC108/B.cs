using System;
using System.Collections.Generic;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = "///" + Console.ReadLine();

		var l = new LinkedList<char>(s);
		var t = 0;
		var fox = "fox";

		for (var node = l.First; node != null; node = node.Next)
		{
			if (node.Value != fox[t])
			{
				t = 0;
			}

			if (node.Value == fox[t])
			{
				t++;

				if (t == 3)
				{
					t = 0;

					node = node.Previous;
					node = node.Previous;
					node = node.Previous;
					l.Remove(node.Next);
					l.Remove(node.Next);
					l.Remove(node.Next);
					node = node.Previous;
					node = node.Previous;
				}
			}
		}
		Console.WriteLine(l.Count - 3);
	}
}
