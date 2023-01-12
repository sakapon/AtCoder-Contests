using System;
using System.Linq;

class A2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var b = ReadL();

		var min = b.Min();
		var max = b.Max();
		var sum = (max + min) * (n + 1) / 2;
		return sum - b.Sum();
	}
}
