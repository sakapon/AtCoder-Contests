using System;

class D
{
	static void Main()
	{
		var x = long.Parse(Console.ReadLine());

		for (long a = -999; a < 999; a++)
			for (long b = -999; b < 999; b++)
				if (a * a * a * a * a - b * b * b * b * b == x) { Console.WriteLine($"{a} {b}"); return; }
	}
}
