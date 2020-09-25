using System;
using System.Linq;

class D2
{
	static void Main()
	{
		// たかだか 26 種類しかありません。
		var w = Console.ReadLine().GroupBy(c => c).Select(g => g.Count()).ToList();
		if (w.Count == 1) { Console.WriteLine(w[0]); return; }

		var r = 0L;
		while (w.Count > 1)
		{
			w.Sort();
			var nw = w[0] + w[1];
			w.RemoveRange(0, 2);
			w.Add(nw);
			r += nw;
		}
		Console.WriteLine(r);
	}
}
