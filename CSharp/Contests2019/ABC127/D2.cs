using System;
using System.Linq;

class D2
{
	static void Main()
	{
		Func<long[]> read = () => Console.ReadLine().Split().Select(long.Parse).ToArray();
		var h = read();
		var a = read().GroupBy(x => x).Select(g => new[] { g.Count(), g.Key }).Concat(new int[h[1]].Select(_ => read()))
			.GroupBy(x => x[1]).OrderByDescending(g => g.Key).Select(g => new { c = g.Key, b = g.Sum(x => x[0]) });

		var M = 0L;
		foreach (var _ in a)
		{
			var b = Math.Min(_.b, h[0]);
			M += _.c * b;
			if ((h[0] -= b) == 0) break;
		}
		Console.WriteLine(M);
	}
}
