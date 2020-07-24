using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1], q = h[2];
		var rs = new int[m].Select(_ => Read()).ToArray();
		var c = Read().Prepend(0).ToArray();

		var map = UndirectedMap(n, rs);

		var r = new List<int>();
		for (int i = 0; i < q; i++)
		{
			var s = Read();
			r.Add(c[s[1]]);

			if (s[0] == 1)
			{
				foreach (var x in map[s[1]])
					c[x] = c[s[1]];
			}
			else
			{
				c[s[1]] = s[2];
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}

	static List<int>[] UndirectedMap(int n, int[][] rs)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<int>());
		foreach (var r in rs)
		{
			map[r[0]].Add(r[1]);
			map[r[1]].Add(r[0]);
		}
		return map;
	}
}
