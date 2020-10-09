using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];

		var st = new ST_Min(n);
		st.InitAllLevels(int.MaxValue);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Set(q[1], q[2]);
			else
				r.Add(st.Submin(q[1], q[2] + 1));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
