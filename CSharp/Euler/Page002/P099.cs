using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8.Linq;
using static EulerLib8.Common;

class P099
{
	const string textUrl = "https://projecteuler.net/project/resources/p099_base_exp.txt";
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var t = GetText(textUrl).Split('\n')
			.Select(l => Array.ConvertAll(l.Split(','), int.Parse))
			.Select((a, i) => (i, v: a[1] * Math.Log(a[0])))
			.FirstMax(p => p.v);
		return t.i + 1;
	}
}
