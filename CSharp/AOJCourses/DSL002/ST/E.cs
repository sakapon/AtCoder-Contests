﻿using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];

		var st = new ST_RangeAdd(n + 1);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Add(q[1], q[2] + 1, q[3]);
			else
				r.Add(st[q[1]]);
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
