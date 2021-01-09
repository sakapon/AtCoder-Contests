using System;
using System.Collections.Generic;

class A
{
	static void Main()
	{
		var r = new List<int>();
		var l = new List<int>();

		for (int k = int.Parse(Console.ReadLine()); k > 0; k--)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				l.Add(q[1]);
			}
			else if (q[0] == 1)
			{
				r.Add(l[q[1]]);
			}
			else
			{
				l.RemoveAt(l.Count - 1);
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
