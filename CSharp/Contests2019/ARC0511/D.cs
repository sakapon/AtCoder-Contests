using System;

class D
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		if (n == 2) { Console.WriteLine(0); return; }

		var r = 0L;
		var rn = Math.Sqrt(n);
		for (int i = 1; i < rn; i++) if ((n - i) % i == 0) r += (n - i) / i;
		Console.WriteLine(r);
	}
}
