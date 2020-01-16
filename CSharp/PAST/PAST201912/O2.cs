using System;
using System.Linq;

class O2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Enumerable.Range(0, n).SelectMany(d => Console.ReadLine().Split().Select(s => new { d, x = int.Parse(s) })).OrderBy(_ => _.x).ToArray();

		var e = new double[n];
		var M = 1.0;
		for (int i = 6 * n - 1; i >= 0; i--)
			M = Math.Max(M, (e[a[i].d] += M / 6) + 1);
		Console.WriteLine(M);
	}
}
