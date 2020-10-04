using System;
using System.Collections.Generic;
using System.Linq;

class B3
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];

		var st = new ST1<long>(n + 1);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Set(q[1], st[q[1]] + q[2], (x, y) => x + y);
			else
				r.Add(st.Get(q[1], q[2] + 1, 0, (x, y) => x + y));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
