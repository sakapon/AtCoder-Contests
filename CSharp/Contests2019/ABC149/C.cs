using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Enumerable.Range(int.Parse(Console.ReadLine()), 99).First(n => IsPrime(n)));

	static bool IsPrime(long n)
	{
		for (long x = 2; x * x <= n; ++x) if (n % x == 0) return false;
		return n > 1;
	}
}
