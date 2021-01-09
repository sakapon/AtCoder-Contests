using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var z = Read();
		var n = z[0];
		var h = Read();
		var ps = new int[z[1]].Select(_ => Read()).ToArray();

		var map = Array.ConvertAll(new int[n + 1], _ => new List<int>());
		foreach (var e in ps)
		{
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}
		Console.WriteLine(Enumerable.Range(1, n).Count(i => map[i].All(j => h[i - 1] > h[j - 1])));
	}
}
