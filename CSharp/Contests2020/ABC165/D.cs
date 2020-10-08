using System;

class D
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), decimal.Parse);
		decimal a = h[0], b = h[1], n = h[2];
		Console.WriteLine(Math.Floor(Math.Min(n, b - 1) * a / b));
	}
}
