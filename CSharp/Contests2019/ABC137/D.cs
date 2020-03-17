using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var js = new int[h[0]].Select(_ => read()).ToLookup(j => j[0], j => j[1]);

		var r = 0;
		var l = new List<int>();
		for (var i = 1; i <= h[1]; i++)
		{
			foreach (var b in js[i])
			{
				var k = l.BinarySearch(b);
				if (k < 0) k = ~k;
				l.Insert(k, b);
			}

			if (!l.Any()) continue;
			r += l.Last();
			l.RemoveAt(l.Count - 1);
		}
		Console.WriteLine(r);
	}
}
