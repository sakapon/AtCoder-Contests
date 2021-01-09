using System;
using System.Collections.Generic;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();

		var r = new List<string>();
		var ls = Array.ConvertAll(new int[h[0]], _ => new List<int>());

		for (int k = h[1]; k > 0; k--)
		{
			var q = Read();
			if (q[0] == 0)
			{
				ls[q[1]].Add(q[2]);
			}
			else if (q[0] == 1)
			{
				r.Add(string.Join(" ", ls[q[1]]));
			}
			else
			{
				ls[q[1]].Clear();
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
