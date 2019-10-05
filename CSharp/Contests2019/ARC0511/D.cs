using System;

class D
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());

		var r = 0L;
		for (long i = 1; i * i + i < n; i++) if (n % i == 0) r += n / i - 1;
		Console.WriteLine(r);
	}
}
