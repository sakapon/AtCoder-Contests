using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var x = s[0];
		var y = s[1];
		var z = s[2];

		return new[] { $"{x}{y}{z}", $"{x}{z}{y}", $"{y}{x}{z}", $"{y}{z}{x}", $"{z}{x}{y}", $"{z}{y}{x}" }
			.Distinct().Count();
	}
}
