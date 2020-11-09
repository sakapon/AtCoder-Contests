using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1] - 1;
		var s = Array.ConvertAll(new int[n], _ => Console.ReadLine());

		var ws = s.GroupBy(w => w).Select(g => (w: g.Key, c: g.Count())).OrderBy(g => -g.c).ToArray();

		if (k > 0 && ws[k].c == ws[k - 1].c || k + 1 < ws.Length && ws[k].c == ws[k + 1].c)
		{
			Console.WriteLine("AMBIGUOUS");
			return;
		}

		Console.WriteLine(ws[k].w);
	}
}
