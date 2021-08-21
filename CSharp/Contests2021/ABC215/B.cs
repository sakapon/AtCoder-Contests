using System;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var k = 0;
		while ((1L << k + 1) <= n) k++;
		return k;
	}
}
