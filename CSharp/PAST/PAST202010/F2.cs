using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1] - 1;
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var ws = s.GroupCounts(w => w).OrderBy(p => -p.Value).ToArray();
		var tie = ws.GroupCounts(p => p.Value);
		Console.WriteLine(tie[ws[k].Value] == 1 ? ws[k].Key : "AMBIGUOUS");
	}
}
