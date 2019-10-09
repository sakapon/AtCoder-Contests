using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var ws = new int[h[0]].Select(_ => read()).OrderByDescending(x => x[2]).ToArray();
		var d = new int[h[1]].Select(_ => int.Parse(Console.ReadLine())).ToArray();
		var c = new int[h[1]];

		foreach (var w in ws)
		{
			var si = Array.BinarySearch(d, w[0] - w[2]);
			if (si < 0) si = ~si;
			var ti = Array.BinarySearch(d, w[1] - w[2]);
			if (ti < 0) ti = ~ti;

			for (int i = si; i < ti; i++) c[i] = w[2];
		}
		Console.WriteLine(string.Join("\n", c.Select(x => x != 0 ? x : -1)));
	}
}
