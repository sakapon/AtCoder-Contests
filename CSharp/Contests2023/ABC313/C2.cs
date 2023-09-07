using System;
using System.Linq;

class C2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var b = a.Sum() / n;
		return Math.Min(Count(b), Count(b + 1));

		long Count(long b) => Math.Max(a.Where(v => v <= b).Sum(v => b - v), a.Where(v => v > b).Sum(v => v - b - 1));
	}
}
