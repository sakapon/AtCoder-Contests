using System;
using System.Numerics;

class D
{
	static void Main()
	{
		BigInteger x = BigInteger.Parse(Console.ReadLine());

		for (BigInteger a = -1000; a <= 1000; a++)
		{
			for (BigInteger b = -1000; b <= 1000; b++)
			{
				if (BigInteger.Pow(a, 5) - BigInteger.Pow(b, 5) == x) { Console.WriteLine($"{a} {b}"); return; }
			}
		}
	}
}
