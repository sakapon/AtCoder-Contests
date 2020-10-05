using System;
using System.Collections.Generic;
using System.Linq;

class E3
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];

		var st = new STR<long>(n + 1, (x, y) => x + y, 0);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Set(q[1], q[2] + 1, q[3]);
			else
				r.Add(st.Get(q[1]));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
