using System;

class D
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		double a = h[0], b = h[1], x = h[2];

		var tan = x < a * a * b / 2 ? a * b * b / (2 * x) : 2 * (a * a * b - x) / (a * a * a);
		Console.WriteLine(Math.Atan(tan) * 180 / Math.PI);
	}
}
