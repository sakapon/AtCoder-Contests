using System;
using System.Collections.Generic;
using System.Linq;

class D3
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<int?>();
		var h = Read();
		var n = h[0];

		var st = new STR<int?>(n, (x, y) => x.HasValue ? x : y, null);
		st.Set(0, n, int.MaxValue);

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
