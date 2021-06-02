using System;
using System.Linq;

class Q052
{
	const long M = 1000000007;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ => ReadL().Sum());

		return a.Aggregate((x, y) => x * y % M);
	}
}
