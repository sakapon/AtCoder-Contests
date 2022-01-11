using System;
using System.Linq;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = long.Parse(Console.ReadLine());
		return string.Join("", Enumerable.Range(0, 60).Select(i => (k & (1L << i)) == 0 ? 0 : 2).Reverse()).TrimStart('0');
	}
}
