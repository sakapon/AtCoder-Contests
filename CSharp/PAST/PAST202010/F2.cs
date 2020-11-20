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

		if (k > 0 && ws[k].Value == ws[k - 1].Value || k + 1 < ws.Length && ws[k].Value == ws[k + 1].Value)
		{
			Console.WriteLine("AMBIGUOUS");
			return;
		}

		Console.WriteLine(ws[k].Key);
	}
}
