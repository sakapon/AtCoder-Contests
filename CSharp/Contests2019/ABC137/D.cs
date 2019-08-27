using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var js = new int[h[0]].Select(_ => read()).Where(j => j[0] <= h[1]).GroupBy(j => j[0]).ToDictionary(g => g.Key, g => g.ToArray());

		var r = 0L;
		var l = new List<int>();
		for (var i = 1; i <= h[1]; i++)
		{
			if (js.ContainsKey(i))
				foreach (var j in js[i])
				{
					var k = l.BinarySearch(j[1]);
					if (k < 0) k = ~k;
					l.Insert(k, j[1]);
				}

			if (!l.Any()) continue;
			r += l.Last();
			l.RemoveAt(l.Count - 1);
		}
		Console.WriteLine(r);
	}
}
