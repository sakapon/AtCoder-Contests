using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections;

class CL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Read();
		var a = Read();
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => Read()[0]);

		var set = new BSArray<int>(a, true);
		return string.Join("\n", qs.Select(k => set.GetFirstIndex(x => x >= k)));
	}
}
