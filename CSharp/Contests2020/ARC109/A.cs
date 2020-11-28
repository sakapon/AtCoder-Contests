using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int a = h[0], b = h[1], x = h[2], y = h[3];
		Console.WriteLine((a > b ? a - b - 1 : b - a) * Math.Min(2 * x, y) + x);
	}
}
