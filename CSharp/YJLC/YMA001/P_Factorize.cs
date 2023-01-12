using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using YJLib8.Numerics;

class P_Factorize
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var qc = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[qc], _ => long.Parse(Console.ReadLine()));

		var d = new Dictionary<long, long[]>();
		foreach (var x in a)
			if (!d.ContainsKey(x)) d[x] = RhoFactorization.Factorize(x);

		return string.Join("\n", a.Select(x => d[x]).Select(r => string.Join(" ", r.Prepend(r.Length))));
	}
}
