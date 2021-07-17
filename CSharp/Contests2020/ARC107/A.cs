using System;
using System.Linq;

class A
{
	const long M = 998244353;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(ReadL().Select(x => x * (x + 1) / 2 % M).Aggregate((x, y) => x * y % M));
}
