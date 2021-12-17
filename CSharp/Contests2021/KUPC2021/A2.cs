using System;
using System.Linq;

class A2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Read();
		var t = int.Parse(Console.ReadLine());

		return s.Select(x => x / t).Distinct().Count();
	}
}
