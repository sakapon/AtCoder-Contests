using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split().Select(int.Parse).GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		var A = a.Max(p => p.Key);

		var c = new double[A + 1];
		for (var d = 1; d <= A; d++) c[d] = 1.0 / d;
		for (var d = 1; d <= A / 2; d++)
			for (var i = 2 * d; i <= A; i += d) c[i] -= c[d];

		var s = 0.0;
		for (var d = 1; d <= A; d++)
		{
			double si = 0, si2 = 0;
			foreach (var p in a)
			{
				if (p.Key % d != 0) continue;
				si += (double)p.Value * p.Key;
				si2 += (double)p.Value * p.Key * p.Key;
			}
			s += (si * si - si2) / 2 * c[d];
		}
		Console.WriteLine(s % 998244353);
	}
}
