using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var ws = new int[h[0]].Select(_ => read()).OrderBy(x => x[2]).ToArray();
		var d = new int[h[1]].Select(_ => int.Parse(Console.ReadLine())).ToList();
		var map = d.Select((x, i) => new { x, i }).ToDictionary(_ => _.x, _ => _.i);
		var c = new int[h[1]];

		foreach (var w in ws)
		{
			var si = d.BinarySearch(w[0] - w[2]);
			if (si < 0) si = ~si;
			var ti = d.BinarySearch(w[1] - w[2]);
			if (ti < 0) ti = ~ti;

			for (int i = si; i < ti; i++) c[map[d[i]]] = w[2];
			d.RemoveRange(si, ti - si);
		}
		Console.WriteLine(string.Join("\n", c.Select(x => x != 0 ? x : -1)));
	}
}
