using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var a = read();

		var s = a.Sum();
		foreach (var d in Divisors(s))
		{
			var m = a.Select(x => x % d).OrderByDescending(x => x).ToArray();
			if (m.Skip(m.Sum() / d).Sum() <= h[1]) { Console.WriteLine(d); return; }
		}
	}

	static IEnumerable<int> Divisors(int v)
	{
		var d = new List<int>();
		for (int i = 1, j, rv = (int)Math.Sqrt(v); i <= rv; i++)
			if (v % i == 0)
			{
				d.Add(i);
				if ((j = v / i) != i) yield return j;
			}
		for (int i = d.Count - 1; i >= 0; i--) yield return d[i];
	}
}
