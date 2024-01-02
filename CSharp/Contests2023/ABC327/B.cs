using System;
using System.Numerics;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var b = BigInteger.Parse(Console.ReadLine());

		for (int a = 1; a < 20; a++)
			if (BigInteger.Pow(a, a) == b) return a;
		return -1;
	}
}
