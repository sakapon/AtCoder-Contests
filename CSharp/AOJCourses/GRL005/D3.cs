using System;
using System.Collections.Generic;
using System.Linq;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var n = int.Parse(Console.ReadLine());
		var map = Array.ConvertAll(new int[n], _ => Read().Skip(1).ToList());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new int[qc], _ => Read());

		var et = new EulerTour(n, 0, map);
		var st = new ST1<long>(2 * n, (x, y) => x + y, 0);

		foreach (var q in qs)
		{
			if (q[0] == 0)
			{
				st.Set(et.ordersMap[q[1]][0] - 1, st.Get(et.ordersMap[q[1]][0] - 1) + q[2]);
				st.Set(et.ordersMap[q[1]].Last(), st.Get(et.ordersMap[q[1]].Last()) - q[2]);
			}
			else
				Console.WriteLine(st.Get(0, et.ordersMap[q[1]][0]));
		}
		Console.Out.Flush();
	}
}
