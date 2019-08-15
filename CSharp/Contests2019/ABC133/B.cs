using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var a = read();
		var ps = Enumerable.Range(0, a[0]).Select(i => read()).ToArray();

		Console.WriteLine(Comb2(a[0]).Select(i => Math.Sqrt(Enumerable.Range(0, a[1]).Sum(k => Math.Pow(ps[i[0]][k] - ps[i[1]][k], 2)))).Count(d => d == (int)d));
	}

	static IEnumerable<int[]> Comb2(int n) { for (var i = 0; i < n; i++) for (var j = i + 1; j < n; j++) yield return new[] { i, j }; }
}
