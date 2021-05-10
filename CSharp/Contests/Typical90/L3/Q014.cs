using System;
using System.Linq;

class Q014
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var b = ReadL();

		Array.Sort(a);
		Array.Sort(b);
		return a.Zip(b, (x, y) => Math.Abs(x - y)).Sum();
	}
}
