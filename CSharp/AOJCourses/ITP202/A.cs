using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<int>();
		var h = Read();
		var n = h[0];

		var qs = new int[n].Select(_ => new Stack<int>()).ToArray();

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
			{
				qs[q[1]].Push(q[2]);
			}
			else if (q[0] == 1)
			{
				if (qs[q[1]].Any()) r.Add(qs[q[1]].Peek());
			}
			else
			{
				if (qs[q[1]].Any()) qs[q[1]].Pop();
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
