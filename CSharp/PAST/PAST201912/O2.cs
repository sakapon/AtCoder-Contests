using System;
using System.Linq;

class O2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var e = new double[n];
		var M = 1.0;
		foreach (var _ in Enumerable.Range(0, n).SelectMany(d => Console.ReadLine().Split().Select(s => new { d, x = int.Parse(s) })).OrderBy(_ => -_.x))
			M = Math.Max(M, (e[_.d] += M / 6) + 1);
		Console.WriteLine(M);
	}
}
