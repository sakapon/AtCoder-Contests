using System;
using System.Linq;

class A2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var p = a.Select(Math.Sign).Aggregate((x, y) => x * y);
		return "-0+"[p + 1];
	}
}
