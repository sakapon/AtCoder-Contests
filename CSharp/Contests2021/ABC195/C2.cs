using System;

class C2
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		Console.WriteLine(Comma(n));
	}

	static long Comma(long n)
	{
		if (n < 1000) return 0;

		for (var (c, x) = (1, 1000L); ; c++, x *= 1000)
			if (n < x * 1000)
				return c * (n - x + 1) + Comma(x - 1);
	}
}
