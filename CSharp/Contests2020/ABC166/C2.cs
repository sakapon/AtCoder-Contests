using System;
using System.Linq;

class C2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var z = Read();
		var n = z[0];
		var h = Read();
		var es = new int[z[1]].Select(_ => Read()).ToArray();

		var map = es.Concat(es.Select(e => new[] { e[1], e[0] })).ToLookup(e => e[0], e => e[1]);
		Console.WriteLine(Enumerable.Range(1, n).Count(i => map[i].All(j => h[i - 1] > h[j - 1])));
	}
}
