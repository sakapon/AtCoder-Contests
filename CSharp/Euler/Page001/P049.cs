using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8.Numerics;

class P049
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int n = 10000;
		var gs = Primes.GetPrimes(n).Where(p => p >= 1000).ToLookup(p => string.Join("", p.ToString().OrderBy(c => c)));

		var l = new List<string>();

		foreach (var g in gs)
		{
			var a = g.ToArray();

			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] == 1487) continue;

				for (int j = i + 1; j < a.Length; j++)
				{
					var ak = 2 * a[j] - a[i];
					if (a.Contains(ak)) l.Add($"{a[i]}{a[j]}{ak}");
				}
			}
		}
		return l.Single();
	}
}
