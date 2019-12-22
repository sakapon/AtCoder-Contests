using System;

class E
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		if (n % 2 == 1) { Console.WriteLine(0); return; }

		long r = 0, t = 2;
		while (t < n) r += n / (t *= 5);
		Console.WriteLine(r);
	}
}
