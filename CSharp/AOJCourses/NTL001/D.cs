using System;

class D
{
	static void Main() => Console.WriteLine(Totient(long.Parse(Console.ReadLine())));

	static long Totient(long n)
	{
		var r = n;
		for (long x = 2; x * x <= n && n > 1; ++x)
			if (n % x == 0)
			{
				r = r / x * (x - 1);
				while ((n /= x) % x == 0) ;
			}
		if (n > 1) r = r / n * (n - 1);
		return r;
	}
}
