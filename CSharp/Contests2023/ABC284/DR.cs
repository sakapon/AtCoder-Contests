using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using YJLib8.Numerics;

class DR
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var r = RhoFactorization.Factorize(n);
		return r[0] == r[1] ? $"{r[0]} {r[2]}" : $"{r[2]} {r[0]}";
	}
}
