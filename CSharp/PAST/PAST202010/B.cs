using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int x = h[0], y = h[1];

		if (y == 0) { Console.WriteLine("ERROR"); return; }

		decimal d = 100 * x / y;
		Console.WriteLine($"{d / 100:F2}");
	}
}
