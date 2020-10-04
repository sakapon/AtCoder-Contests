using System;
using System.Collections.Generic;
using System.Linq;

class A3
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];

		var st = new ST1<int>(n);
		st.Init(new int[0], int.MaxValue, Math.Min);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Set(q[1], q[2], Math.Min);
			else
				r.Add(st.Get(q[1], q[2] + 1, int.MaxValue, Math.Min));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
