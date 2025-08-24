using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var r = new List<long>();
		var d = Enumerable.Range(0, n).ToDictionary(i => i + 1, i => (long)a[i]);
		var v0 = 0L;

		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				d.Clear();
				v0 = q[1];
			}
			else if (q[0] == 2)
			{
				var i = q[1];
				var x = q[2];
				d[i] = d.GetValueOrDefault(i) + x;
			}
			else
			{
				var i = q[1];
				r.Add(v0 + d.GetValueOrDefault(i));
			}
		}
		return string.Join("\n", r);
	}
}
