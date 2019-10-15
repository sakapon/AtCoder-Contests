using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var n = int.Parse(Console.ReadLine());
		var a = read();
		var m = int.Parse(Console.ReadLine());
		var c = new int[m].Select(_ => read()).ToArray();

		var map = Enumerable.Range(1, n).Select(i => c.Select((x, j) => new { x, j }).Where(_ => _.x[0] <= i && i <= _.x[1]).Select(_ => _.j).ToArray()).ToArray();
		var b = c.Select(x => x[2]).ToArray();

		var r = 0L;
		while (true)
		{
			var d = map.Select((x, i) => x.Sum(j => (long)b[j]) - a[i]).ToArray();
			var mi = MaxWithIndex(d);
			if (mi.Value <= 0) break;
			r += mi.Value;
			foreach (var i in map[mi.Key]) b[i] = 0;
		}
		Console.WriteLine(r);
	}

	static KeyValuePair<int, long> MaxWithIndex(IList<long> a)
	{
		if (a.Count == 0) return new KeyValuePair<int, long>(-1, long.MinValue);
		var k = 0;
		var m = a[0];
		for (var i = 1; i < a.Count; i++) if (a[i] > m) { k = i; m = a[i]; }
		return new KeyValuePair<int, long>(k, m);
	}
}
